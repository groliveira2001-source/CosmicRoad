using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class EnemyIA : MonoBehaviour
{

  

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private GameObject _Enemy_Explosion;


    private UIManager _UIManager;

    [SerializeField]
    private AudioClip _Clip;

   

    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.Find("Canvas"). GetComponent<UIManager>();

        
    }

    // Update is called once per frame
    void Update()
    {

        if(_UIManager.ESC==false)
        {
            Destroy(this.gameObject);
        }


        if (_UIManager.EndGame == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);


            float RandonX = Random.Range(-7.0f, 7.0f);



            if (transform.position.y < -6.0f)
            {

                transform.position = new Vector3(RandonX, 15, 0);
            }

        }

        else 
        {
            Destroy(this.gameObject);
            Instantiate(_Enemy_Explosion, transform.position, Quaternion.identity);

        }

        if(_UIManager.score < 100)
        {
            speed = 3;
        }

        if (_UIManager.score > 100)
        {
            speed = 5;
        }

        if (_UIManager.score > 500)
        {
            speed = 8;
        }

        if (_UIManager.score > 2500)
        {
            speed = 10;
        }
        if (_UIManager.score > 4000)
        {
            speed = 15;
        }






    }
    // TAG em laser e inimigo
    // Se o laser encostar no inimigo os dois morrem
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Big Hit");

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            Instantiate(_Enemy_Explosion, transform.position, Quaternion.identity);
            
            
            _UIManager.UpdateScore();

            AudioSource.PlayClipAtPoint(_Clip, transform.position);
          
            Destroy(this.gameObject);
        }

        

        else if (other.tag == "Player")// se encostar no player da dano e morre
        {
            Player BOB = other.GetComponent<Player>();

            if (BOB != null)
            {
                BOB.Damage();
            }

            
            Instantiate(_Enemy_Explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_Clip,transform.position);
            Destroy(this.gameObject);
        }

        
        

    }


}
