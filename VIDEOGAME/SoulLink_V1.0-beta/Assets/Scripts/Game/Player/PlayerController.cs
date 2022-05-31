/*
Code used to make player move while including animation settings, its a variant
of player move. Might be deleted or replaced.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField] Animator anim;
    Rigidbody2D rb2d;
    SpriteRenderer rendr;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rendr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        anim.SetFloat("Horizontal", horizontal);

        if (horizontal < 0)
        {
            rendr.flipX = true;
        } else
        {
            rendr.flipX = false;
        }
        
        // Condition to allow dodge: if space clicked
        if(Input.GetKeyDown("space"))
        {
            // increase velocity by adding 50 to speed
            rb2d.velocity = new Vector3(horizontal*(speed+50), vertical*(speed+50), 0);
        }
        // if space not clicked
        else
        {
            // Continue with normal velocity
            rb2d.velocity = new Vector3(horizontal*speed, vertical*speed, 0);
        }
    }
}
