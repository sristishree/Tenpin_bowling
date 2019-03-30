using UnityEngine;
using System.Collections;

public class Motion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.1f, 0f, 0f);
        }
	}
}
