/*
Code that controls all aspects of the game builder. This includes object
instantiation depending on button pressed, creation of strings where
level data will be stored and more. 

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// Class that stores info of rooms placed in builder
public class RoomToBePlaced
{
    public string name;
    public int X;
    public int Y;
}

// Class that stores info of enemies placed in builder
public class EnemyToBePlaced
{
    public string name;
    public int roomX;
    public int roomY;
    public float X;
    public float Y;
}

// Class that stores info of obstacles placed in builder
public class ObstacleToBePlaced
{
    public string name;
    public int roomX;
    public int roomY;
    public float X;
    public float Y;
}

public class GameBuilderController : MonoBehaviour
{
    // Array of item buttons
    public ItemController[] itemButtons;
    // Array containing the items to be placed
    public GameObject[] itemList;
    // Array containing item images
    public GameObject[] itemImage;
    // Variable that stores all game objects that will be placed
    public GameObject emptyParent;
    // List that stores all rooms to be placed
    public List<RoomToBePlaced> roomsToBePlaced = new List<RoomToBePlaced>();
    // List that stores all enemies to be placed
    public List<EnemyToBePlaced> enemiesToBePlaced = new List<EnemyToBePlaced>();
    // List that stores all obstacles to be placed
    public List<ObstacleToBePlaced> obstaclesToBePlaced = new List<ObstacleToBePlaced>();
    // Id of current button pressed
    public int currentButtonPressedId;
    // Id of object related to button
    public string currentButtonPressedObjectId;
    // Type of object placed
    public string currentButtonPressedType;
    // Stores current room opened in builder
    public GameObject currentRoom;
    // Stores grid size of rooms
    public Vector2 gridSize;
    // Stores room buttons
    GameObject roomButtons;
    // Stores enemy buttons
    public GameObject enemyButtons;
    // Stores boss button
    public GameObject bossButton;
    // Stores boss dropdown menu
    public GameObject bossDropdown;
    // Stores enemy dropdown menu
    public GameObject enemyDropdown;
    // Stores obstacle dropdown menu
    public GameObject obstacleDropdown;
    // Checks if enemy dropdown menu is active
    bool enemyDropdownPressed = false;
    // Checks if obstacle dropdown menu is active
    bool obstacleDropdownPressed = false;
    // Checks if boss dropdown menu is active
    bool bossDropdownPressed = false;
    // Checks if room has been opened so that enemies can be placed
    public bool enemyPlaceState = false;
    // Checks if boss has been placed
    public bool bossPlaced = false;
    // String that stores room data
    public string levelString;
    // String that stores enemy data
    public string enemyString;
    // String that stores obstacle data
    public string obstacleString;
    // Variable that stores audio of builder
    AudioSource placeAudio; 

    // Start is called before the first frame update
    void Awake() {
        // Destroys duplicated game builder controller objects stored in dont 
        // destroy on load object
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameBuilderController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        // Preserves game builder object even when changing scenes to test
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Room buttons are found
        roomButtons = GameObject.FindGameObjectWithTag("RoomButtons");
        // Start room is always placed
        RoomToBePlaced startRoom = new RoomToBePlaced();
        startRoom.name = "Start";
        startRoom.X = 0;
        startRoom.Y = 0;
        roomsToBePlaced.Add(startRoom);
        // Gets audio source
        placeAudio = GetComponent<AudioSource>();
    }

    private void Update() {
        // Screen to world pos of mouse
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // Current position on grid for rooms
        Vector2 currentPosOnGrid = SnapToGrid(worldPos);

        int roomPosX = (int)(currentPosOnGrid.x/17.5);
        int roomPosY = (int)(currentPosOnGrid.y/9);

        // When button odf builder is pressed it instaniates item image of 
        // object that will be placed in builder
        if (Input.GetMouseButtonDown(0) && itemButtons[currentButtonPressedId].clicked)
        {
            itemButtons[currentButtonPressedId].clicked = false;

            // Runs if room isnt opened
            if (!enemyPlaceState)
            {
                // Checks if room already exists in that position
                if (DoesRoomExist(roomPosX, roomPosY) == false)
                {
                    // Checks if en room is already placed, it can´t be placed
                    // twice
                    if (currentButtonPressedObjectId == "End" && DoesEndRoomExist())
                    {
                        Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
                        return;
                    }

                    // Stores placed room data
                    RoomToBePlaced room = new RoomToBePlaced();
                    room.name = currentButtonPressedObjectId;
                    room.X = roomPosX;
                    room.Y = roomPosY;
                    
                    // Room data is added to list
                    roomsToBePlaced.Add(room);

                    // Object is placed in builder
                    GameObject newRoom = Instantiate(itemList[currentButtonPressedId],
                    new Vector3(currentPosOnGrid.x, currentPosOnGrid.y, 0),
                    Quaternion.identity);
                    newRoom.transform.parent = emptyParent.transform;
                }
                // If element placed is obstacle, obstacle data is stored and
                // added to obstacle list
            } else if (currentButtonPressedType == "obstacle")
            {
                ObstacleToBePlaced obstacle = new ObstacleToBePlaced();
                obstacle.name = currentButtonPressedObjectId;
                obstacle.roomX = roomPosX;
                obstacle.roomY = roomPosY;
                obstacle.X = worldPos.x;
                obstacle.Y = worldPos.y;

                obstaclesToBePlaced.Add(obstacle);

                GameObject newObstacle = Instantiate(itemList[currentButtonPressedId],
                new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
                newObstacle.transform.parent = emptyParent.transform;
                // If enemy button is pressed, enemy data is stored in enemy list
            } else
            {
                // if (currentButtonPressedType == "Boss" && GameObject.FindGameObjectWithTag("Boss") != null)
                // {
                //     Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
                //     return;
                // }
                EnemyToBePlaced enemy = new EnemyToBePlaced();
                enemy.name = currentButtonPressedObjectId;
                enemy.roomX = roomPosX;
                enemy.roomY = roomPosY;
                enemy.X = worldPos.x;
                enemy.Y = worldPos.y;

                enemiesToBePlaced.Add(enemy);

                GameObject newEnemy = Instantiate(itemList[currentButtonPressedId],
                new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
                newEnemy.transform.parent = emptyParent.transform;
            }
            // Audio is played when object is placed
            placeAudio.Play();
            // Item image is destroyed
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }
    }

    // Function that handles scene management between game builder and test
    // scene
    public void ToggleGameBuilder()
    {
        // Scene name is stored
        Scene scene = SceneManager.GetActiveScene();

        // If test button is clicked on game builder scene, the scene changes
        // from game builder to test scene
        if (scene.name == "GameBuiler")
        {
            if (enemyPlaceState)
            {
                Return();
            }
            // Checs if boss has been placed
            if (GameObject.FindGameObjectWithTag("Boss") != null)
            {
                bossPlaced = true;
            }else
            {
                bossPlaced = false;
            }
            // Level info is created into strings
            CreateInfoStrings();
            // Builder buttons are set to not active
            roomButtons.SetActive(false);
            emptyParent.SetActive(false);
            obstacleDropdown.SetActive(false);
            enemyDropdown.SetActive(false);
            // Test scene is loaded
            SceneManager.LoadScene("Main");
        } 
        else
        {
            // Info strings are reset
            levelString = "";
            enemyString = "";
            obstacleString = "";
            CreateInfoStrings();
            roomButtons.SetActive(true);
            emptyParent.SetActive(true);
            // If active scene is test scene, and test button is clicked,
            // builder scene is loaded.
            SceneManager.LoadScene("GameBuiler");
        }
    }

    // Function that toggles between room opened and not opened
    public void ToggleEnemyPlaceState(float posX, float posY)
    {
        // If room is clicked when not inide room, camera zooms into room
        if (!enemyPlaceState)
        {
            // Camera size and position is changed
            Camera.main.transform.position = new Vector3(posX, posY, -10);
            Camera.main.orthographicSize = 5;
            // Room buttons are deactivated and enemy buttons are active
            roomButtons.SetActive(false);
            enemyButtons.SetActive(true);
            // In case that the room clicked is end room, boss button is 
            // activated
            if (currentRoom.name.Contains("End"))
            {
                bossButton.SetActive(true);
            }
            enemyPlaceState = true;
            return;
        }

        // In case room is already opened, and return button is pressed, 
        // camera changes back and correct buttos are activated.
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = 60;
        roomButtons.SetActive(true);
        enemyButtons.SetActive(false);
        bossButton.SetActive(false);
        enemyPlaceState = false;
    }

    // Function that snaps room images to grid size
    public Vector2 SnapToGrid(Vector2 originalPos)
    {
        Vector2 newPosition = new Vector2
        (
            Mathf.Round(originalPos.x / gridSize.x) * gridSize.x,
            Mathf.Round(originalPos.y / gridSize.y) * gridSize.y
        );

        return newPosition;
    }

    // Function that writes stored info into strings
    void CreateInfoStrings(){
        foreach (RoomToBePlaced room in roomsToBePlaced)
        {
            levelString+= room.name + "," + room.X + "," + room.Y + "_";
            LevelInformation.levelRooms = levelString;
        }
        
        foreach (EnemyToBePlaced enemy in enemiesToBePlaced)
        {
            enemyString += enemy.name + "," + enemy.roomX + "," + enemy.roomY + "," + enemy.X + "," + enemy.Y + "_";
            LevelInformation.levelEnemies = enemyString;
        }

        foreach (ObstacleToBePlaced obstacle in obstaclesToBePlaced)
        {
            obstacleString += obstacle.name + "," + obstacle.roomX + "," + obstacle.roomY + "," + obstacle.X + "," + obstacle.Y + "_";
            LevelInformation.levelObstacles = obstacleString;
        }
    }

    // Function that checks if room exists based on coordinates
    public bool DoesRoomExist(int x, int y){
        return roomsToBePlaced.Find(item => item.X == x && item.Y == y) != null;
    }

    // Function that checks if end room exists
    bool DoesEndRoomExist()
    {
        return roomsToBePlaced.Find(item => item.name == "End") != null;
    }

    // Function that returns room to be placed object based on coordinates
    public RoomToBePlaced FindRoom(int x, int y)
    {
        return roomsToBePlaced.Find( item => item.X == x && item.Y == y);
    }

    // Function that returns enemy to be placed object based on coordinates
    public EnemyToBePlaced FindEnemy( float x, float y)
    {
        return enemiesToBePlaced.Find( item => item.X == x && item.Y == y);
    }

    // Function that returns obstacle to be placed object based on coordinates
    public ObstacleToBePlaced FindObstacle( float x, float y)
    {
        return obstaclesToBePlaced.Find( item => item.X == x && item.Y == y);
    }

    // Function that toggles enemy dropdown menu
    public void ToggleEnemyDropdown()
    {
        if (enemyDropdownPressed)
        {
            enemyDropdown.SetActive(false);
            enemyDropdownPressed = false;
            return;
        }
        // This function deactivates already open panels too
        enemyDropdown.SetActive(true);
        enemyDropdownPressed = true;
        obstacleDropdown.SetActive(false);
        bossDropdown.SetActive(false);
    }

    // Function that toggles obstacle dropdown menu
    public void ToggleObstacleDropdown()
    {
        if (obstacleDropdownPressed)
        {
            obstacleDropdown.SetActive(false);
            obstacleDropdownPressed = false;
            return;
        }
        // This function deactivates already open panels too
        obstacleDropdown.SetActive(true);
        obstacleDropdownPressed = true;
        enemyDropdown.SetActive(false);
        bossDropdown.SetActive(false);
    }

    // Function that toggles boss dropdown menu
    public void ToggleBossDropdown()
    {
        if (bossDropdownPressed)
        {
            bossDropdown.SetActive(false);
            bossDropdownPressed = false;
            return;
        }
        // This function deactivates already open panels too
        bossDropdown.SetActive(true);
        bossDropdownPressed = true;
        enemyDropdown.SetActive(false);
        obstacleDropdown.SetActive(false);
    }

    // Function that returns to default builder sate
    public void Return()
    {
        ToggleEnemyPlaceState(0, 0);
        obstacleDropdown.SetActive(false);
        enemyDropdown.SetActive(false);
    }

    // Function that deletes do not destroy on load object and returns to main
    // menu
    public void ReturnToMenu()
    {
        LevelInformation.levelEnemies = "";
        LevelInformation.levelRooms = "";
        LevelInformation.levelObstacles = "";
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }
}
