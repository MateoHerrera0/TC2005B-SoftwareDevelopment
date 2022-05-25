using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeleteController : MonoBehaviour
{
    GameBuilderController editor;
    public bool isItRoom;
    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
    }

    private void OnMouseOver() {
        
        if (!this.isItRoom)
        {
            if (Input.GetMouseButtonDown(1))
                {
                    
                        Destroy(this.gameObject);
                        editor.enemiesToBePlaced.Remove(editor.FindEnemy(this.transform.position.x, this.transform.position.y));
                
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
}
