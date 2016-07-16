using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public float flashTime;
    public static PlayerStatus S;

    public int rings = 0;

    public bool gotMushroom = false;
    public bool canPickupRings = true;

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
        canPickupRings = false;
        for (int i = 0; i < flashes; i++)
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(flashTime);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(flashTime);
        }
        canPickupRings = true;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
