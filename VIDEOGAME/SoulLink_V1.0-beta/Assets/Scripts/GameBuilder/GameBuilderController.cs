using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomToBePlaced
{
    public string name;
    public int X;
    public int Y;
}

public class EnemyToBePlaced
{
    public string name;
    public int roomX;
    public int roomY;
    public float X;
    public float Y;
}
public class GameBuilderController : MonoBehaviour
{
    public ItemController[] itemButtons;
    public GameObject[] itemList;
    public GameObject[] itemImage;
    public GameObject emptyParent;
    public List<RoomToBePlaced> roomsToBePlaced = new List<RoomToBePlaced>();
    public List<EnemyToBePlaced> enemiesToBePlaced = new List<EnemyToBePlaced>();
    private FollowMouseBuilder mouse;
    public int currentButtonPressed;
    public string currentButtonPressedName;
    public Vector2 gridSize;
    GameObject roomButtons;
    public GameObject enemyButtons;
    int itemImageX;
    int itemImageY;
    public bool enemyPlaceState = false;

    // Start is called before the first frame update
    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameBuilderController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        roomButtons = GameObject.FindGameObjectWithTag("RoomButtons");
        RoomToBePlaced startRoom = new RoomToBePlaced();
        startRoom.name = "Start";
        startRoom.X = 0;
        startRoom.Y = 0;
        roomsToBePlaced.Add(startRoom);

    }

    private void Update() {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 currentPosOnGrid = SnapToGrid(worldPos);

        int roomPosX = (int)(currentPosOnGrid.x/17.5);
        int roomPosY = (int)(currentPosOnGrid.y/9);

        if (Input.GetMouseButtonDown(0) && itemButtons[currentButtonPressed].clicked)
        {
            itemButtons[currentButtonPressed].clicked = false;

            if (!enemyPlaceState)
            {
                if (DoesRoomExist(roomPosX, roomPosY) == false)
                {
                    RoomToBePlaced room = new RoomToBePlaced();
                    room.name = currentButtonPressedName;
                    room.X = roomPosX;
                    room.Y = roomPosY;
                    
                    roomsToBePlaced.Add(room);
    
                    GameObject newRoom = Instantiate(itemList[currentButtonPressed], new Vector3(currentPosOnGrid.x, currentPosOnGrid.y, 0), Quaternion.identity);
                    newRoom.transform.parent = emptyParent.transform;
                }
            } else
            {
                EnemyToBePlaced enemy = new EnemyToBePlaced();
                enemy.name = currentButtonPressedName;
                enemy.roomX = roomPosX;
                enemy.roomY = roomPosY;
                enemy.X = worldPos.x;
                enemy.Y = worldPos.y;

                enemiesToBePlaced.Add(enemy);

                GameObject newEnemy = Instantiate(itemList[currentButtonPressed], new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
                newEnemy.transform.parent = emptyParent.transform;
            }
            
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));

        }

        if (Input.GetButtonDown("Cancel"))
        {
            ToggleEnemyPlaceState(0, 0);
        }
    }

    public void ToggleGameBuilder()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "GameBuiler")
        {
            if (enemyPlaceState)
            {
                ToggleEnemyPlaceState(0, 0);
            }
            CreateTxtFile();
            roomButtons.SetActive(false);
            emptyParent.SetActive(false);
            SceneManager.LoadScene("BasementMain");
        } 
        else
        {
            roomButtons.SetActive(true);
            emptyParent.SetActive(true);
            SceneManager.LoadScene("GameBuiler");
        }
    }

    public void ToggleEnemyPlaceState(float posX, float posY)
    {
        if (!enemyPlaceState)
        {
            Camera.main.transform.position = new Vector3(posX, posY, -10);
            Camera.main.orthographicSize = 5;
            roomButtons.SetActive(false);
            enemyButtons.SetActive(true);
            enemyPlaceState = true;
            return;
        }

        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = 60;
        roomButtons.SetActive(true);
        enemyButtons.SetActive(false);
        enemyPlaceState = false;
    }

    public Vector2 SnapToGrid(Vector2 originalPos)
    {
        Vector2 newPosition = new Vector2
        (
            Mathf.Round(originalPos.x / gridSize.x) * gridSize.x,
            Mathf.Round(originalPos.y / gridSize.y) * gridSize.y
        );

        return newPosition;
    }

    void CreateTxtFile(){
        string path = Application.dataPath + "/Level.txt";
        string levelInfo = "";
        foreach (RoomToBePlaced room in roomsToBePlaced)
        {
            levelInfo += room.name + "," + room.X + "," + room.Y + "\n";

        }
        File.WriteAllText(path, levelInfo);

        path = Application.dataPath + "/Enemy.txt";
        string enemyInfo = "";

        foreach (EnemyToBePlaced enemy in enemiesToBePlaced)
        {
            enemyInfo += enemy.name + "," + enemy.roomX + "," + enemy.roomY + "," + enemy.X + "," + enemy.Y + "\n";

        }
        File.WriteAllText(path, enemyInfo);

    }

    public bool DoesRoomExist(int x, int y){
        return roomsToBePlaced.Find(item => item.X == x && item.Y == y) != null;
    }

    public RoomToBePlaced FindRoom( int x, int y)
    {
        return roomsToBePlaced.Find( item => item.X == x && item.Y == y);
    }

    public EnemyToBePlaced FindEnemy( float x, float y)
    {
        return enemiesToBePlaced.Find( item => item.X == x && item.Y == y);
    }
}
