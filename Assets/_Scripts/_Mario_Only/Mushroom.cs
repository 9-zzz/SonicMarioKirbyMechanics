using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour
{

    public bool outsideOfBlock = false;

    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!outsideOfBlock)
            transform.Translate(0, 1.0f * Time.deltaTime, 0);
        else
            rb2d.velocity = new Vector2(1, rb2d.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "block")
        {
            outsideOfBlock = true;
            GetComponent<Collider2D>().isTrigger = false;
            rb2d.gravityScale = 1;
        }
    }

     void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatus.S.transform.localScale *= 2;
            PlayerStatus.S.gotMushroom = true;

            Destroy(gameObject);
        }
    }


}
