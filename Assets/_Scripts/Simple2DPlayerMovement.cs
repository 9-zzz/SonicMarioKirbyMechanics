using UnityEngine;
using System.Collections;

public class Simple2DPlayerMovement : MonoBehaviour
{

    public float maxSpeed = 10.0f;
    public float jumpForce = 200.0f;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rb2d.AddForce(new Vector2(0, jumpForce));
    }

}
