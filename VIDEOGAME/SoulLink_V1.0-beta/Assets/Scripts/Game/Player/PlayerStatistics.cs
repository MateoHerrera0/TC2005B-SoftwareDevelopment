/*
Code that handles get and post requests for created levels.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


// Create classes that correspond to the data that will be sent/received
// via the API

// Allow the class to be extracted from Unity
// https://stackoverflow.com/questions/40633388/show-members-of-a-class-in-unity3d-inspector
[System.Serializable]
public class newPlayerStatistics
{
    public bool activeBool;
    public int usernameID;
}
public class PlayerStatistics : MonoBehaviour
{
      [SerializeField] string url;
    [SerializeField] string updateStatisticsEp;

    // These are the functions that must be called to interact with the API

    public void UpdatePlayerStatistics(bool activeBool)
    {
        
        StartCoroutine(PutPlayerStatistic(activeBool));
    }

    // These functions make the connection to the API

    IEnumerator PutPlayerStatistic(bool activeBool)
    {
        /*
        // This should work with an API that does NOT expect JSON
        WWWForm form = new WWWForm();
        form.AddField("name", "newGuy" + Random.Range(1000, 9000).ToString());
        form.AddField("surname", "Tester" + Random.Range(1000, 9000).ToString());
        Debug.Log(form);
        */

        // Create the object to be sent as json
        newPlayerStatistics newStatistic = new newPlayerStatistics();
        
        newStatistic.activeBool = activeBool;
        newStatistic.usernameID = PlayerPrefs.GetInt("userID");
        //Debug.Log("USER: " + newStatistic);
        string jsonData = JsonUtility.ToJson(newStatistic);
        //Debug.Log("BODY: " + jsonData);

        // Send using the Put method:
        // https://stackoverflow.com/questions/68156230/unitywebrequest-post-not-sending-body
        using (UnityWebRequest www = UnityWebRequest.Put(url + updateStatisticsEp, jsonData))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + updateStatisticsEp, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "PUT";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                Debug.Log("Response: " + www.downloadHandler.text);
            } else {
                Debug.Log("Error: " + www.error);
            }
        }
    }
}
