using UnityEngine;
using System.Collections;

public class Ball_final : MonoBehaviour {
    public float value = 0.0f;
    const float multiplier = 10.0f;
    public void init(float val)
    {
        value = val;
        print("Value stored as " + value);
        int i = 0;
        for (i = 0; i < 100; i++)
        {
            transform.Translate(Vector3.down * multiplier);
        }
        print("Command executed");
    }
    public void start()
    {
        
    }
    void update() 
    {
    }

	
}
