using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticsManager : MonoBehaviour
{
    private bool touchStarted = false;


    private static HapticsManager instance;
    public static HapticsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HapticsManager>();
            }
            return instance;
        }
    }

    public void HapticsFeedback(Touch touch)
    {
        if (touch.phase == TouchPhase.Began && !touchStarted)
        {
            // Trigger haptic feedback when touch begins
            Handheld.Vibrate();
            touchStarted = true;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            touchStarted = false;
        }
    }
}
