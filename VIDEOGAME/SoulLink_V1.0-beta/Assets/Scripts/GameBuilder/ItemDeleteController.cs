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
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
        Vector2 pos = editor.SnapToGrid(new Vector2(this.transform.position.x, this.transform.position.y));
        roomX = (int)(pos.x/17.5);
        roomY = (int)(pos.y/9);
    }

    private void Update() {
        if (editor.FindRoom(roomX, roomY) == null)
        {
            DeleteObject();
        }
    }

    private void OnMouseDown() {
                DeleteObject();
        Debug.Log(gameObject.name);

    }

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
