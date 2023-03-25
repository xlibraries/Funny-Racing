using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : ScoreManager
{
    [SerializeField]
    private string scenename;

    private void Update()
    {
        if ((GameManager.fuelPresent <= 0) || GameManager.isGrounded)
        {
            SubmitScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            SubmitScore();
        }
    }

    public void SubmitScore()
    {
        Debug.Log("Game Won");
        SubmitScore((int)GameManager.distanceCovered);
        SceneManager.LoadScene(scenename);
    }
}