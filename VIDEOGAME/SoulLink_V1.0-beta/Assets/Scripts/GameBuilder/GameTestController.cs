using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameTestController : MonoBehaviour
{
<<<<<<< Updated upstream
    string pathLevel;
    string pathEnemy;
    string[] allInfo;
=======
    string allLevel;
    string allEnemy;
>>>>>>> Stashed changes
    public GameObject[] enemyList;
    List<string> names = new List<string>();
    List<int> ids = new List<int>();
    List<int> x = new List<int>();
    List<int> y = new List<int>();
    List<float> enemyX = new List<float>();
    List<float> enemyY = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        pathLevel = Application.dataPath + "/Level.txt";

        allInfo = File.ReadAllLines(pathLevel);

        foreach (string level in allInfo)
        {
            string[] splitArray = level.Split(',');
            names.Add(splitArray[0]);
            x.Add(int.Parse(splitArray[1]));
            y.Add(int.Parse(splitArray[2]));
        }

        for (int i = 0; i < names.Count; i++)
        {
            RoomController.instance.LoadRoom(names[i], x[i], y[i]);
=======

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
>>>>>>> Stashed changes
        }

        x.Clear();
        y.Clear();

<<<<<<< Updated upstream
        pathEnemy = Application.dataPath + "/Enemy.txt";
        allInfo = File.ReadAllLines(pathEnemy);

        foreach (string enemy in allInfo)
        {
            string[] splitArray = enemy.Split(',');
            ids.Add(int.Parse(splitArray[0]));
            x.Add(int.Parse(splitArray[1]));
            y.Add(int.Parse(splitArray[2]));
            enemyX.Add(float.Parse(splitArray[3]));
            enemyY.Add(float.Parse(splitArray[4]));
        }
=======
        allEnemy = LevelInformation.levelEnemies;
>>>>>>> Stashed changes

       if (allEnemy != null)
       {
            string[] enemy = allEnemy.Split('_');
    
            for (int i = 0; i < enemy.Length-1; i++)
            {
                string[] splitArray = enemy[i].Split(',');
                ids.Add(int.Parse(splitArray[0]));
                x.Add(int.Parse(splitArray[1]));
                y.Add(int.Parse(splitArray[2]));
                enemyX.Add(float.Parse(splitArray[3]));
                enemyY.Add(float.Parse(splitArray[4]));
            }
    
            StartCoroutine(WaitTillRoomsAreLoaded());
       }
    }

    IEnumerator WaitTillRoomsAreLoaded()
    {
        yield return new WaitUntil(() => RoomController.instance.loadedRooms.Count == names.Count);
        for (int i = 0; i < x.Count; i++)
        {
            GameObject enemy = Instantiate(enemyList[ids[i]], new Vector3(enemyX[i], enemyY[i], 0), Quaternion.identity);
            Room room = RoomController.instance.FindRoom(x[i], y[i]);
            enemy.transform.parent = room.transform;
        }
    }
}
