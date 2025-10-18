using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] GameLives;

    public Image livesImageDisplay;

    [SerializeField]
    public GameObject titleScreen;

    [SerializeField]
    public GameObject VideoShow;

    [SerializeField] 
    public GameObject PauseScreen;

    [SerializeField]
    public GameObject YouWin;

    public int score;

    public int best_score;

 

    public bool EndGame = false;

    public bool ESC = false;

    public Text scoreText;

    public Text bestScoreText;

    private GameManager _gameManager;


    public void Start()
    {
       _gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();

        YouWin.SetActive(false);

        best_score = PlayerPrefs.GetInt("Bananas", 0);
        bestScoreText.text = "Best:" + best_score;

    }

    public void Update()
    {
        ResetScore();

        YouWon();

        BestScore();

        

        

    }

    public void UpdateLives(int currentLives)//criar uma variável que guardará quantas vidas o player tem (int = 0,1,2,3)
    {
        Debug.Log("PLayer Lives:" + currentLives);
         livesImageDisplay.sprite = GameLives[currentLives];

        
    }

    public void UpdateScore()
    {       

            score += 10;


            scoreText.text = ("Score:" + score);

        
    }
    public void ResetScore()

    {
        if(_gameManager.gameOver==true)
        {
            score = 0;
        }
    }
    public void BestScore()
    {

        if (score > best_score)
        {
            best_score = score;
            PlayerPrefs.SetInt("Bananas", best_score);
        }




        bestScoreText.text = ("Best:" + best_score);
    }
    public void ShowTitileScreen()
    {
        
        titleScreen.SetActive(true);
        ESC = false;
        
    }
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        VideoShow.SetActive(false);
        scoreText.text = "Score:"+0;
        ESC = true;
    }
    public void YouWon()
    {
        if (score == 5000)
        {
            YouWin.SetActive(true);
            EndGame = true;

        }
    }

   

}
