using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int PowerupID;

    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y<-6.0f)
            Destroy(this.gameObject);
    }
    



    private void OnTriggerEnter2D(Collider2D other)
    { 
        Debug.Log("Collision with " + other);

        if (other.tag == "Player")


        {
            Player BOB = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (BOB != null)
            {
                if (PowerupID == 0)//triple shoot
                {
                    BOB.TripleShotPowerUpOn();
                    
                }
                else if (PowerupID == 1)//Super Speed
                {
                    BOB.speedBoostOn();
                   
                }
                else if (PowerupID == 2)//Shield
                {
                   BOB.ShieldPowerUpOn();
                   
                }


                Destroy(this.gameObject);
                

            }


            }

        
    }
}

