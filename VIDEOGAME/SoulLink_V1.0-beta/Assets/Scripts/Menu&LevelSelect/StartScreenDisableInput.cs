/*
Code that disables all key capture in embedded build

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenDisableInput : MonoBehaviour
{
    // Start is called before the first frame update, and it disables capture
    // all key inputs.
    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            UnityEngine.WebGLInput.captureAllKeyboardInput = false;
        #endif
    }
}
