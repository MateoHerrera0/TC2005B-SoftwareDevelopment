/*
Code used to make tinted objects follow mouse during game builder.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseBuilder : MonoBehaviour
{
    // Variable that stores game builder controller game object
    private GameBuilderController editor;
    // Variable that stores the image position
    public Vector2 imagePos;

    void Start() {
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Screen to world position of mouse
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        
        // Depending on state of builder (inside or outside room), snap to place
        // is implemented
        if (!editor.enemyPlaceState)
        {
            transform.position = editor.SnapToGrid(worldPos);
        } else
        {
            transform.position = worldPos;
        }
        // Image position is updated.
        imagePos = transform.position;
    }

    
}
