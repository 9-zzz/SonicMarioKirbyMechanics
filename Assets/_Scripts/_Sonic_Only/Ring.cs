using UnityEngine;
using System.Collections;

// This class handles two types of rings.
// The rings are tagged "ring" and on the "ring" layer.
// Set gravity scale to zero and collider to 'is trigger' for regular ring.
// Set gravity scale to 1 and freeze rotation along z-axis for 'ring_fallen' prefab.
public class Ring : MonoBehaviour
{
    public float rotationSpeed = 300;
    public float ringLifetime = 6;

    // Dragged in the ringParticles prefab in Unity Editor.
    public GameObject ringParticles;

    public bool fallen = false;

    public AudioClip ringSound;

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

    // For when it's in its' default regular ring suspended state. Exists until player hits it.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupRing();
        }
    }

    // For when it falls out of the player. In Start() we set it to be destroyed automatically after some time.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupRing();
        }
    }

    public void PickupRing()
    {
        // We make use of the singleton on the PlayerStatus script as there is only one unique player.
        PlayerStatus.S.rings += 1;
        AudioSource.PlayClipAtPoint(ringSound, transform.position);
        Instantiate(ringParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
