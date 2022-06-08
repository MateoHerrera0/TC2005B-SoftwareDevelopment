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
public class BuilderStatistics
{
    public int demonEnemy;
    public int regularEnemy;
    public int dragonEnemy;
    public int goblinEnemy;
    public int muddyEnemy;
    public int zombieEnemy;
    public int boxObstacle;
    public int floorSpikesObstacle;
    public int holeObject;
    public int ogreBoss;
    public int zombieBoss;
    public int usernameID;
}

// Allow the class to be extracted from Unity
[System.Serializable]
public class BuilderStatisticsList
{
    public List<BuilderStatistics> levels;
}
public class GameBuilderStatistics : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string updateStatisticsEp;
    [SerializeField] GameBuilderController editor;

    // This is where the information from the api will be extracted
    public BuilderStatisticsList allStatistics;

    // These are the functions that must be called to interact with the API

    public void UpdateStatistics()
    {
        
        StartCoroutine(PutStatistic());
    }

    // These functions make the connection to the API

    IEnumerator PutStatistic()
    {
        /*
        // This should work with an API that does NOT expect JSON
        WWWForm form = new WWWForm();
        form.AddField("name", "newGuy" + Random.Range(1000, 9000).ToString());
        form.AddField("surname", "Tester" + Random.Range(1000, 9000).ToString());
        Debug.Log(form);
        */

        // Create the object to be sent as json
        BuilderStatistics newStatistic = new BuilderStatistics();
        for (int i = 0; i < editor.enemiesToBePlaced.Count; i++)
        {
            switch (editor.enemiesToBePlaced[i].name)
            {
                case "0":
                    newStatistic.regularEnemy += 1;
                    break;
                case "1":
                    newStatistic.dragonEnemy += 1;
                    break;
                case "2":
                    newStatistic.demonEnemy += 1;
                    break;
                case "3":
                    newStatistic.goblinEnemy += 1;
                    break;
                case "4":
                    newStatistic.muddyEnemy += 1;
                    break;
                case "5":
                    newStatistic.zombieEnemy += 1;
                    break;
                case "9":
                    newStatistic.ogreBoss += 1;
                    break;
                case "10":
                    newStatistic.zombieBoss += 1;
                    break;
                default:
                    break;
            }
        }

        for (int i = 0; i < editor.obstaclesToBePlaced.Count; i++)
        {
            switch (editor.obstaclesToBePlaced[i].name)
            {
                case "6":
                    newStatistic.floorSpikesObstacle += 1;
                    break;
                case "7":
                    newStatistic.holeObject += 1;
                    break;
                case "8":
                    newStatistic.boxObstacle += 1;
                    break;
                default:
                    break;
            }
        }
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