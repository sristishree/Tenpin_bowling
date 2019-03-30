using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderControlSpeed : MonoBehaviour
{
    public Ball_com bfnl;
    public Slider sl;
    public GameObject g;
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Player");
    }
    public void Valuefun(int val)
    {
        sl = GetComponent<Slider>();
        //If we want to set the slider value according to some other input:
        sl.value = val;
        PlayerPrefs.SetInt("speed_get", val);
        //If we want to take input from slider to set value:
        //float val = sl.value;
        //bfnl = g.GetComponent<Ball_com>();
        //bfnl.init(val);
    }
}