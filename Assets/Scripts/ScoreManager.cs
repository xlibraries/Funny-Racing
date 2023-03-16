using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SubmitScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "HighScore",
                Value = playerScore
            }

        }
        }, result => OnStatisticsUpdated(result), FailureCallback);
    }

    private void OnStatisticsUpdated(UpdatePlayerStatisticsResult updateResult)
    {
        Debug.Log("Successfully submitted high score");
    }

    private void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    ////Get the players with the top 10 high scores in the game
    //public void RequestLeaderboard()
    //{
    //    PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
    //    {
    //        StatisticName = "HighScore",
    //        StartPosition = 0,
    //        MaxResultsCount = 10
    //    }, result => DisplayLeaderboard(result), FailureCallback);
    //}

}
