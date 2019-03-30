using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SliderControl : MonoBehaviour {
    public Ball_com bfnl;
    public Slider sl;
    public GameObject g;
    
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Player");
    }

    public void Valuefun(int i)
    {        
        sl = GetComponent<Slider>();
        float val = sl.value;
        //bfnl = g.GetComponent<Ball_com>();
        //bfnl.init(val);
    }	
}
