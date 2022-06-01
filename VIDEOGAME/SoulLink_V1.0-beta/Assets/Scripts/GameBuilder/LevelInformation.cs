/*
Code that contains the information of the level so it can be accesed in
other scripts.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInformation : MonoBehaviour
{
    public static string levelRooms;
    public static string levelEnemies;
    public static string levelObstacles;
    public static string levelName;
    public TMP_InputField input;

    public void updateName()
    {
        levelName = input.text;
    }
}
