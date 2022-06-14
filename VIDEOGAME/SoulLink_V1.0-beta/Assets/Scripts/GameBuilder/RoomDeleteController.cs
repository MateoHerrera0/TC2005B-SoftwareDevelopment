using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDeleteController : MonoBehaviour
{
    GameBuilderController editor;
    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
        Vector2 pos = editor.SnapToGrid(new Vector2(this.transform.position.x, this.transform.position.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        if(!editor.enemyPlaceState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(this.gameObject);
                editor.roomsToBePlaced.Remove(editor.FindRoom((int)(this.transform.position.x/17.5), (int)(this.transform.position.y/9)));

            }
            if (Input.GetMouseButtonDown(1))
            {
                editor.currentRoom = this.gameObject;
                editor.ToggleEnemyPlaceState(this.transform.position.x, this.transform.position.y);
            }
        }
    }
}
