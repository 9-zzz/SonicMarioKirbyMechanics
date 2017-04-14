using UnityEngine;

public class AutoDestroyPS : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
