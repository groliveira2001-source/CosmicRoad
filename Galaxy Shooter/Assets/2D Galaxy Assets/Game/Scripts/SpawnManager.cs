using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Spawn : MonoBehaviour
{

    
    [SerializeField]
    private GameObject EnemyShipPrefab;

    [SerializeField]
    private GameObject[] powerups;//ficed list of objects of the same nature

    [SerializeField] 
    private GameObject EnemyHorizontalPrefab;

    [SerializeField] 
    private GameObject EnemyAlertPrefab;

    [SerializeField] private Transform uiCanvas;


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
        StartCoroutine(EnemyHorizontalRoutine());
    }




    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(5f);


        float RandomX = Random.Range(-7, 7);



            while (_gameManager.gameOver == false && _uiManager.EndGame == false)
            {

                Instantiate(EnemyShipPrefab, new Vector3(RandomX, 30, 0), Quaternion.identity);
                yield return new WaitForSeconds(3.0f);
            }

            

    }

    IEnumerator PowerUpSpawnRoutine()
    {


        yield return new WaitForSeconds(5f);


        while (_gameManager.gameOver == false && _uiManager.EndGame == false)
            {

                int randomPowerUp = Random.Range(0, 3);

                Instantiate((powerups[randomPowerUp]), new Vector3(Random.Range(-7, 7), 6, 0), Quaternion.identity);

                yield return new WaitForSeconds(8.0f);
            }

        
    }

    IEnumerator EnemyHorizontalRoutine()
    {

        yield return new WaitForSeconds(5f);

        while (_gameManager.gameOver == false && _uiManager.EndGame == false)
        {
            // Mostra o alerta na UI
            GameObject alert = Instantiate(EnemyAlertPrefab, uiCanvas);
            alert.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -460);
            Destroy(alert, 1.5f);

            // Espera um pouco antes de spawnar o inimigo (tempo do alerta)
            yield return new WaitForSeconds(1.5f);

            // Spawna o inimigo depois do alerta
            Instantiate(EnemyHorizontalPrefab, new Vector3(0, -3.8f, 0), Quaternion.identity);

            // Ajusta o intervalo conforme a pontuação
            float spawnDelay = 20f;
            if (_uiManager.score > 200) spawnDelay = 15f;
            if (_uiManager.score > 500) spawnDelay = 10f;
            if (_uiManager.score > 800) spawnDelay = 7f;
            if (_uiManager.score > 1000) spawnDelay = 5f;
            if (_uiManager.score > 2500) spawnDelay = 3f;

            // Espera antes de mostrar o próximo alerta
            yield return new WaitForSeconds(Random.Range(spawnDelay - 2f, spawnDelay + 2f));
        }
    }






}
