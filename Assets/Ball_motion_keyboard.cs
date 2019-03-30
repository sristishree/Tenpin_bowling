using UnityEngine;
using System.Collections;
using System.IO;
[RequireComponent (typeof(Rigidbody))]
public class Ball_motion_keyboard : MonoBehaviour {
    public float xforce = 10000.0f;
    const float multiplier = 1.0f;
    public float yforce = 10.0f;
    public float zforce = 80.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //x axis movement
        float x = 0.0f;
        
        if(Input.GetKey(KeyCode.D))
        {
            x = x + xforce;
        }
        if(Input.GetKey(KeyCode.A))
        {
            x = x - xforce;
        }
        System.Console.WriteLine(xforce);
        //z axis movement
        float z = 0.0f;
        print(xforce);
        if (Input.GetKey(KeyCode.W))
        {

            transform.Translate(Vector3.down * multiplier);
            //transform.Translate(new Vector3(0,-1,0)* multiplier);  //moderate speed, no effect of changing constant, random directions after collision
            //GetComponent<Rigidbody>().AddForce(new Vector3(0,0,1) * xforce, ForceMode.VelocityChange); //faster than previous, yet slow, no effect of changing constant
            //transform.Translate(new Vector3(0, -1, 0) * xforce); //moves in randomm directions after collision
            //GetComponent<Rigidbody>().velocity = Vector3.forward*xforce; //very slow, no effect of changing constant
            //GetComponent<Rigidbody>().velocity()



        }
        if (Input.GetKey(KeyCode.S))
        {
            z = z - zforce;
        }

        //y axis movement
        float y = 0.0f;

        if (Input.GetKey(KeyCode.Q))
        {
            y = y + yforce;
        }
        if (Input.GetKey(KeyCode.E))
        {
            y = y - yforce;
        }
		//GetComponent<Rigidbody> ().velocity (x, y, z);
		
	}
}
