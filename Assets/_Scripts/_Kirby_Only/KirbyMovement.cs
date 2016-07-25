using UnityEngine;
using System.Collections;

public class KirbyMovement : MonoBehaviour
{
    public static KirbyMovement S;

    Rigidbody2D rb2d;

    float xinput;

    public float speed = 5.0f;
    public float jumpSpeed = 12.0f;

    public int jumpCtr = 5;
    public int maxJumps = 5;

    public bool canMove = true;

    void Awake()
    {
        S = this;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Explains 'FixedUpdate' -> http://unity3d.com/learn/tutorials/topics/scripting/update-and-fixedupdate
    void FixedUpdate()
    {
        // Explains 'GetAxis' -> https://unity3d.com/learn/tutorials/topics/scripting/getaxis
        xinput = Input.GetAxis("Horizontal");

        if (xinput != 0)
            transform.localScale = new Vector2(Mathf.Sign(xinput) * transform.localScale.y, transform.localScale.y);

        if (canMove)
            rb2d.velocity = new Vector2(xinput * speed, rb2d.velocity.y);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCtr > 0)
        {
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            rb2d.velocity = new Vector2(0, jumpSpeed);
            jumpCtr -= 1;
        }
    }

    public void Englarge()
    {
        transform.localScale = new Vector2(transform.localScale.x * 1.5f, 1.5f);
        speed /= 2;
        jumpSpeed /= 2;
    }

    public void Shrink()
    {
        transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x)*1, 1); // Set scale to 1.
        speed *= 2;
        jumpSpeed *= 2;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
            jumpCtr = maxJumps;
    }

}
