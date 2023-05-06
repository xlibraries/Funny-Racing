using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private readonly CarController player;
    public bool isSuspension = false;

    public void UpgradeSuspension()
    {
        isSuspension= true;
    }

    public void DowngradeSuspension()
    {
        float dampingRatio = player.frontWheel.suspension.dampingRatio;
        int frequency = (int)player.frontWheel.suspension.frequency;
        dampingRatio -= 0.1f;
        frequency -= 500;
        player.frontWheel.suspension.dampingRatio.Equals(dampingRatio);
        player.frontWheel.suspension.frequency.Equals(frequency);
        player.backWheel.suspension.dampingRatio.Equals(dampingRatio);
        player.backWheel.suspension.frequency.Equals(frequency);
    }
}
