using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float timer = 0.0f;
    public bool effect = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(effect)
        {
            timer += Time.deltaTime;
            if(timer < 0.1f)
                sprite.color = Color.red;
            else
            {
                sprite.color = Color.white;
                effect = false;
                timer = 0.0f;
            }
        }
    }
}
