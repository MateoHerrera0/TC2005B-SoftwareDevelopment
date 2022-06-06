/*
Code used to manage room specific details, like which doors are loaded. 
This scriot is also used to assign room info to specific instances.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Room width
    public float Width;
    // Room height
    public int Height;
    // Room X coordinates
    public int X;
    // Room Y coordinates
    public int Y;
    // Bool variable that checks if unconnected doors have been removed
    private bool updatedDoors = false;
    

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    // List of doors in room
    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        // Check if room controller script is present in scene
        if (RoomController.instance == null)
        {
            Debug.Log("Pressed start in wrong room");
            return;
        }

        // Retrieves all doors in room and puts them in array
        Door[] ds = GetComponentsInChildren<Door>(); 
        foreach(Door d in ds)
        {
            // Adds doors to list
            doors.Add(d);
            // Categorizes each door depending on its type in the door script
            switch(d.doorType)
            {
                case Door.DoorType.right:
                rightDoor = d;
                break;
                case Door.DoorType.left:
                leftDoor = d;
                break;
                case Door.DoorType.top:
                topDoor = d;
                break;
                case Door.DoorType.bottom:
                bottomDoor = d;
                break;
            }
        }

        // Calls the room controller register room function with this room.
        RoomController.instance.RegisterRoom(this);
    }

    // void Update() {
    //     if(name.Contains("End") && !updatedDoors)
    //     {
    //         RemoveUnconnectedDoors();
    //         updatedDoors = true;
    //     }
    // }

    // Function that removes unconnected doors in room.
    public void RemoveUnconnectedDoors()
    {
        // It iterates through all doors in a room and removes them based on 
        // on wether or not there is a roomnext to the door
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                    {
                        door.doorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.left:
                    if(GetLeft() == null)
                    {
                        door.doorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.top:
                    if(GetTop() == null)
                    {
                        door.doorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.bottom:
                    if(GetBottom() == null)
                    {
                        door.doorCollider.SetActive(true);
                        door.gameObject.SetActive(false);
                    }
                break;
            }
        }
    }

    // Checks if there is a room to the right
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    // Checks if there is room to the left
    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    // Checks if there is room up.
    public Room GetTop()
    {
        if(RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    
    // Checks if there is room downs
    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    // Gets room center coordinates.
    public Vector3 GetRoomCentre()
    {
        return new Vector3(X*Width, Y*Height, 0);
    }

    // Returns current room to room controller.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            if (this.name.Contains("End"))
            {
                GameController.instance.endRoomReached = true;
            }
        }
    }
}
