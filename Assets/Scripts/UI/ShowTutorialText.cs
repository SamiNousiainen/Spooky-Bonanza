using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ShowTutorialText : MonoBehaviour
{
    public TextMeshProUGUI sharedTextUI;
    public FadeOutText fadeScript;
    public float displayTime = 3f;

    [SerializeField] private List<CanvasGroup> keybindIcons;
    [SerializeField] private float keybindFadeDuration = 0.5f;
    [SerializeField] private float keybindInterval = 1f;

    private Coroutine showMessageCoroutine;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;

            if (fadeScript != null)
                fadeScript.FadeIn();
            else
                sharedTextUI.gameObject.SetActive(true);

            showMessageCoroutine = StartCoroutine(ShowMessage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            if (showMessageCoroutine != null)
            {
                StopCoroutine(showMessageCoroutine);
                showMessageCoroutine = null;
            }

            if (fadeScript != null)
                fadeScript.FadeOut();
            else
                sharedTextUI.gameObject.SetActive(false);

            StartCoroutine(FadeOutKeybinds());
        }
    }

    IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(FadeInKeybindsOneByOne());

        yield return new WaitForSeconds(displayTime);

        if (isPlayerInside)
        {
            if (fadeScript != null)
                fadeScript.FadeOut();
            else
                sharedTextUI.gameObject.SetActive(false);

            StartCoroutine(FadeOutKeybinds());

            Destroy(gameObject, keybindFadeDuration + 0.1f);
        }
    }

    private IEnumerator FadeInKeybindsOneByOne()
    {
        for (int i = 0; i < keybindIcons.Count; i++)
        {
            CanvasGroup cg = keybindIcons[i];
            cg.gameObject.SetActive(true);
            StartCoroutine(FadeCanvasGroup(cg, 0f, 1f, keybindFadeDuration));
            yield return new WaitForSeconds(keybindInterval);
        }
    }

    private IEnumerator FadeOutKeybinds()
    {
        foreach (CanvasGroup cg in keybindIcons)
        {
            StartCoroutine(FadeCanvasGroup(cg, 1f, 0f, keybindFadeDuration));
        }

        yield return new WaitForSeconds(keybindFadeDuration);

        foreach (CanvasGroup cg in keybindIcons)
        {
            cg.gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float time = 0f;
        cg.alpha = start;

        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        cg.alpha = end;
    }
}