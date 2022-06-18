/*
Code used to control how the camera moves. It follows the player from room
to room.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Instance of Camera Controller so methods can be called easier
    public static CameraController instance;
    // Current room the player is in
    public Room currRoom;
    // Speed in which camera follows player from room to room
    public float moveSpeedChangeRoom;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    // Updates camera position to match player in room
    void UpdatePosition()
    {
        if (currRoom == null)
        {
            return;
        }

        // Calls function to get position
        Vector3 targetPos = GetCameraTargetPosition();

        // Changes camera position to target position
        transform.position = Vector3.MoveTowards(transform.position,
        targetPos, Time.deltaTime * moveSpeedChangeRoom);

    }

    // Function that return the new camera position
    Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)
        {
            return Vector3.zero;
        }

        // Changes camera position to center of current room
        Vector3 targetPos = currRoom.GetRoomCentre();
        targetPos.z = transform.position.z;
        
        return targetPos;
    }

    // Checks if camera is still switching between scenes
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
