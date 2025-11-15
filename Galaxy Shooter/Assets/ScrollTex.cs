using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollText : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float targetY = 339f; // Posição final no eixo Y
    public float speed = 20f; // Velocidade de subida

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Move o texto suavemente até a posição alvo
        if (rectTransform.anchoredPosition.y < targetY)
        {
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                Mathf.MoveTowards(rectTransform.anchoredPosition.y, targetY, speed * Time.deltaTime)
            );
        }
    }
}
