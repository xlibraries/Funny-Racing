using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionManager : MonoBehaviour
{
    public CarController carController; // Reference to the CarController GameObject

    private float frontWheelDampingRatio;
    private float initialFrontWheelDampingRatio;
    private const string SuspensionKey = "SuspensionDampingRatio";

    public static SuspensionManager Instance { get; private set; }

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
    public void Initialize(float initialDampingRatio)
    {
        frontWheelDampingRatio = initialDampingRatio;
        initialFrontWheelDampingRatio = initialDampingRatio;
    }

    // Upgrade the suspension by adding a damping delta
    public void UpgradeSuspension(float dampingDelta)
    {
        frontWheelDampingRatio += dampingDelta;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Downgrade the suspension by subtracting a damping delta
    public void DowngradeSuspension(float dampingDelta)
    {
        frontWheelDampingRatio -= dampingDelta;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Reset the suspension to its initial damping ratio
    public void ResetSuspension()
    {
        frontWheelDampingRatio = initialFrontWheelDampingRatio;
        ApplySuspensionProperties();
        SaveSuspensionValue();
    }

    // Reset the suspension damping ratio to a specified value
    public void ResetDampingRatio(float dampingRatio)
    {
        frontWheelDampingRatio = dampingRatio;
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
            carController.frontWheel.suspension = frontSuspension;
            carController.backWheel.suspension = frontSuspension;

            Debug.Log("dampingRatio: " + frontWheelDampingRatio);
        }
        else
        {
            Debug.LogError("CarController component is missing. Make sure to assign the CarController GameObject to the SuspensionManager script in the Inspector.");
        }
    }

    // Save the suspension value using PlayerPrefs
    private void SaveSuspensionValue()
    {
        PlayerPrefs.SetFloat(SuspensionKey, frontWheelDampingRatio);
        PlayerPrefs.Save();
    }

    // Load the suspension value from PlayerPrefs
    private void LoadSuspensionValue()
    {
        if (PlayerPrefs.HasKey(SuspensionKey))
        {
            frontWheelDampingRatio = PlayerPrefs.GetFloat(SuspensionKey);
        }
    }
}
