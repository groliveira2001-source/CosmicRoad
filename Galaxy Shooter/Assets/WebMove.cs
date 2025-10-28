using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebMove : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float speed = 2f;        // velocidade da descida
    public float spawnY = 1501f;    // posição Y em que a nova imagem será instanciada
    public float destroyY = -2430f; // posição Y em que a imagem atual será destruída

    [Header("Referências")]
    public GameObject imagePrefab;  // prefab da imagem a ser instanciada

    private RectTransform rectTransform;

    void Start()
    {
        // Pega o RectTransform (usado em elementos UI)
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Movimento para baixo (em espaço local)
        rectTransform.anchoredPosition += Vector2.down * speed * Time.deltaTime;

        // Quando sair da tela, instancia uma nova e destrói esta
        if (rectTransform.anchoredPosition.y <= destroyY)
        {
            // Instancia a nova imagem
            GameObject newImage = Instantiate(imagePrefab, rectTransform.parent);

            // Define a posição inicial no eixo Y (mantém o X e Z da atual)
            RectTransform newRect = newImage.GetComponent<RectTransform>();
            newRect.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, spawnY);

            // Destroi a imagem antiga
            Destroy(gameObject);
        }
    }
}
