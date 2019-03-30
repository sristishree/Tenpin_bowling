using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Acceleration : MonoBehaviour {
    public float xvel = 90.0f;
    public float zvel = 90.0f;
    public float yvel = 50.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var vel = GetComponent<Rigidbody>().velocity;
        if (Input.GetKey(KeyCode.D))
            vel[1] = vel[1] + xvel;
        if (Input.GetKey(KeyCode.W))
            vel[3] = vel[3] + zvel;
        GetComponent<Rigidbody>().AddForce(vel[1], vel[2], vel[3]);
	}
}