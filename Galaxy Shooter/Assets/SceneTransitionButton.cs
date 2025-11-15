using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionButton : MonoBehaviour
{
    [Header("Configurações")]
    public string nextSceneName; // Nome da próxima cena
    public float fadeDuration = 1.5f; // Tempo do fade

    [Header("Referências")]
    public CanvasGroup fadeCanvas; // CanvasGroup com imagem preta para o fade

    private void Start()
    {
        
    }

    IEnumerator LoadNextScene()
    {
        // Fade-in
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        // Carrega a nova cena
        SceneManager.LoadScene(nextSceneName);
    }
}
