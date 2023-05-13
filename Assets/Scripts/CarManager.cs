using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private CarController player;

    private void Awake()
    {
        player = GetComponentInParent<CarController>();
        if (player == null)
        {
            Debug.LogError("CarController component is missing on the CarManager's parent GameObject.");
        }
    }


    private float FrontWheelDampingRatio
    {
        get => player.frontWheel.suspension.dampingRatio;
        set
        {
            JointSuspension2D suspension = player.frontWheel.suspension;
            suspension.dampingRatio = value;
            player.frontWheel.suspension = suspension;
            player.backWheel.suspension = suspension;
        }
    }

    public void UpgradeSuspension()
    {
        AdjustSuspension(-0.1f, -500);
    }

    public void DowngradeSuspension()
    {
        AdjustSuspension(0.1f, 500);
    }

    private void AdjustSuspension(float dampingDelta, int frequencyDelta)
    {
        float newDampingRatio = FrontWheelDampingRatio + dampingDelta;
        int newFrequency = (int)player.frontWheel.suspension.frequency + frequencyDelta;

        FrontWheelDampingRatio = newDampingRatio;
        JointSuspension2D suspension = player.frontWheel.suspension;
        suspension.frequency = newFrequency;
        player.frontWheel.suspension = suspension;
        player.backWheel.suspension = suspension;

        Debug.Log("dampingRatio: " + newDampingRatio);
        Debug.Log("frequency: " + newFrequency);
    }
}

