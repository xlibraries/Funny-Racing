using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapticsManager : MonoBehaviour
{
    private bool touchStarted = false;
    private long hapticDurationMillis = 100; // Set the desired haptic feedback duration in milliseconds

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
            StartHapticFeedback();
            touchStarted = true;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            touchStarted = false;
            StopHapticFeedback();
        }
        touchStarted = false;
    }

    private void StartHapticFeedback()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            vibrator.Call("vibrate", hapticDurationMillis);
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    private void StopHapticFeedback()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
            vibrator.Call("cancel");
        }
        else
        {
            // There is no way to stop haptic feedback on other platforms
        }
    }

    public void OnButtonClick()
    {
        Touch dummyTouch = new()
        {
            phase = TouchPhase.Began
        };
        HapticsFeedback(dummyTouch);
    }
}
