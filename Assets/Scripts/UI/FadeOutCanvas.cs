using UnityEngine;

public class FadeOutCanvas : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float delayTime = 2f;
    public bool autoReverse = false;

    private CanvasGroup canvasGroup;
    private bool fadingIn = true;
    private bool isDelaying = false;
    private float delayTimer = 0f;
    private float fadeSpeed;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup-komponenttia ei löytynyt!");
        }
        fadeSpeed = 1f / fadeDuration;
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (canvasGroup == null) return;

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
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;
                if (autoReverse) StartDelay();
            }
        }
        else
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0f)
            {
                canvasGroup.alpha = 0f;
                if (autoReverse) StartDelay();
                else gameObject.SetActive(false);
            }
        }
    }

    public void FadeIn()
    {
        fadingIn = true;
        isDelaying = false;
        gameObject.SetActive(true);
    }

    public void FadeOut()
    {
        fadingIn = false;
        isDelaying = false;
    }

    private void StartDelay()
    {
        isDelaying = true;
        delayTimer = delayTime;
    }
}

