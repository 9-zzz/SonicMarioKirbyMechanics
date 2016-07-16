using UnityEngine;
using System.Collections;

public class BlockPieces : MonoBehaviour
{

    //public Component[] rbs;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2.0f);

        /*
        rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs) {
            rb.AddForce(0, 90, 0);
        }
        */

        transform.GetChild(1).GetComponent<Rigidbody>().AddForce(0, 20, 0, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

    }

}
