using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// This object is tagged as "Player".
public class PlayerStatus : MonoBehaviour
{
    // This is a singleton.
    public static PlayerStatus S;

    MeshRenderer mRenderer;

    public int rings = 0;
    public int flashes = 5;

    public float flashTime = 0.15f;

    public bool gotMushroom = false;

    void Awake()
    {
        S = this;
        mRenderer = GetComponent<MeshRenderer>();
    }

    public void HurtFlashMethod()
    {
        StartCoroutine(HurtFlash());
    }

    IEnumerator HurtFlash()
    {
        // Is now on a layer that can not collide with rings.
        // To view layer collision matrix: "Edit" -> "Project Settings" -> "Physics 2D".
        gameObject.layer = 8;

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
    }

    // Reloads the current scene. MUST have line 3 for this.
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
