using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionManager : MonoBehaviour
{
    public CarController carController; // Reference to the CarController GameObject

    private float frontWheelDampingRatio;
    private float initialFrontWheelDampingRatio;

    public void Initialize(float initialDampingRatio)
    {
        frontWheelDampingRatio = initialDampingRatio;
        initialFrontWheelDampingRatio = initialDampingRatio;
    }

    public void UpgradeSuspension(float dampingDelta)
    {
        frontWheelDampingRatio += dampingDelta;
        ApplySuspensionProperties();
    }

    public void DowngradeSuspension(float dampingDelta)
    {
        frontWheelDampingRatio -= dampingDelta;
        ApplySuspensionProperties();
    }

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


    public void ResetSuspension()
    {
        frontWheelDampingRatio = initialFrontWheelDampingRatio;
        ApplySuspensionProperties();
    }
}
