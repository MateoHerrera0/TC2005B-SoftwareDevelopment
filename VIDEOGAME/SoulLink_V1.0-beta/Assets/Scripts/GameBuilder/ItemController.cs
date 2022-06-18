/*
Code that handles which button generates which object of the builder.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Variable that stores button id
    public int buttonId;
    // Variable that stores object id
    public string objectId;
    // Bool that checks if button has been pressed
    public bool clicked = false;
    // 
    public bool returnGame = false;
    // String that stores object type
    public string type;
    // Variable that stores game builder controller
    private GameBuilderController editor;
    
    // Start is called before the first frame update
    void Start()
    {
        // Game builder controller is found
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
        returnGame = true;
    }

    // Functionn that instatiates item image related to button when clicked and
    // updates button info in builder.
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
