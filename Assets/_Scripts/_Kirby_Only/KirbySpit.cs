using UnityEngine;
using System.Collections;

public class KirbySpit : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float shootForce = 3;
    public float lifeTime = 1.5f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        rb2d.AddForce((new Vector3(KirbyMovement.S.transform.localScale.x, 0, 0)) * shootForce, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
