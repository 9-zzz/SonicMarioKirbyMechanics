using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    public GameObject ringFallen;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatus.S.HurtColliderFlashMethod(10);

            for(int i = 0; i < PlayerStatus.S.rings; i++)
            {
                Instantiate(ringFallen, PlayerStatus.S.transform.position, Quaternion.identity);
            }

            PlayerStatus.S.rings = 0;
            //Ring explode
        }
    }

}
