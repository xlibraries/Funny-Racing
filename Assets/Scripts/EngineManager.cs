using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    public CarController carController; // Reference to the CarController GameObject

    private float frontWheelDampingRatio;
    private float initialFrontWheelDampingRatio = 0.1f;

    private int frontWheeelFrequency;
    private int initialFrontWheelfrequency = 4;

    public float dampingDelta = 0.1f;
    public int frequencyDelta = 400;

    private const string SuspensionKey = "SuspensionProperties";

    SuspensionData suspensionData = new();

    public static EngineManager Instance { get; private set; }

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

        LoadSuspensionValue();
    }

    private void Start()
    {
        ApplySuspensionProperties();
    }

    // Initialize the suspension with an initial damping ratio
    public void Initialize(float initialDampingRatio, int initialFrequency)
    {
        frontWheelDampingRatio = initialDampingRatio;
        initialFrontWheelDampingRatio = initialDampingRatio;
        frontWheeelFrequency = initialFrequency;
        initialFrontWheelfrequency = initialFrequency;
    }

    // Upgrade the suspension by adding a damping delta
    public void UpgradeSuspension()
    {
        frontWheelDampingRatio += dampingDelta;
        frontWheeelFrequency += frequencyDelta;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Downgrade the suspension by subtracting a damping delta
    public void DowngradeSuspension()
    {
        frontWheelDampingRatio -= dampingDelta;
        frontWheeelFrequency -= frequencyDelta;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Reset the suspension to its initial damping ratio
    public void ResetSuspension()
    {
        frontWheelDampingRatio = initialFrontWheelDampingRatio;
        frontWheeelFrequency = initialFrontWheelfrequency;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Set the suspension damping ratio to a specified value
    public void SetSuspensionData(float dampingRatio, int frequency)
    {
        frontWheelDampingRatio = dampingRatio;
        frontWheeelFrequency = frequency;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Apply the suspension properties to the car's front and back wheels
    private void ApplySuspensionProperties()
    {
        if (carController != null)
        {
            JointSuspension2D frontSuspension = carController.frontWheel.suspension;
            frontSuspension.dampingRatio = frontWheelDampingRatio;
            frontSuspension.frequency = frontWheeelFrequency;
            carController.frontWheel.suspension = frontSuspension;
            carController.backWheel.suspension = frontSuspension;

            Debug.Log("dampingRatio: " + frontWheelDampingRatio);
            Debug.Log("Frequency: " + frontWheeelFrequency);
        }
        else
        {
            Debug.LogError("CarController component is missing. Make sure to assign the CarController GameObject to the SuspensionManager script in the Inspector.");
        }
    }

    // Save the suspension value using PlayerPrefs
    private void SaveSuspensionValue()
    {
        suspensionData.DampingRatio = frontWheelDampingRatio;
        suspensionData.Frequency = frontWheeelFrequency;
        PlayerPrefs.SetFloat("DamingRatioKey", suspensionData.DampingRatio);
        PlayerPrefs.SetInt("FrequencyKey", suspensionData.Frequency);
        string json = JsonUtility.ToJson(suspensionData);
        PlayerPrefs.SetString(SuspensionKey, json);
        PlayerPrefs.Save();
    }

    // Load the suspension value from PlayerPrefs
    private void LoadSuspensionValue()
    {
        if (PlayerPrefs.HasKey(SuspensionKey))
        {
            frontWheelDampingRatio = PlayerPrefs.GetFloat("DamingRatioKey");
            frontWheeelFrequency = PlayerPrefs.GetInt("FrequencyKey");
        }
    }

    [System.Serializable]
    public class SuspensionData
    {
        public int Frequency { get; set; }
        public float DampingRatio { get; set; }
    }
}
