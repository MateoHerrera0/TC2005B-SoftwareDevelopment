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
    private float timer; 

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

        Vector2 dodge = new Vector2(horizontal*(speed+50), vertical*(speed+50)); 
        if (horizontal < 0)
        {
            rendr.flipX = true;
        } else
        {
            rendr.flipX = false;
        }
        Debug.Log(rb2d.velocity);
        // Condition to allow dodge: if space clicked
        if(Input.GetKeyDown("space"))
        {
            rb2d.AddForce(dodge, ForceMode2D.Impulse);
            //timer += Time.deltaTime;
            // increase velocity by adding 50 to speed
            //rb2d.velocity = new Vector3(horizontal*(speed+30), vertical*(speed+30), 0);
        }
        else
        // if space not clicked
            // Continue with normal velocity
            rb2d.velocity = new Vector3(horizontal*speed, vertical*speed, 0);
    }
}
