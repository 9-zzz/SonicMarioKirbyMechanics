using UnityEngine;
using System.Collections;

public class BasicBlock : MonoBehaviour
{
    Animator anim;

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
            if (PlayerStatus.S.gotMushroom)
            {
                transform.parent.GetChild(1).gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            anim.Play("block_move", 0, 0);
            print("hit head");
        }
    }

}
