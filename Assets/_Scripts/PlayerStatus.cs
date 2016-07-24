using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// This object is tagged as "Player".
public class PlayerStatus : MonoBehaviour
{
    // This is a singleton.
    public static PlayerStatus S;

    public Rigidbody2D rb2d;

    MeshRenderer mRenderer;
    Collider2D coll;

    public int rings = 0;
    public int flashes = 5;

    public float flashTime = 0.15f;

    public bool gotMushroom = false;

    public AudioClip deathSound;

    void Awake()
    {
        S = this;
        mRenderer = GetComponent<MeshRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    public void HurtFlashMethod(float spikesXposition)
    {
        StartCoroutine(HurtFlash(spikesXposition));
    }

    IEnumerator HurtFlash(float spikesXposition)
    {
        // Is now on a layer that can not collide with rings.
        // To view layer collision matrix: "Edit" -> "Project Settings" -> "Physics 2D".
        gameObject.layer = 8;
        Player2DMovement.S.canMove = false;

        // Player movement is disables and player is pushed in a direction relative to spikes hit.
        if (transform.position.x > spikesXposition)
            rb2d.velocity = new Vector2(3, 7);
        else
            rb2d.velocity = new Vector2(-3, 7);

        // Divide  flashTime by 2 so it makes sense. Ends up as 
        // (5 * 0.15) = 0.75 seconds time that player can't collide with rings.
        for (int i = 0; i < flashes; i++)
        {
            mRenderer.enabled = false;
            yield return new WaitForSeconds(flashTime / 2);
            mRenderer.enabled = true;
            yield return new WaitForSeconds(flashTime / 2);
        }

        // Back on default layer after (flashTime*flashes) amount of time, in seconds.
        // Now player can collide with rings again.
        gameObject.layer = 0;
        Player2DMovement.S.canMove = true;
    }

    public void DeathFall()
    {
        // Make sure the player is not behind anything and collides with nothing.
        transform.Translate(transform.forward * -1);
        coll.enabled = false;

        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Player2DMovement.S.canMove = false;
        rb2d.velocity = new Vector2(0, 10);
        StartCoroutine(TimedRestart(2));
    }

    // Reloads the current scene after 'x' seconds. MUST have line 3 for this.
    IEnumerator TimedRestart(float x)
    {
        yield return new WaitForSeconds(x);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
