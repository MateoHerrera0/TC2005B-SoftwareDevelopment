/*
Code used to define health bar and health points of each character
And to make an effect when health is lost

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Image that specifies hps in red 
    public Image hpImage;
    // White effect for each time hp is reduced
    public Image hpEffectImage;

    // Health points
    [HideInInspector] public float hp;
    // Maximum possible health points
    [SerializeField] public float maxHp;
    // Hurt speed
    [SerializeField] private float hurtSpeed = 0.005f;

    private void Start()
    {
        // At the beginning, the character has the maximum hp possible
        hp = maxHp;
    }

    private void Update()
    {
        // health bar will be the proportion of hp/maxHp
        hpImage.fillAmount = hp / maxHp;

        // If the effect image is higher than the hpImage
        if(hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            // Reduce proportion of effect image 
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        // If effect image isn't higher than the hpImage
        else
        {
            // Effect image fill will be the same as hpImage's
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
}
