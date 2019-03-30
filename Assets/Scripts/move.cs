using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0.5f,0f,0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-0.5f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.localScale = new Vector3(30f, 30f, 30f);
        }
        if (Input.GetKey(KeyCode.X))
        {
            transform.localScale = new Vector3(5f, 5f, 5f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, 0.5f, 0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, -0.5f, 0f);
        }
	}
}
