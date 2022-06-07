/*
Code that handles which button generates which object of the builder.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int buttonId;
    public string objectId;
    public bool clicked = false;
    public bool returnGame = false;
    public string type;
    private GameBuilderController editor;
    
    // Start is called before the first frame update
    void Start()
    {
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
        returnGame = true;
    }

    public void PlacementButtonClicked()
    {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        clicked = true;

        Instantiate(editor.itemImage[buttonId], new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
        
        editor.currentButtonPressedId = buttonId;
        editor.currentButtonPressedObjectId = objectId;
        editor.currentButtonPressedType = type;
    }
}
