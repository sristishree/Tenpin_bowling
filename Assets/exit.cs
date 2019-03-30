using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour {

	// Use this for initialization
	public void OnExit(int i)
    {
        print("Quit");
        Application.Quit();
    }
}
