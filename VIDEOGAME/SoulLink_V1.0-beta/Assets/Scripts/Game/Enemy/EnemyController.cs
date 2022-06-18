/*
Code used to determine whether enemies are in room and make 
them act accordingly.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public bool notInRoom;
    public Behaviour enemyType;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // If player is not in room, behaviour is disabled.
        if (notInRoom)
        {
            enemyType.enabled = false;
            return;
        }
        enemyType.enabled = true;
    }
}
