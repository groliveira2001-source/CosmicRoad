using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{


    public bool shieldon = false;

    public bool canTripleShoot = false;

    public bool speedBoost = false;

    public int lives = 3;

    public bool PlayerX = false;
    public bool Player1 = false;
    public bool Player2 = false;

    [SerializeField]
    public GameObject _FireRight;
    [SerializeField]
    public GameObject _FireLeft;

    [SerializeField]
    public GameObject _Shield_Prefab;

    [SerializeField]
    public GameObject _Player_explosion;

    [SerializeField]
    private GameObject _TripleShoot;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _FireRate = 0.25f;

    private float _CanFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _UIManager;

    private GameManager _gameManager;

    private Spawn _spawnManager;

    [SerializeField]
    private AudioClip _clip;

    [SerializeField]
    public bool IsAlive = true;

    // Start is called before the first frame update
    private void Start()
    {

        

        _gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn>();

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();


        if (transform.position.x < -9.6f || transform.position.x > 9.4f)
        {
            Destroy(this.gameObject);
        }

        if (_gameManager.isCoop == false)
        {
            transform.position = new Vector3(0, 0, 0);                      
        }    

        if (_UIManager != null)
        {
            _UIManager.UpdateLives(lives);
        }

    }









    // Update is called once per frame
    private void Update()
    {


        Movement();

        if (Input.GetMouseButtonDown(0))                              

            if (Player1 == true) 
            {
                Shoot();
            }
            

        if (Input.GetKeyDown(KeyCode.Space))

            if(Player2 == true)
            {
                Shoot();
            }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))

            if (PlayerX == true)
            {
                Shoot();
            }


        if (_gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }
    }


     
  

    public void ShieldPowerUpOn()
    {
        shieldon = true;
        _Shield_Prefab.SetActive(true);
    }
  
    private void Movement()
    {
            float horizontalPxInput = Input.GetAxis("HorizontalPx");
            float verticalPxInput = Input.GetAxis("VerticalPx");

            float horizontalP1Input = Input.GetAxis("HorizontalP1");
            float verticalP1Input = Input.GetAxis("VerticalP1");
        
            float horizontalP2Input = Input.GetAxis("HorizontalP2");
            float verticalP2Input = Input.GetAxis("VerticalP2");


        if (PlayerX == true)
        {
            if (speedBoost == true)
            {

                transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalPxInput * 1.5f);
                transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalPxInput * 1.5f);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalPxInput);
                transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalPxInput);


            }

            if (transform.position.y > 0)

                transform.position = new Vector3(transform.position.x, 0, 0);

            else if (transform.position.y < -4.2f)

                transform.position = new Vector3(transform.position.x, -4.2f, 0);


            if (transform.position.x > 9.5f)

                transform.position = new Vector3(-9.5f, transform.position.y, 0);

            else if (transform.position.x < -9.5f)

                transform.position = new Vector3(9.5f, transform.position.y, 0);
        }






        if (Player1 ==true)
        {
            if (speedBoost == true)
            {

                transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalP1Input * 1.5f);
                transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalP1Input * 1.5f);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalP1Input);
                transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalP1Input);

             
            }

            if (transform.position.y > 0)

                transform.position = new Vector3(transform.position.x, 0, 0);

            else if (transform.position.y < -4.2f)

                transform.position = new Vector3(transform.position.x, -4.2f, 0);


            if (transform.position.x > 9.5f)

                transform.position = new Vector3(-9.5f, transform.position.y, 0);

            else if (transform.position.x < -9.5f)

                transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        

            if(Player2 ==true)
        
        {
            if (speedBoost == true)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalP2Input * 1.5f);
                transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalP2Input * 1.5f);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalP2Input);
                transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalP2Input);
            }

            if (transform.position.y > 0)

                transform.position = new Vector3(transform.position.x, 0, 0);

            else if (transform.position.y < -4.2f)

                transform.position = new Vector3(transform.position.x, -4.2f, 0);


            if (transform.position.x > 9.5f)

                transform.position = new Vector3(-9.5f, transform.position.y, 0);

            else if (transform.position.x < -9.5f)

                transform.position = new Vector3(9.5f, transform.position.y, 0);
        }

    }

    public void Damage()
    {



        if (shieldon == true)
        {
            shieldon = false;
            _Shield_Prefab.SetActive(false);
            return;
        }


        if (lives<4)
        {
            _FireRight.SetActive(true);
        }
        if (lives < 3)
        {
            _FireLeft.SetActive(true);
        }

        if (lives < 2)
        {
          
            Instantiate(_Player_explosion,transform.position, Quaternion.identity);
         
            IsAlive = false;
            _UIManager.ShowTitileScreen();                   
            StartCoroutine(RestartCall());
            Destroy(this.gameObject);

        }

        if(IsAlive == false)
        {
            _gameManager.gameOver = true;
        }

       

        lives = lives - 1;

        _UIManager.UpdateLives(lives);


    }

    private void Shoot()
    {
        if (Time.time > _CanFire)
        {
            AudioSource.PlayClipAtPoint(_clip,transform.position);
            if (canTripleShoot == true)
            {
                Instantiate(_TripleShoot, transform.position + new Vector3(0, 0, 0), Quaternion.identity);              

            }
            else

                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            _CanFire = Time.time + _FireRate;

        }
    }
    public void TripleShotPowerUpOn()
    {
        canTripleShoot = true;
            StartCoroutine(TripleShootPowerDownRoutine());
    }
    public IEnumerator TripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShoot = false;
    }
    public IEnumerator RestartCall()
    {
        if (IsAlive == false)
        {
            
            yield return new WaitForSeconds(5);
            IsAlive = true;

        }
    }
    public void speedBoostOn()
    {

        
        speedBoost = true;
        {
            StartCoroutine(speedBoostDownRoutine());
        }
    
    
    }
    public IEnumerator speedBoostDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        speedBoost = false;

    }


   

}




