using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;

    public bool isCoop = false;

    public int ispause = 0;


    [SerializeField]
    public GameObject player;

    [SerializeField]
    public GameObject CoopP;


    private UIManager _uiManager;
    private Spawn _spawnmanager;
  
  
   
   

    private void Start()
    {
      _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
      _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawn>();

        _uiManager.PauseScreen.SetActive(false);




    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ispause == 0)
            {
                _uiManager.PauseScreen.SetActive(true);

                Time.timeScale = 0f;

                ispause = 1;

               
            }
            else if (ispause == 1)
            {
                _uiManager.PauseScreen.SetActive(false);

                Time.timeScale = 1f;

                ispause = 0;
            }
        }




        if (gameOver == true)
        {
            

            if (isCoop == false)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Single_M();
                }

            }


            if (isCoop == true)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Coop_M();
                }


            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }

     

    }

    private void Coop_M()
    {
           
            gameOver = false;
            _uiManager.HideTitleScreen();
            _spawnmanager.StartSpawnRoutines();
            Instantiate(CoopP, transform.position + new Vector3(2f, -1f, 0), Quaternion.identity);
           
        
    }

    private void Single_M()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameOver = false;
            _uiManager.HideTitleScreen();
            _spawnmanager.StartSpawnRoutines();
            Instantiate(player, transform.position + new Vector3(0, 0, 0), Quaternion.identity);


        }
    }





}




