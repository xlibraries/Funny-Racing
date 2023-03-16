using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : ScoreManager
{
    [SerializeField]
    private string scenename;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            Debug.Log("Game Won");
            SubmitScore(100);
            SceneManager.LoadScene(scenename);
        }
    }

}
