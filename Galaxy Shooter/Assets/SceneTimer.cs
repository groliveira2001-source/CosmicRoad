using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SceneTimer : MonoBehaviour
{
    public float totalTime = 28f;
    float timeRemaining;
    public Image radialImage;   // Image type = Filled (fillAmount)
    public Image barImage;      // optional: adjust rect width for bar
    public RectTransform barFill; // optional: scale x
    public TMP_Text timeText;   // TextMeshPro
    public Color normalColor = Color.green;
    public Color warningColor = new Color(1f, 0.6f, 0f);
    public Color dangerColor = Color.red;
    public AudioSource audioSource;
    public AudioClip warningClip;
    public AudioClip dangerClip;

    bool isRunning = true;
    bool warned10 = false;
    bool warned5 = false;

    public event Action OnTimerEnd;

    void Start()
    {
        timeRemaining = totalTime;
        UpdateVisuals();
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;

        HandleThresholds();
        UpdateVisuals();

        if (timeRemaining <= 0f)
        {
            isRunning = false;
            OnTimerEnd?.Invoke();
        }
    }

    void HandleThresholds()
    {
        if (!warned10 && timeRemaining <= 10f)
        {
            warned10 = true;
            // tocar som + animar
            if (audioSource && warningClip) audioSource.PlayOneShot(warningClip);
            // exemplo: pulse animation (implementar animator)
        }
        if (!warned5 && timeRemaining <= 5f)
        {
            warned5 = true;
            if (audioSource && dangerClip) audioSource.PlayOneShot(dangerClip);
            // intensificar piscar
        }
    }

    void UpdateVisuals()
    {
        float t = Mathf.Clamp01(timeRemaining / totalTime);

        // radial fill
        if (radialImage) radialImage.fillAmount = t;

        // bar fill (scale)
        if (barFill)
        {
            Vector3 s = barFill.localScale;
            s.x = t;
            barFill.localScale = s;
        }

        // color changes
        if (radialImage)
        {
            if (timeRemaining <= 5f) radialImage.color = dangerColor;
            else if (timeRemaining <= 10f) radialImage.color = warningColor;
            else radialImage.color = normalColor;
        }

        // numeric text
        if (timeText)
        {
            int seconds = Mathf.CeilToInt(timeRemaining);
            timeText.text = string.Format("00:{0:00}", seconds);
        }
    }

    // controle externo
    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;
    public void RestartTimer()
    {
        timeRemaining = totalTime;
        warned10 = warned5 = false;
        isRunning = true;
    }
}

