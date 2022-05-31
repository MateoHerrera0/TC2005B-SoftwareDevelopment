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
        roomX = (int)pos.x/17;
        roomY = (int)pos.y/9;
    }

    private void Update() {
        if (!isItRoom && editor.FindRoom(roomX, roomY) == null)
        {
            DeleteObject();
        }
    }

    private void OnMouseOver() {
        
        if (!this.isItRoom)
        {
            if (Input.GetMouseButtonDown(1))
                {
                    DeleteObject();
                }
        }

        if(!editor.enemyPlaceState)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(this.gameObject);
                editor.roomsToBePlaced.Remove(editor.FindRoom((int)(this.transform.position.x/17.5), (int)(this.transform.position.y/9)));

            }
            if (Input.GetMouseButtonDown(0))
            {
                editor.ToggleEnemyPlaceState(this.transform.position.x, this.transform.position.y);
            }
        }
    }

    void DeleteObject()
    {
        Destroy(this.gameObject);
        if (!isItObstacle)
        {
            editor.enemiesToBePlaced.Remove(editor.FindEnemy(this.transform.position.x, this.transform.position.y));
            return;
        }
        editor.obstaclesToBePlaced.Remove(editor.FindObstacle(this.transform.position.x, this.transform.position.y));
    }
}
