using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour
{
    public float speed = 30;
    public GameObject ringParticles;

    public bool fallen = false;

    // Use this for initialization
    void Start()
    {
        if (fallen)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-85, 85), Random.Range(0, 85)));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerStatus.S.canPickupRings)
        {
            PlayerStatus.S.rings += 1;
            Instantiate(ringParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (fallen)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));
        }
    }

}
