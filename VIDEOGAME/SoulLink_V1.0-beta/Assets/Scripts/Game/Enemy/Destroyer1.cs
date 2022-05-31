/*
Code used to destroy projectiles after a period of time

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer1 : MonoBehaviour
{
    // Determine time 
    [SerializeField] private float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Use destroy with 2 parameters, to destroy object after time
        Destroy(gameObject, timer);
    }
}
