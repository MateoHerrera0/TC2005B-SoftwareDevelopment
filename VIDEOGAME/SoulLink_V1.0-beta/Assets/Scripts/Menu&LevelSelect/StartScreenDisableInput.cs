using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenDisableInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            UnityEngine.WebGLInput.captureAllKeyboardInput = false;
        #endif
    }
}
