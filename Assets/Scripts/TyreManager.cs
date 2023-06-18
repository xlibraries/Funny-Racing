using UnityEngine;

public class TyreManager : MonoBehaviour
{
    public GameObject parentPrefab;
    public string frontTyreName;
    public string backTyreName;

    public float linearDragDelta = 0.1f;
    public float angularDragDelta = 0.01f;
    private float initialFrontLinearDrag = 0.2f;
    private float initialFrontAngularDrag = 0.01f;
    [HideInInspector] public float frontLinearDrag;
    [HideInInspector] public float frontAngularDrag;

    private Transform frontTyreTransform;
    private Transform backTyreTransform;
    private Rigidbody2D frontTyreRb;
    private Rigidbody2D backTyreRb;

    private const string TyreKey = "TyreProperties";

    private TyreData tyreData = new();

    public static TyreManager Instance { get; private set; }

    private void Awake()
    {
        // Set the Instance reference when the script is initialized
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        // Initialize the SuspensionManager script
        // (e.g., load values, set properties)

        LoadTyreValue();
    }

    private void Start()
    {
        GetTyres();
        ApplyTyreProperties();
    }

    private void GetTyres()
    {
        frontTyreTransform = parentPrefab.transform.Find(frontTyreName);
        backTyreTransform = parentPrefab.transform.Find(backTyreName);

        if (frontTyreTransform != null && backTyreTransform != null)
        {
            frontTyreRb = frontTyreTransform.GetComponent<Rigidbody2D>();
            backTyreRb = backTyreTransform.GetComponent<Rigidbody2D>();

            //Debug.Log("FrontTyre GameObject found: " + frontTyreTransform.name);
            //Debug.Log("BackTyre GameObject found: " + backTyreTransform.name);
        }
        else
        {
            Debug.LogError("Child GameObject not found. Make sure to set the correct name in the ChildGameObjectGetter script.");
        }
    }

    // Initialize the tyre with an initial Linear Drag & initial Angular Drag
    public void Initialize(float initialLinearDrag, int initialAngularDrag)
    {
        frontLinearDrag = initialLinearDrag;
        initialFrontLinearDrag = initialLinearDrag;
        frontAngularDrag = initialAngularDrag;
        initialFrontAngularDrag = initialAngularDrag;
    }

    public void UpgradeTyre()
    {
        ModifyTyreProperties(linearDragDelta, angularDragDelta);
    }

    public void DowngradeTyre()
    {
        ModifyTyreProperties(-linearDragDelta, -angularDragDelta);
    }

    private void ModifyTyreProperties(float linearDragDelta, float angularDragDelta)
    {
        frontLinearDrag += linearDragDelta;
        frontAngularDrag += angularDragDelta;

        ApplyTyreProperties();
        SaveTyreValue();
    }

    public void ResetTyre()
    {
        frontLinearDrag = initialFrontLinearDrag;
        frontAngularDrag = initialFrontAngularDrag;

        ApplyTyreProperties();
        SaveTyreValue();
    }

    public void SetTyreData(float linearDrag, float angularDrag)
    {
        frontLinearDrag = linearDrag;
        frontAngularDrag = angularDrag;

        ApplyTyreProperties();
        SaveTyreValue();
    }

    private void ApplyTyreProperties()
    {
        if (parentPrefab != null)
        {
            frontTyreRb.drag = frontLinearDrag;
            frontTyreRb.angularDrag = frontAngularDrag;
            backTyreRb.drag = frontLinearDrag;
            backTyreRb.angularDrag = frontAngularDrag;

            //Debug.Log("LinearDrag: " + frontTyreRb.drag);
            //Debug.Log("AngularDrag: " + frontTyreRb.angularDrag);
        }
        else
        {
            Debug.LogError("Parent Prefab component is missing. Make sure to assign the Parent Prefab GameObject to the SuspensionManager script in the Inspector.");
        }
    }

    private void SaveTyreValue()
    {
        tyreData.LinearDrag = frontLinearDrag;
        tyreData.AngularDrag = frontAngularDrag;
        PlayerPrefs.SetFloat("LinerDrag", tyreData.LinearDrag);
        PlayerPrefs.SetFloat("AngularDrag", tyreData.AngularDrag);
        string json = JsonUtility.ToJson(tyreData);
        PlayerPrefs.SetString(TyreKey, json);
        PlayerPrefs.Save();
    }

    private void LoadTyreValue()
    {
        if (PlayerPrefs.HasKey(TyreKey))
        {
            frontLinearDrag = PlayerPrefs.GetFloat("LinerDrag");
            frontAngularDrag = PlayerPrefs.GetFloat("AngularDrag");
        }
    }

    [System.Serializable]
    public class TyreData
    {
        public float LinearDrag { get; set; }
        public float AngularDrag { get; set; }
    }
}