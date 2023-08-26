//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.Networking;

//public class ApiManager : MonoBehaviour
//{
//    private const string baseUrl = "https://your-backend-url.com";

//    public IEnumerator RegisterPlayer(string playerName, string password, Action<bool> callback)
//    {
//        string url = baseUrl + "/register";
//        WWWForm form = new WWWForm();
//        form.AddField("playerName", playerName);
//        form.AddField("password", password);

//        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.LogError("Registration error: " + www.error);
//                callback?.Invoke(false);
//            }
//            else
//            {
//                Debug.Log("Registration successful!");
//                callback?.Invoke(true);
//            }
//        }
//    }

//    public IEnumerator LoginPlayer(string playerName, string password, Action<bool> callback)
//    {
//        string url = baseUrl + "/login";
//        WWWForm form = new WWWForm();
//        form.AddField("playerName", playerName);
//        form.AddField("password", password);

//        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.LogError("Login error: " + www.error);
//                callback?.Invoke(false);
//            }
//            else
//            {
//                Debug.Log("Login successful!");
//                callback?.Invoke(true);
//            }
//        }
//    }

//    public IEnumerator SubmitScore(string playerName, int score, Action<bool> callback)
//    {
//        string url = baseUrl + "/submit-score";
//        WWWForm form = new WWWForm();
//        form.AddField("playerName", playerName);
//        form.AddField("score", score.ToString());

//        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.LogError("Score submission error: " + www.error);
//                callback?.Invoke(false);
//            }
//            else
//            {
//                Debug.Log("Score submitted!");
//                callback?.Invoke(true);
//            }
//        }
//    }

//    public IEnumerator GetHighScores(Action<string> callback)
//    {
//        string url = baseUrl + "/high-scores";

//        using (UnityWebRequest www = UnityWebRequest.Get(url))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.LogError("High score retrieval error: " + www.error);
//            }
//            else
//            {
//                callback?.Invoke(www.downloadHandler.text);
//            }
//        }
//    }
//}
