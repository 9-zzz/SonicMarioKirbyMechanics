using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    // This is a singleton.
    public static PlayerStatus S;

    public float flashTime;

    public int rings = 0;

    public bool gotMushroom = false;

    void Awake()
    {
        S = this;
    }

    public void HurtColliderFlashMethod(int flashes)
    {
        StartCoroutine(HurtColliderFlash(flashes));
    }

    IEnumerator HurtColliderFlash(int flashes)
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
        gameObject.layer = 0;
    }

}
