using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RingsText : MonoBehaviour {

    Text thisText;

	// Use this for initialization
	void Start () {

        thisText = GetComponent<Text>();

	
	}
	
	// Update is called once per frame
	void Update () {
	
        thisText.text = "<color=yellow>RINGS</color> " + PlayerStatus.S.rings.ToString();
	}
}
