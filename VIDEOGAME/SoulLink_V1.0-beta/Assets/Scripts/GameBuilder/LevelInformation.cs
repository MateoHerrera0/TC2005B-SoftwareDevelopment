using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInformation : MonoBehaviour
{
    public static string levelRooms;
    public static string levelEnemies;
    public static string levelObstacle;
    public static string levelName;
    public TMP_InputField input;

    public void updateName()
    {
        levelName = input.text;
    }
}
