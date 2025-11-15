using UnityEngine;
using UnityEngine.UI;
using TMPro; // se usar TextMeshPro

public class RadialTimerController : MonoBehaviour
{
    public Image radialImage;    // Image configurada como Filled > Radial 360
    public float totalTime = 28f;
    float timeRemaining;
    public TMP_Text timeText;    // opcional: referência para mostrar texto "00:28"
    public Color normalColor = Color.green;
    public Color warningColor = new Color(1f, 0.6f, 0f);
    public Color dangerColor = Color.red;

    void Start()
    {
        timeRemaining = totalTime;
        UpdateVisuals();
    }

    void Update()
    {
        if (timeRemaining <= 0f) return;
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;
        UpdateVisuals();

        if (timeRemaining == 0f)
        {
            // evento de fim - implementar conforme necessidade
            Debug.Log("Timer fim");
        }
    }

    void UpdateVisuals()
    {
        float t = Mathf.Clamp01(timeRemaining / totalTime);
        if (radialImage) radialImage.fillAmount = t;

        // cores por thresholds
        if (radialImage)
        {
            if (timeRemaining <= 5f) radialImage.color = dangerColor;
            else if (timeRemaining <= 10f) radialImage.color = warningColor;
            else radialImage.color = normalColor;
        }

        if (timeText)
        {
            int secs = Mathf.CeilToInt(timeRemaining);
            timeText.text = $"00:{secs:00}";
        }
    }

    // métodos públicos para pausar/reiniciar
    public void Pause() => enabled = false;
    public void Resume() => enabled = true;
    public void Restart()
    {
        timeRemaining = totalTime;
        enabled = true;
        UpdateVisuals();
    }
}
