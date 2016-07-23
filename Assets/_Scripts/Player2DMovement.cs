using UnityEngine;
using System.Collections;

// This is an extremely quick and dirty demo-only 2D character controller.
// The focus of this tutorial is on the other mechanics.
// I put physics-2D-material with zero friction on the collider to stop from "sticking" to walls.
public class Player2DMovement: MonoBehaviour
{
    public static Player2DMovement S;

    // Set mass to 100. Set gravity scale to 3. Freeze rotation along Z-axis.
    Rigidbody2D rb2d;

    public float speed = 5.0f;
    public float jumpSpeed = 12.0f;

    public bool canJump = false;
    public bool canMove = true;

    void Awake()
    {
        S = this;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Explains 'FixedUpdate' -> http://unity3d.com/learn/tutorials/topics/scripting/update-and-fixedupdate
    void FixedUpdate()
    {
        if (canMove)
        {
            // Explains 'GetAxis' -> https://unity3d.com/learn/tutorials/topics/scripting/getaxis
            rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            canJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
            canJump = true;
    }

}
