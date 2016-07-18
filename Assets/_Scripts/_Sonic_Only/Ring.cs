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
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    // For when it's in its' default suspended state. Exists until player hits it.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupRing();
        }
    }

    // For when it falls out of the player. In Start() we set it to be destroyed after some time.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupRing();
        }
    }

    public void PickupRing()
    {
        AudioSource.PlayClipAtPoint(ringSound, transform.position);
        PlayerStatus.S.rings += 1;
        Instantiate(ringParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
