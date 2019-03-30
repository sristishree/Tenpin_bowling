using UnityEngine;
using System.Collections;

public class MainCam : MonoBehaviour {
    public int count=0;
    public string name = "hello";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool isKey = Input.GetKey(KeyCode.A);
        print(isKey);

	}
}
