using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float rotationSpeed = 30;
    public float ringLifetime = 6;

    public GameObject ringParticles;

    public bool fallen = false;

    public AudioClip ringSound;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        if (fallen)
        {
            Destroy(gameObject, ringLifetime);
            rb2d.gravityScale = 0.75f;
            rb2d.AddForce(new Vector2(Random.Range(-4.0f, 4.0f), Random.Range(4, 7)), ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupRing();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupRing();
        }
    }

    public void PickupRing()
    {
        AudioSource.PlayClipAtPoint (ringSound, transform.position);
        PlayerStatus.S.rings += 1;
        Instantiate(ringParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
