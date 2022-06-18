/*
Code used to control object deletion in builder.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeleteController : MonoBehaviour
{
    GameBuilderController editor;
    public bool isItRoom;
    public bool isItObstacle;
    public int roomX;
    public int roomY;
    // Start is called before the first frame update
    void Start()
    {
        // Editor game object is stored
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
        // Position of current room is stored
        Vector2 pos = editor.SnapToGrid(new Vector2(this.transform.position.x, this.transform.position.y));
        roomX = (int)(pos.x/17.5);
        roomY = (int)(pos.y/9);
    }

    private void Update() {
        // If room is deleted, all objects inside are deleted too
        if (editor.FindRoom(roomX, roomY) == null)
        {
            DeleteObject();
        }
    }

    // When clicked, object is deleted
    private void OnMouseDown() {
        DeleteObject();
        Debug.Log(gameObject.name);

    }

    // Function that deletes object according to type.
    void DeleteObject()
    {
        if (!isItObstacle)
        {
            editor.enemiesToBePlaced.Remove(editor.FindEnemy(this.transform.position.x, this.transform.position.y));
        } else
        {
            editor.obstaclesToBePlaced.Remove(editor.FindObstacle(this.transform.position.x, this.transform.position.y));
        }
        Destroy(this.gameObject);
    }
}
