using UnityEngine;
using UnityEngine.UI;

public class RingsText : MonoBehaviour
{
    // MUST have line 2 in order to use 'Text' objects.
    Text thisText;

    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps the text up to date by accessing the singleton on the player.
        thisText.text = "<color=yellow>RINGS</color> " + PlayerStatus.S.rings.ToString();
    }

}
