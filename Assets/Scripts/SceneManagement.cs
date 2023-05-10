using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SceneManagement : MonoBehaviour
{
    public string sceneName;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnResetbuttomClick()
    {
        SuspensionManager.Instance.ResetDampingRatio(0.1f);
    }
}
