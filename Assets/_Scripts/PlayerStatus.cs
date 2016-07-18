using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    // This is a singleton.
    public static PlayerStatus S;

    public int rings = 0;
    public int flashes;

    public float flashTime;

    public bool gotMushroom = false;

    void Awake()
    {
        S = this;
    }

    public void HurtColliderFlashMethod()
    {
        StartCoroutine(HurtColliderFlash());
    }

    IEnumerator HurtColliderFlash()
    {
        // Is now on a layer that can not collide with rings. 
        // Set in the editor "Edit" -> "Project Settings" -> "Physics 2D".
        gameObject.layer = 8;

        for (int i = 0; i < flashes; i++)
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(flashTime);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(flashTime);
        }

        // Back on default layer after (flashTime*flashes) amount of time, in seconds.
        gameObject.layer = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
