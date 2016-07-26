using UnityEngine;
using System.Collections;

public class KirbyMonster : MonoBehaviour
{
    // All start as '0' change in Unity editor.
    public int monsterID = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "kirbyPuff")
        {
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "kirby" && KirbyController.S.isSucking)
        {
            KirbyController.S.InhaleMonster(monsterID);

            //KirbyStatus.S.canSuck = false;

            Destroy(gameObject);
        }
    }

}
