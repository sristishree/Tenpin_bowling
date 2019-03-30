using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderControlSpin : MonoBehaviour
{
    public Ball_com bfnl;
    public Slider sl;
    public GameObject g;
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Player");
    }
    public void Valuefun(float val)
    {
        sl = GetComponent<Slider>();
        //If we want to set the slider value according to some other input:
        sl.value = (int)val;
        val = val * 0.0002f;
        PlayerPrefs.SetFloat("spin_get", val);
    }
}