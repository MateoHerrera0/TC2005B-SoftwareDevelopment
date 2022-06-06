/*
Code that cotrols room generation. It interprets grid coordinates and 
instances scenes additively depending on which room name is provided.
This script also deteermines wheter a room has enemies or not.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class that stores each room's info
public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    
    //Room controller instacne for easy attribute and method access
    public static RoomController instance;
    public RoomInfo currentLoadRoomData;
    public Room currRoom;
    // Queue that will store rooms that need to be loded
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    // List that stores rooms already loaded
    public List<Room> loadedRooms = new List<Room>();
    // Bool variable that checks if load room courutine is active or if it
    // has finished
    bool isLoadingRoom = false;
    // Bool variable that checks if unconnected doors have been removed.
    bool updatedRooms = false;
    
    void Awake() {
        // Instance of class is created on awake
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start() {
        // LoadRoom("Start", 0, 0);
        // LoadRoom("Empty", 1, 0);
        // LoadRoom("Empty", -1, 0);
        // LoadRoom("Empty", 0, 1);
        // LoadRoom("Empty", 2, -1);
    }

    void Update() {
        UpdateRoomQueue();
    }

    // Function that calls the load room coroutine if no rooms are currently
    // being loaded and removes unconnected doors
    void UpdateRoomQueue()
    {
        // If room is currently being loaded this function doeasent continue to
        // ensure no courutine overlapping
        if (isLoadingRoom)
        {
            return;
        }

        // When all rooms are loaded the unconnected doors are removed from 
        // each room. This is done by calling the RemoveUnconnectedFoors()
        // function from the room class.
        if (loadRoomQueue.Count == 0)
        {
            if (!updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        // Removes room from queue since it will be loaded.
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        // Load room courutine is called.
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    // Function that enqueues rooms to loadRoomQueue so they will be loaded
    // into the scene
    public void LoadRoom( string name, int x, int y)
    {
        // Checks if the room already exists so they dont overlap
        if(DoesRoomExist(x, y) == true)
        {
            return;
        }

        // Creates new room info for soon to be loaded room.
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        updatedRooms = false;

        // Adds new room info to queue
        loadRoomQueue.Enqueue(newRoomData);
    }

    // Coroutine that loads room into active scene 
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = info.name;
        // The room is loaded with the Additive load scene mode. So that the 
        // room scene is loaded as an object inside active scene
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        // Waits until scene is completely loaded.
        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    // Function that adds loaded rooms into loaded room list, and moves loaded
    // rooms to their postition respective to their grid coordinates. For example
    // a room with x = 0  and y = 1 will appear above the starting room whose 
    // coords are x = 0 and  y = 0.
    public void RegisterRoom(Room room)
    {
        // Code runs if room doesn't already exist.
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            // Moves room into world position relative to grid position.
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );

            // Room class info is updated.
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            // Assigns starting camera position to starting room.
            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            // Room is added to list.
            loadedRooms.Add(room);
        }
    }

    // Function that checs if room exists depending on coordinates.
    public bool DoesRoomExist(int x, int y){
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    // Function that returns room based on coordinates.
    public Room FindRoom( int x, int y)
    {
        return loadedRooms.Find( item => item.X == x && item.Y == y);
    }

    // Function that changes camera position to current room and starts a 
    // coroutine that will block exits of room until enemies are defeated.
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
        StartCoroutine(RoomCoroutine());
    }

    // This was done so that the camera finished transitioning form room to room
    // before enemies started attacking.
    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateRooms();
    }

    // Function that blocks doors in room when enemies are alivie in it.
    public void UpdateRooms()
    {
        // This process iterates through all rooms.
        foreach(Room room in loadedRooms)
        {
            // If rooms are not the current room enemies are deactivated so that
            // player isnt targeted outside room and doors remain open.
            if(currRoom != room)
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if(enemies != null)
                {
                    foreach(EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = true;
                    }
                }
                foreach(Door door in room.GetComponentsInChildren<Door>())
                {
                    door.doorCollider.SetActive(false);
                }
            }
            else
            {
                // This line of code searches for enemies in room.
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                // If enemies exist in room doors are blocked, if not doors
                // remain open.
                if(enemies.Length > 0)
                {
                    foreach(EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = false;
                    }
                    foreach(Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                    }

                }
                else
                {
                    foreach(Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }  
            }
        }
    } 
}
