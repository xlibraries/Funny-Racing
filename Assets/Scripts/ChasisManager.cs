using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasisManager : MonoBehaviour
{
    public CarController carController; // Reference to the CarController GameObject

    [HideInInspector] public float veichelMass;
    private float initialVeichelMass= 0.5f;

    public float veichelMassDelta = 0.1f;

    private const string ChasisKey = "ChasisProperties";

    ChasisData chasisData = new();

    public static ChasisManager Instance { get; private set; }

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

        // Initialize the ChasisManager script
        // (e.g., load values, set properties)

        LoadChasisValue();
    }

    private void Start()
    {
        ApplyChasisProperties();
    }

    // Initialize the chasis with an initial damping ratio
    public void Initialize(float initialDampingRatio)
    {
        veichelMass = initialDampingRatio;
        initialVeichelMass = initialDampingRatio;
    }

    // Upgrade the chasis by adding a damping delta
    public void UpgradeChasis()
    {
        veichelMass += veichelMassDelta;
        ApplyChasisProperties();
        SaveChasisValue();
    }

    // Downgrade the chasis by subtracting a damping delta
    public void DowngradeChasis()
    {
        veichelMass -= veichelMassDelta;
        ApplyChasisProperties();
        SaveChasisValue();
    }

    // Reset the chasis to its initial damping ratio
    public void ResetChasis()
    {
        veichelMass = initialVeichelMass;
        ApplyChasisProperties();
        SaveChasisValue();
    }

    // Set the chasis damping ratio to a specified value
    public void SetChasisData(float veichelMassRatio)
    {
        veichelMass = veichelMassRatio;
        ApplyChasisProperties();
        SaveChasisValue();
    }

    // Apply the chasis properties to the car's front and back wheels
    private void ApplyChasisProperties()
    {
        if (carController != null)
        {
            Rigidbody2D rb = carController.GetComponent<Rigidbody2D>();
            rb.mass = veichelMass;
            carController.rb.mass= veichelMass;

            //Debug.Log("veichelMassRatio: " + veichelMass);
        }
        else
        {
            Debug.LogError("CarController component is missing. Make sure to assign the CarController GameObject to the ChasisManager script in the Inspector.");
        }
    }

    // Save the chasis value using PlayerPrefs
    private void SaveChasisValue()
    {
        chasisData.Mass = veichelMass;
        PlayerPrefs.SetFloat("VeichelMassKey", chasisData.Mass);
        string json = JsonUtility.ToJson(chasisData);
        PlayerPrefs.SetString(ChasisKey, json);
        PlayerPrefs.Save();
    }

    // Load the chasis value from PlayerPrefs
    private void LoadChasisValue()
    {
        if (PlayerPrefs.HasKey(ChasisKey))
        {
            veichelMass = PlayerPrefs.GetFloat("VeichelMassKey");
        }
    }

    [System.Serializable]
    public class ChasisData
    {
        public float Mass { get; set; }
    }
}
