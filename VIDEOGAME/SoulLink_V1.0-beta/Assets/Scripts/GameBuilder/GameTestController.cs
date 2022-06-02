/*
Code that deciphers string level data, and uses it to create the level.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameTestController : MonoBehaviour
{
    string allLevel;
    string allEnemy;
    string allObstacle;
    public GameObject[] objectList;
    List<string> names = new List<string>();
    List<int> ids = new List<int>();
    List<int> x = new List<int>();
    List<int> y = new List<int>();
    List<float> objectX = new List<float>();
    List<float> objectY = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        allLevel = LevelInformation.levelRooms;
        if (allLevel != null)
        {
            string[] level = allLevel.Split('_');
    
            for (int i = 0; i < level.Length-1; i++)
            {
                string[] splitArray = level[i].Split(',');
                names.Add(splitArray[0]);
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
            }
    
            for (int i = 0; i < names.Count; i++)
            {
                RoomController.instance.LoadRoom(names[i], x[i], y[i]);
            }
        }

        x.Clear();
        y.Clear();
        
        allEnemy = LevelInformation.levelEnemies;

       if (allEnemy != null)
       {
            string[] enemy = allEnemy.Split('_');
    
            for (int i = 0; i < enemy.Length-1; i++)
            {
                string[] splitArray = enemy[i].Split(',');
                ids.Add(int.Parse(splitArray[0]));
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
                objectX.Add(float.Parse(splitArray[3]));
                objectY.Add(float.Parse(splitArray[4]));
            }
            StartCoroutine(WaitTillRoomsAreLoaded());
       }

        allObstacle = LevelInformation.levelObstacles;

       if (allObstacle != null)
       {
            string[] obstacle = allObstacle.Split('_');
    
            for (int i = 0; i < obstacle.Length-1; i++)
            {
                string[] splitArray = obstacle[i].Split(',');
                ids.Add(int.Parse(splitArray[0]));
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
                objectX.Add(float.Parse(splitArray[3]));
                objectY.Add(float.Parse(splitArray[4]));
            }

            StartCoroutine(WaitTillRoomsAreLoaded());
       }
    }

    IEnumerator WaitTillRoomsAreLoaded()
    {
        yield return new WaitUntil(() => RoomController.instance.loadedRooms.Count == names.Count);
        for (int i = 0; i < x.Count; i++)
        {
            GameObject newObject = Instantiate(objectList[ids[i]], new Vector3(objectX[i], objectY[i], 0), Quaternion.identity);
            Room room = RoomController.instance.FindRoom(x[i], y[i]);
            newObject.transform.parent = room.transform;
        }
        ids.Clear();
        x.Clear();
        y.Clear();
        objectX.Clear();
        objectY.Clear();
    }
}
