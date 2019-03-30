using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class Ball_com : MonoBehaviour {
    public float value = 0.0f;
    public int flag = 0;
    const float multiplier = 0.7f;
    public Skittles sk;
    public Score score;
    public PanelActive panel;
    public GameObject game;
    public GameObject game2;
    public GameObject game3;
    public GameObject ball;
    Rigidbody rb;

    int sp = 0;
    float ps = 0;
    //fudget factor will decide by what amount will side spin torque increase in every update
    //This will be input by the user
    float fudgeFactor = 0.0f;
    float c = 0.0f;
    // Use this for initialization
	void Start () {
        //StartCoroutine(corr());
        
        game = GameObject.FindGameObjectWithTag("skittle");
        game3 = GameObject.FindGameObjectWithTag("Panel");
        ball = GameObject.FindGameObjectWithTag("Player");

        //sk = game.GetComponent <Skittles>();
        sk = game.AddComponent<Skittles>();
        
        panel = game3.AddComponent<PanelActive>();
        panel.reset();
        sk.initial();
        sp = PlayerPrefs.GetInt("speed_get");
        ps = PlayerPrefs.GetFloat("pos_get");
        fudgeFactor = PlayerPrefs.GetFloat("spin_get");

        this.flag = 1;
        this.value = sp;
        Vector3 ii = new Vector3(ps, ball.transform.position.y, ball.transform.position.z);
        ball.transform.position = ii;
    }
    
 
     void Update () {
         if(flag==1)
         {
            c=c-fudgeFactor;
            //print("The value is " +value);
            rb = GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, 0, 1) * value/6, ForceMode.VelocityChange);
            
            rb.AddForce( c*Vector3.Cross(rb.velocity,new Vector3(0.0f,2.4f,0.0f)), ForceMode.Force);

            sk.count();
            if (sp!=0 && GameObject.FindGameObjectWithTag("Player").transform.position.z >= 1500)
            {
                panel.set();
                game2 = GameObject.FindGameObjectWithTag("text");
                score = game2.GetComponent<Score>();
                score.init();
                score.scoring();
                //sk.reset();
                flag = 0;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                StartCoroutine("corr");
                
            }
            else if(sp==0)
            {
                panel.set();
                game2 = GameObject.FindGameObjectWithTag("text");
                score = game2.GetComponent<Score>();
                score.init();
                score.scoring();
                //sk.reset();
                flag = 0;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                StartCoroutine("corr");
            }
            
        }

         
     }

    public void Change_by_index(int i)
    {
        SceneManager.LoadScene(i);
        

    }
    public IEnumerator corr()
    {
        
        yield return new WaitForSeconds(3.0f);
        Change_by_index(0);
    }

}
