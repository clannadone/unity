using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour {
    public Text ErrorText;
    public UnityEvent Event;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ShowMessage(string str)
    {
        ErrorText.text = str;
    }

}
