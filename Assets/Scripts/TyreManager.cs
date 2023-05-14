using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreManager : MonoBehaviour
{
    public CarController carController; // Reference to the CarController GameObject

    private float frontWheelLinearDrag;
    private float initialFrontWheelLinearDrag = 0.1f;

    private float frontWheelAngularDrag;
    private float initialFrontWheelAngularDrag = 0.05f;

    public float linearDragDelta = 7;
    public float angularDragDelta = 1.5f;

    private const string TyreKey = "TyreProperties";

    TyreData tyreData = new();

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

        // Initialize the TyreManager script
        // (e.g., load values, set properties)

        LoadTyreValue();
    }

    private void Start()
    {
        ApplyTyreProperties();
    }

    // Initialize the tyre with an initial linearDrag 
    public void Initialize(int initialLinearDrag, int initialAngularDrag)
    {
        frontWheelLinearDrag = initialLinearDrag;
        initialFrontWheelLinearDrag = initialLinearDrag;
        frontWheelAngularDrag = initialAngularDrag;
        initialFrontWheelAngularDrag = initialAngularDrag;
    }

    // Upgrade the tyre by adding a linearDrag delta
    public void UpgradeTyre()
    {
        frontWheelLinearDrag *= linearDragDelta;
        frontWheelAngularDrag *= angularDragDelta;
        ApplyTyreProperties();
        SaveTyreValue();
    }

    // Downgrade the tyre by subtracting a linearDrag delta
    public void DowngradeTyre()
    {
        frontWheelLinearDrag -= linearDragDelta;
        frontWheelAngularDrag -= angularDragDelta;
        ApplyTyreProperties();
        SaveTyreValue();
    }

    // Reset the tyre to its initial linearDrag 
    public void ResetTyre()
    {
        frontWheelLinearDrag = initialFrontWheelLinearDrag;
        frontWheelAngularDrag = initialFrontWheelAngularDrag;
        ApplyTyreProperties();
        SaveTyreValue();
    }

    // Set the tyre linearDrag to a specified value
    public void SetTyreData(int linearDrag, int angularDrag)
    {
        frontWheelLinearDrag = linearDrag;
        frontWheelAngularDrag = angularDrag;
        ApplyTyreProperties();
        SaveTyreValue();
    }

    private void ApplyTyreProperties()
    {
        if (carController != null)
        {
            CircleCollider2D frontWheelCollider = carController.frontWheel.GetComponentInChildren<CircleCollider2D>();
            Rigidbody2D frontWheelRigidbody = carController.frontWheel.GetComponentInChildren<Rigidbody2D>();
            frontWheelRigidbody.drag = frontWheelLinearDrag;
            frontWheelRigidbody.angularDrag = frontWheelAngularDrag;
            frontWheelCollider.sharedMaterial.friction = frontWheelLinearDrag / 1000f;
            frontWheelCollider.sharedMaterial.bounciness = 0f;

            CircleCollider2D backWheelCollider = carController.backWheel.GetComponentInChildren<CircleCollider2D>();
            Rigidbody2D backWheelRigidbody = carController.backWheel.GetComponentInChildren<Rigidbody2D>();
            backWheelRigidbody.drag = frontWheelLinearDrag;
            backWheelRigidbody.angularDrag = frontWheelAngularDrag;
            backWheelCollider.sharedMaterial.friction = frontWheelLinearDrag / 1000f;
            backWheelCollider.sharedMaterial.bounciness = 0f;

            Debug.Log("LinearDrag: " + frontWheelLinearDrag);
            Debug.Log("AngularDrag: " + frontWheelAngularDrag);
        }
        else
        {
            Debug.LogError("CarController component is missing. Make sure to assign the CarController GameObject to the TyreManager script in the Inspector.");
        }
    }



    // Save the tyre value using PlayerPrefs
    private void SaveTyreValue()
    {
        tyreData.MaxLinearDrag = frontWheelLinearDrag;
        tyreData.MaxSpeed = frontWheelAngularDrag;
        PlayerPrefs.SetFloat("LinearDragKey", tyreData.MaxLinearDrag);
        PlayerPrefs.SetFloat("AngularDragKey", tyreData.MaxSpeed);
        string json = JsonUtility.ToJson(tyreData);
        PlayerPrefs.SetString(TyreKey, json);
        PlayerPrefs.Save();
    }

    // Load the tyre value from PlayerPrefs
    private void LoadTyreValue()
    {
        if (PlayerPrefs.HasKey(TyreKey))
        {
            frontWheelLinearDrag = PlayerPrefs.GetInt("LinearDragKey");
            frontWheelAngularDrag = PlayerPrefs.GetFloat("AngularDragKey");
        }
    }

    [System.Serializable]
    public class TyreData
    {
        public float MaxSpeed { get; set; }
        public float MaxLinearDrag { get; set; }
    }
}
