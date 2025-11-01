using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private UIManager _uimanager;

    private void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public void Start_Single_Player()
    {
        Debug.Log("Single");

        SceneManager.LoadScene("Single_Player_V");
    }

    public void Start_Coop()
    {
        Debug.Log("Coop");
       
        SceneManager.LoadScene("Multi_Player_V");
     
    }

    public void Main_Return()
    {
        Debug.Log("Return");

        SceneManager.LoadScene("Single_Player_V");
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        _uimanager.PauseScreen.SetActive(false);
        Time.timeScale = 1.0f;     
    }
}
