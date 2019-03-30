using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class page_change : MonoBehaviour
{
    public void Change_by_index(int i)
    {
        SceneManager.LoadScene(i);
    }
}

