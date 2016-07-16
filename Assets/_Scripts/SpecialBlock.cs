using UnityEngine;
using System.Collections;

public class SpecialBlock : MonoBehaviour
{
    Animator anim;
    public GameObject mushroom;
    public bool wasHit = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PlayerTop")
        {
            anim.Play("s_block_move", 0, 0);

            if (!wasHit)
            {
                Instantiate(mushroom, transform.position, transform.rotation);
                transform.GetChild(0).gameObject.SetActive(false);
                wasHit = true;
            }
        }
    }

}
