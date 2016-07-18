using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    // Dragged the 'ring_fallen' prefab in editor.
    public GameObject ringFallen;

    public AudioClip ringExplodeSound;

    Transform ringShooter;

    void Start()
    {
        // Accessing player's children tranforms through singleton at runtime.
        ringShooter = PlayerStatus.S.transform.GetChild(1);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Game over, man. Game over!
        if (other.gameObject.tag == "Player" && PlayerStatus.S.rings == 0)
            PlayerStatus.S.Restart();

        if (other.gameObject.tag == "Player")
        {
            LoseRings();
       }
    }

    void LoseRings()
    {
        AudioSource.PlayClipAtPoint(ringExplodeSound, transform.position);

        // Make sure the spikes are CENTERED.
        PlayerStatus.S.HurtFlashMethod(transform.position.x);

        for (int i = 0; i < PlayerStatus.S.rings; i++)
        {
            // Randomly fans out the rings in a 180 angle range above the player.
            ringShooter.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-90, 90));

            // Alternative method for shooting rings out of the player. 
            // ringShooter.transform.Rotate(0, 0, (180 / PlayerStatus.S.rings));

            var ringF = Instantiate(ringFallen, PlayerStatus.S.transform.position, Quaternion.identity) as GameObject;

            ringF.GetComponent<Rigidbody2D>().AddForce(ringShooter.up * 4, ForceMode2D.Impulse);
        }

        PlayerStatus.S.rings = 0;
    }

}
