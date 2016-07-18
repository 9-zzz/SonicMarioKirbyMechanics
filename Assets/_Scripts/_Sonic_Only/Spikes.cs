using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    public GameObject ringFallen;

    public AudioClip ringExplodeSound;

    Transform ringShooter;

    void Start()
    {
        ringShooter = PlayerStatus.S.transform.GetChild(1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerStatus.S.rings == 0)
            PlayerStatus.S.Restart();

        if (other.gameObject.tag == "Player")
            LoseRings();
    }

    void LoseRings()
    {
        AudioSource.PlayClipAtPoint(ringExplodeSound, transform.position);

        PlayerStatus.S.HurtColliderFlashMethod();

        for (int i = 0; i < PlayerStatus.S.rings; i++)
        {
            ringShooter.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-90, 90));
            //ringShooter.transform.Rotate(0, 0, (180 / PlayerStatus.S.rings));

            var ringF = Instantiate(ringFallen, PlayerStatus.S.transform.position, Quaternion.identity) as GameObject;

            ringF.GetComponent<Rigidbody2D>().AddForce(ringShooter.up * 4, ForceMode2D.Impulse);
        }

        PlayerStatus.S.rings = 0;
    }

}
