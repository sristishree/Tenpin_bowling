using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class Ball_command : MonoBehaviour
{
    public float value = 0.0f;
    const float multiplier = 10.0f;
    void start() {
        print("Start");
    }
    public void init(float val)
    {
        value = val;
        print("Value stored as " + value);
        
    }
    void update()
    {
        int i = 0;
        if (Input.GetKey(KeyCode.W))
        {
            print("Command executed");
            for (i = 0; i < 100; i++)
            {
                transform.Translate(Vector3.down * multiplier);
            }
        }
    }


}
