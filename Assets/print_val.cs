using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class print_val : MonoBehaviour {
    public float speed;
    public Text countText;


    // Use this for initialization
    public void Set (string val) {
        countText = GetComponent<Text>();
        string s = val;
        countText.text = s;
	}
	
}
