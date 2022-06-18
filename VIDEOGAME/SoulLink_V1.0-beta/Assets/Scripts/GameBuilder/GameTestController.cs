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
    // String that contains all room data
    string allLevel;
    // String that contains all enemy data
    string allEnemy;
    // String that contains all obstacle data
    string allObstacle;
    // Array of placeable game objects
    public GameObject[] objectList;
    // String list that contains object names
    List<string> names = new List<string>();
    // String list that contains object ids
    List<int> ids = new List<int>();
    // String list that contains room x
    List<int> x = new List<int>();
    // String list that contains room y
    List<int> y = new List<int>();
    // String list that contains object x
    List<float> objectX = new List<float>();
    // String list that contains object y
    List<float> objectY = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        // All room info is called
        allLevel = LevelInformation.levelRooms;
        // Checks if level data is null first
        if (allLevel != null)
        {
            string[] level = allLevel.Split('_');

            // Room data is parsed and added to corresponding lists
            for (int i = 0; i < level.Length-1; i++)
            {
                string[] splitArray = level[i].Split(',');
                names.Add(splitArray[0]);
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
            }

            // Rooms are loaded through room controller methods
            for (int i = 0; i < names.Count; i++)
            {
                RoomController.instance.LoadRoom(names[i], x[i], y[i]);
            }
        }

        x.Clear();
        y.Clear();
        
        // All enemy info is called
        allEnemy = LevelInformation.levelEnemies;

        //All enemy info is checked first 
       if (allEnemy != null)
       {
            string[] enemy = allEnemy.Split('_');

            // Enemy info is parsed and stored in corresponding lists
            for (int i = 0; i < enemy.Length-1; i++)
            {
                string[] splitArray = enemy[i].Split(',');
                ids.Add(int.Parse(splitArray[0]));
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
                objectX.Add(float.Parse(splitArray[3]));
                objectY.Add(float.Parse(splitArray[4]));
            }
            // Coroutine is called
            StartCoroutine(WaitTillRoomsAreLoaded());
       }

        // All obstacle info is called
        allObstacle = LevelInformation.levelObstacles;

        //All obstacle info is checked first 
       if (allObstacle != null)
       {
            string[] obstacle = allObstacle.Split('_');

            // Obstacle info is parsed and stored in corresponding lists
            for (int i = 0; i < obstacle.Length-1; i++)
            {
                string[] splitArray = obstacle[i].Split(',');
                ids.Add(int.Parse(splitArray[0]));
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
                objectX.Add(float.Parse(splitArray[3]));
                objectY.Add(float.Parse(splitArray[4]));
            }

            // Coroutine is called
            StartCoroutine(WaitTillRoomsAreLoaded());
       }
    }

    // Coroutine that checks if rooms are already loaded before objects are set
    // as children
    IEnumerator WaitTillRoomsAreLoaded()
    {
        yield return new WaitUntil(() => RoomController.instance.loadedRooms.Count == names.Count);
        // When rooms are all loaded, objects are intantiated and set as 
        // children
        for (int i = 0; i < x.Count; i++)
        {
            GameObject newObject = Instantiate(objectList[ids[i]], new Vector3(objectX[i], objectY[i], 0), Quaternion.identity);
            Room room = RoomController.instance.FindRoom(x[i], y[i]);
            newObject.transform.parent = room.transform;
        }
        // Everything is cleared
        ids.Clear();
        x.Clear();
        y.Clear();
        objectX.Clear();
        objectY.Clear();
    }
}
