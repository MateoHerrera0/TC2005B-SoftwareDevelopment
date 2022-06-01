/*
Code that handles get and post requests for created levels.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


// Create classes that correspond to the data that will be sent/received
// via the API

// Allow the class to be extracted from Unity
// https://stackoverflow.com/questions/40633388/show-members-of-a-class-in-unity3d-inspector
[System.Serializable]
public class User
{
    public int usernameID;
    public string username;
    public string pwd;
    public string email;
    public int gameStatisticsID;
    public int builderStatisticsID;
}

// Allow the class to be extracted from Unity
[System.Serializable]
public class UserList
{
    public List<User> levels;
}

public class UsernameSelect : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] string getUsersEP;
    [SerializeField] InputField inputUsername;
    [SerializeField] Input inputPassword;

    // This is where the information from the api will be extracted
    public UserList allUsers;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     QueryLevel();
        // }
        // if (Input.GetKeyDown(KeyCode.N)) {
        //     InsertNewUser();
        // }
    }

    // These are the functions that must be called to interact with the API

    public void QueryUser()
    {
        StartCoroutine(GetUser());
    }

    // These functions make the connection to the API

    IEnumerator GetUser()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + getUsersEP + inputUsername.text))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"users\":" + www.downloadHandler.text + "}";
                allUsers = JsonUtility.FromJson<UserList>(jsonString);
                Debug.Log(jsonString);
                if (allUsers != null)
                {
                    login();
                }
            } else {
                notLogin();
            }
        }
    }

    // IEnumerator AddLevel()
    // {
    //     /*
    //     // This should work with an API that does NOT expect JSON
    //     WWWForm form = new WWWForm();
    //     form.AddField("name", "newGuy" + Random.Range(1000, 9000).ToString());
    //     form.AddField("surname", "Tester" + Random.Range(1000, 9000).ToString());
    //     Debug.Log(form);
    //     */

    //     // Create the object to be sent as json
    //     Level newLevel = new Level();
    //     newLevel.levelName = LevelInformation.levelName;
    //     newLevel.roomLayout = LevelInformation.levelRooms;
    //     newLevel.enemyLayout = LevelInformation.levelEnemies;
    //     newLevel.objectLayout = LevelInformation.levelObstacles;
    //     newLevel.usernameID = 1;
    //     //Debug.Log("USER: " + newLevel);
    //     string jsonData = JsonUtility.ToJson(newLevel);
    //     //Debug.Log("BODY: " + jsonData);

    //     // Send using the Put method:
    //     // https://stackoverflow.com/questions/68156230/unitywebrequest-post-not-sending-body
    //     using (UnityWebRequest www = UnityWebRequest.Put(url + getUsersEP, jsonData))
    //     {
    //         //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
    //         // Set the method later, and indicate the encoding is JSON
    //         www.method = "POST";
    //         www.SetRequestHeader("Content-Type", "application/json");
    //         yield return www.SendWebRequest();

    //         if (www.result == UnityWebRequest.Result.Success) {
    //             Debug.Log("Response: " + www.downloadHandler.text);
    //         } else {
    //             Debug.Log("Error: " + www.error);
    //         }
    //     }
    // }

    void login()
    {
        Debug.Log("loggedin");
    }

    void notLogin()
    {
        Debug.Log("notlogged");
    }
}
