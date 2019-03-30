using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelActive : MonoBehaviour {
    public CanvasGroup cg;
	// Use this for initialization
	public void reset () {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
	}
    public void set()
    {
        cg.GetComponent<CanvasGroup>();
        cg.alpha = 1;
    }
}
