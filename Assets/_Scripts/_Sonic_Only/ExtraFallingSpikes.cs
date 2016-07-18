using UnityEngine;
using System.Collections;

public class ExtraFallingSpikes : MonoBehaviour
{
    public GameObject spikes;

    public float maxTime = 1;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("HaveFun", 1, Random.Range(0.0f, maxTime));
    }

    void HaveFun()
    {
        var fallingSpike = Instantiate(spikes, transform.position + transform.up * 8 + transform.right * Random.Range(-3.0f, 3.0f),
            Quaternion.Euler(0, 0, 180)) as GameObject;

        fallingSpike.AddComponent<Rigidbody2D>();

        Destroy(fallingSpike, 3.0f);

        maxTime -= 0.2f;
    }

}
