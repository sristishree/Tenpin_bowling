using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderControlPos : MonoBehaviour
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
        sl.value = val;
        PlayerPrefs.SetFloat("pos_get", val);
    }
}