using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Spawn : MonoBehaviour
{

    
    [SerializeField]
    private GameObject EnemyShipPrefab;

    [SerializeField]
    private GameObject[] powerups;//ficed list of objects of the same nature

    private GameManager _gameManager;

   private UIManager _uiManager;
  

    // Start is called before the first frame update
     void Start()
    {
        _gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }




    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
        
    

    IEnumerator EnemySpawnRoutine()
    {
       
 
        float RandomX = Random.Range(-7, 7);



            while (_gameManager.gameOver == false && _uiManager.EndGame == false)
            {

                Instantiate(EnemyShipPrefab, new Vector3(RandomX, 30, 0), Quaternion.identity);
                yield return new WaitForSeconds(3.0f);
            }

            

    }

    IEnumerator PowerUpSpawnRoutine()
    {

        


        while (_gameManager.gameOver == false && _uiManager.EndGame == false)
            {

                int randomPowerUp = Random.Range(0, 3);

                Instantiate((powerups[randomPowerUp]), new Vector3(Random.Range(-7, 7), 6, 0), Quaternion.identity);

                yield return new WaitForSeconds(8.0f);
            }

        
    }

    

}
