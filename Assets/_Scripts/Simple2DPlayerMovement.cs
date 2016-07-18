using UnityEngine;
using System.Collections;

// This is an extremely quick and dirty demo-only 2D character controller.
// The focus of this tutorial is on the other mechanics.
public class Simple2DPlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float maxSpeed = 10.0f;
    public float jumpSpeed = 12.0f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, rb2d.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);

    }

}
