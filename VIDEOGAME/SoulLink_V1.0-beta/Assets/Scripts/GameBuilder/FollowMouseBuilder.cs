/*
Code used to make tinted objects follow mouse during game builder.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseBuilder : MonoBehaviour
{
    private GameBuilderController editor;
    public Vector2 imagePos;

    void Start() {
        editor = GameObject.FindGameObjectWithTag("GameBuilderController").GetComponent<GameBuilderController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        
        if (!editor.enemyPlaceState)
        {
            transform.position = editor.SnapToGrid(worldPos);
        } else
        {
            transform.position = worldPos;
        }
        imagePos = transform.position;
    }

    
}
