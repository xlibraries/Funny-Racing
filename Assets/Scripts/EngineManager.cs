using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    public CarController carController; // Reference to the CarController GameObject

    [HideInInspector] public float frontWheelTorque;
    private float initialFrontWheelTorque = 500;

    [HideInInspector] public float frontWheeelPower;
    private float initialFrontWheelPower = 400;

    public float torqueDelta = 7;
    public float powerDelta = 1.5f;

    private const string EngineKey = "EngineProperties";

    EngineData engineData = new();

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

        // Initialize the EngineManager script
        // (e.g., load values, set properties)

        LoadEngineValue();
    }

    private void Start()
    {
        ApplyEngineProperties();
    }

    // Initialize the engine with an initial torque 
    public void Initialize(int initialTorque, int initialPower)
    {
        frontWheelTorque = initialTorque;
        initialFrontWheelTorque = initialTorque;
        frontWheeelPower = initialPower;
        initialFrontWheelPower = initialPower;
    }

    // Upgrade the engine by adding a torque delta
    public void UpgradeEngine()
    {
        frontWheelTorque *= torqueDelta;
        frontWheeelPower *= powerDelta;
        ApplyEngineProperties();
        SaveEngineValue();
    }

    // Downgrade the engine by subtracting a torque delta
    public void DowngradeEngine()
    {
        frontWheelTorque -= torqueDelta;
        frontWheeelPower -= powerDelta;
        ApplyEngineProperties();
        SaveEngineValue();
    }

    // Reset the engine to its initial torque 
    public void ResetEngine()
    {
        frontWheelTorque = initialFrontWheelTorque;
        frontWheeelPower = initialFrontWheelPower;
        ApplyEngineProperties();
        SaveEngineValue();
    }

    // Set the engine torque to a specified value
    public void SetEngineData(int torque, int power)
    {
        frontWheelTorque = torque;
        frontWheeelPower = power;
        ApplyEngineProperties();
        SaveEngineValue();
    }

    // Apply the engine properties to the car's front and back wheels
    private void ApplyEngineProperties()
    {
        if (carController != null)
        {
            JointMotor2D frontEngine = carController.frontWheel.motor;
            frontEngine.maxMotorTorque = frontWheelTorque;
            carController.speed = frontWheeelPower;
            carController.frontWheel.motor = frontEngine;
            carController.backWheel.motor = frontEngine;

            //Debug.Log("Torque: " + frontWheelTorque);
            //Debug.Log("Power: " + frontWheeelPower);
        }
        else
        {
            Debug.LogError("CarController component is missing. Make sure to assign the CarController GameObject to the EngineManager script in the Inspector.");
        }
    }

    // Save the engine value using PlayerPrefs
    private void SaveEngineValue()
    {
        engineData.MaxTorque = (int)frontWheelTorque;
        engineData.MaxSpeed = frontWheeelPower;
        PlayerPrefs.SetInt("TorqueKey", engineData.MaxTorque);
        PlayerPrefs.SetFloat("PowerKey", engineData.MaxSpeed);
        string json = JsonUtility.ToJson(engineData);
        PlayerPrefs.SetString(EngineKey, json);
        PlayerPrefs.Save();
    }

    // Load the engine value from PlayerPrefs
    private void LoadEngineValue()
    {
        if (PlayerPrefs.HasKey(EngineKey))
        {
            frontWheelTorque = PlayerPrefs.GetInt("TorqueKey");
            frontWheeelPower = PlayerPrefs.GetFloat("PowerKey");
        }
    }

    [System.Serializable]
    public class EngineData
    {
        public float MaxSpeed { get; set; }
        public int MaxTorque { get; set; }
    }
}
