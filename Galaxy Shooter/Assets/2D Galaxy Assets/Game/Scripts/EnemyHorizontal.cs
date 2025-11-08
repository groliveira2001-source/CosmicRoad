using System.Collections;
using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip explosionClip;

    private UIManager _uiManager;

    private int direction = 1; // 1 = direita, -1 = esquerda
    private float minY = -3.8f; // altura onde ele se move
    private float maxX = 10f; // limites de tela

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        // Decide o lado de onde vai aparecer
        if (Random.value > 0.5f)
        {
            transform.position = new Vector3(-maxX, minY, 0);
            direction = 1;
        }
        else
        {
            transform.position = new Vector3(maxX, minY, 0);
            direction = -1;
        }
    }

    void Update()
    {
        if (_uiManager.EndGame)
        {
            Destroy(gameObject);
            return;
        }

        // Movimento lateral
        transform.Translate(Vector3.right * speed * Time.deltaTime * direction);

        // Se sair da tela, destrói
        if (Mathf.Abs(transform.position.x) > maxX + 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")// se encostar no player da dano e morre
        {
            Player BOB = other.GetComponent<Player>();

            if (BOB != null)
            {
                BOB.Damage();
            }


            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionClip, transform.position);
            Destroy(this.gameObject);
        }
    }
}

