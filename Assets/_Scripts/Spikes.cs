using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    public GameObject ringFallen;

    public AudioClip ringExplodeSound;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(ringExplodeSound, transform.position);

            PlayerStatus.S.HurtColliderFlashMethod(5);

            for (int i = 0; i < PlayerStatus.S.rings; i++)
            {
                Instantiate(ringFallen, PlayerStatus.S.transform.position, Quaternion.identity);
            }

            PlayerStatus.S.rings = 0;
        }
    }

}
