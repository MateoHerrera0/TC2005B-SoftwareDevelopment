/*
Code that handles get and post requests for users

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


// Create classes that correspond to the data that will be sent/received
// via the API

// Allow the class to be extracted from Unity
// https://stackoverflow.com/questions/40633388/show-members-of-a-class-in-unity3d-inspector
[System.Serializable]
public class User
{
    public int usernameID;
    // public string username;
    // public string pwd;
    // public string email;
    // public int gameStatisticsID;
    // public int builderStatisticsID;
}

public class NewUser
{
    public int usernameID;
    public string username;
    public string pwd;
    public string email;
}

// Allow the class to be extracted from Unity
[System.Serializable]
public class UserList
{
    public List<User> users;
}

public class UsernameSelect : MonoBehaviour
{
    // String of loalhost url
    [SerializeField] string url;
    // Users endpoint
    [SerializeField] string getUsersEP;
    // Input field of username of login panel
    [SerializeField] InputField loginUsername;
    // Input field of password of login panel
    [SerializeField] InputField loginPassword;
    // Text where error can appear in login panel
    [SerializeField] Text loginError;
    // Input field of username of sign up panel
    [SerializeField] InputField signUpUsername;
    // Input field of password of sign up panel
    [SerializeField] InputField signUpPassword;
    // Input field of email of sign up panel
    [SerializeField] InputField signUpEmail;
    // Text where error can appear in sign up panel
    [SerializeField] Text signUpError;
    // Variable that stores sign up panel
    [SerializeField] GameObject signUpPanel;
    // Variable that stores login panel
    [SerializeField] GameObject loginPanel;
    // Variable that stores menu controller
    [SerializeField] MenuController menu;
    // Variable that stores Player Statistics
    [SerializeField] PlayerStatistics playerStatistics;
    // Bool that checks if sign up panel is active
    bool signUpState = false;

    // This is where the information from the api will be extracted
    public UserList allUsers;

    // These are the functions that must be called to interact with the API
    public void QueryUser()
    {
        StartCoroutine(GetUser());
    }

    public void InsertNewUser()
    {
        StartCoroutine(AddUser());
    }

    public void DeleteUser()
    {
        StartCoroutine(StartDeleteProcedure());
    }

    // Function that toggles sign up panel
    public void ToggleSignUp()
    {
        if (signUpState)
        {
            signUpPanel.SetActive(false);
            loginPanel.SetActive(true);
            signUpState = false;
            return;
        }

        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);
        signUpState = true;
    }

    // These functions make the connection to the API
    IEnumerator GetUser()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + getUsersEP + loginUsername.text + "/" + loginPassword.text))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"users\":" + www.downloadHandler.text + "}";
                allUsers = JsonUtility.FromJson<UserList>(jsonString);
                if (allUsers.users.Count > 0)
                {
                    login(allUsers.users[0]);
                } else
                {
                    notLogin(www.downloadHandler.text);
                }
            } else {
                notLogin(www.error);
            }
        }
    }

    IEnumerator StartDeleteProcedure()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + getUsersEP + PlayerPrefs.GetInt("userID").ToString()))
        {   
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                //Debug.Log("Response: " + www.downloadHandler.text);
                // Compose the response to look like the object we want to extract
                // https://answers.unity.com/questions/1503047/json-must-represent-an-object-type.html
                string jsonString = "{\"status\":" + www.downloadHandler.text + "}";
                Debug.Log(jsonString);
                menu.Logout();
            } else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    IEnumerator AddUser()
    {
        /*
        // This should work with an API that does NOT expect JSON
        WWWForm form = new WWWForm();
        form.AddField("name", "newGuy" + Random.Range(1000, 9000).ToString());
        form.AddField("surname", "Tester" + Random.Range(1000, 9000).ToString());
        Debug.Log(form);
        */

        // Create the object to be sent as json
        NewUser newUser = new NewUser();
        newUser.username = signUpUsername.text;
        newUser.email = signUpEmail.text;
        newUser.pwd = signUpPassword.text;
        //Debug.Log("USER: " + newUser);
        string jsonData = JsonUtility.ToJson(newUser);
        //Debug.Log("BODY: " + jsonData);

        // Send using the Put method:
        // https://stackoverflow.com/questions/68156230/unitywebrequest-post-not-sending-body
        using (UnityWebRequest www = UnityWebRequest.Put(url + getUsersEP, jsonData))
        {
            //UnityWebRequest www = UnityWebRequest.Post(url + getUsersEP, form);
            // Set the method later, and indicate the encoding is JSON
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) {
                loginUsername.text = signUpUsername.text;
                loginPassword.text = signUpPassword.text;
                QueryUser();
            } else {
                notLogin(www.downloadHandler.text);
            }
        }
    }

    // Function that stores player id in player prefs and changes scene to 
    // Main Menu
    void login(User userID)
    {
            PlayerPrefs.SetInt("userID", userID.usernameID);
            playerStatistics.UpdatePlayerStatistics(true);
            SceneManager.LoadScene("MainMenu");
    }

    // Function that prints error
    void notLogin(string whyNot)
    {
        if (signUpState)
        {
            signUpError.text = whyNot;
            return;
        }
        loginError.text = whyNot;
    }
}
