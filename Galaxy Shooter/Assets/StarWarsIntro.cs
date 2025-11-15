using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StarWarsIntro : MonoBehaviour
{
    [Header("Configurações do Texto")]
    public RectTransform textTransform;   // Referência ao texto
    public float targetY = 339f;           // Posição final no eixo Y
    public float speed = 20f;              // Velocidade de subida

    [Header("Configurações da Transição")]
    public CanvasGroup fadeCanvas;         // CanvasGroup da imagem preta
    public float fadeDuration = 1.5f;      // Tempo do fade
    public float waitBeforeNextScene = 10f; // Tempo antes de trocar de cena (em segundos)
    public string nextSceneName;           // Nome da próxima cena

    void Start()
    {
        // Garante que o fade começa invisível
        if (fadeCanvas != null)
            fadeCanvas.alpha = 0;

        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        float elapsed = 0f;

        // Enquanto o texto não chegou ao destino
        while (textTransform.anchoredPosition.y < targetY)
        {
            textTransform.anchoredPosition = new Vector2(
                textTransform.anchoredPosition.x,
                Mathf.MoveTowards(textTransform.anchoredPosition.y, targetY, speed * Time.deltaTime)
            );
            elapsed += Time.deltaTime;

            // Se passou o tempo limite, já inicia a transição
            if (elapsed >= waitBeforeNextScene)
                break;

            yield return null;
        }

        // Espera um pouco, se ainda restar tempo
        if (elapsed < waitBeforeNextScene)
            yield return new WaitForSeconds(waitBeforeNextScene - elapsed);

        // Inicia o fade e muda de cena
        yield return StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        if (fadeCanvas != null)
        {
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                fadeCanvas.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
                yield return null;
            }
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
