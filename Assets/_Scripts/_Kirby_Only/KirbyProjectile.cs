using UnityEngine;
using System.Collections;

public class KirbyProjectile : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float shootForce = 15;
    public float lifeTime = 0.5f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        //rb2d.AddForce((new Vector3(KirbyController.S.transform.localScale.x, 0, 0)) * shootForce, ForceMode2D.Impulse);

        rb2d.velocity = new Vector2((KirbyController.S.transform.localScale.x * shootForce) + KirbyController.S.rb2d.velocity.x, 0);
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }

}
