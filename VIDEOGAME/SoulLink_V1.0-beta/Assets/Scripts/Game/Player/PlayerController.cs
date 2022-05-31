// Player movement script
// Mateo Herrera Lavalle
// 7-04-2022
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
        

        rb2d.velocity = new Vector3(horizontal*speed, vertical*speed, 0);

    }
}
