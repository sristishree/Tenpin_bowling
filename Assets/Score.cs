using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {
    public Text text;
    public Skittles sk;
    public GameObject game;
    Stack score = new Stack(4);
    // Use this for initialization
    void Start()
    {
        
    }
    public void init()
    {
        //game = GameObject.FindGameObjectWithTag("skittle");
        //sk = game.GetComponent<Skittles>();
        text = GetComponent<Text>();
    }

	public void scoring () {
        int highscore = PlayerPrefs.GetInt("high", 0);
        int score = PlayerPrefs.GetInt("Counter");
        if (highscore < score)
            PlayerPrefs.SetInt("high", score);
        text.text = "YOUR SCORE IS " + score + "\n\n\n" + "HIGHEST SCORE : " + PlayerPrefs.GetInt("high");
/*
        print(score.Count);
        if (score.Count != 4)
        {
            score.Push(PlayerPrefs.GetInt("Counter"));
        }
        else
        {
            text.text = "YOUR SCORE IS " + PlayerPrefs.GetInt("counter") + "\n" + "YOUR TOTAL SCORE IS ";
            score.Clear();
        }
        */
    }

    
}
