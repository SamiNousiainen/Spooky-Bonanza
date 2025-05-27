using TMPro;
using UnityEngine;

public class FadeOutText : MonoBehaviour
{
    public float fadeDuration = 2f;
    public float delayTime = 4f;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private bool fadingIn = true;
    [SerializeField] private bool autoReverse = false;

    [SerializeField] private float alphaValue;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float delayTimer = 0f;
    [SerializeField] private bool isDelaying = false;


    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI-komponenttia ei löytynyt!");
            return;
        }

        fadeSpeed = 1f / fadeDuration;
        alphaValue = 0f;
        SetAlpha(alphaValue);
    }

    void Update()
    {
        if (text == null) return;

        if (isDelaying)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
            {
                isDelaying = false;
                fadingIn = !fadingIn;
            }
            return;
        }

        if (fadingIn)
        {
            alphaValue += fadeSpeed * Time.deltaTime;
            if (alphaValue >= 1f)
            {
                alphaValue = 1f;
                SetAlpha(alphaValue);

                if (autoReverse)
                {
                    StartDelay();
                }
            }
        }
        else
        {
            alphaValue -= fadeSpeed * Time.deltaTime;
            if (alphaValue <= 0f)
            {
                alphaValue = 0f;
                SetAlpha(alphaValue);
                gameObject.SetActive(false);

                if (autoReverse)
                {
                    StartDelay();
                }
            }
        }

        SetAlpha(alphaValue);
    }

    public void FadeIn()
    {
        Debug.Log("FadeIn method called");
        fadingIn = true;
        isDelaying = false;
        gameObject.SetActive(true);
    }

    public void FadeOut()
    {
        Debug.Log("FadeOut method called");
        fadingIn = false;
        isDelaying = false;
    }

    void StartDelay()
    {
        isDelaying = true;
        delayTimer = delayTime;
    }

    void SetAlpha(float alpha)
    {
        Color c = text.color;
        text.color = new Color(c.r, c.g, c.b, alpha);
    }
}
