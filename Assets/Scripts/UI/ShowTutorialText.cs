using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ShowTutorialText : MonoBehaviour
{
    public TextMeshProUGUI sharedTextUI;
    public FadeOutText fadeScript; 
    public float displayTime = 5f;

    private Coroutine showMessageCoroutine;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            Debug.Log("Player entered trigger - FadeIn called");
            isPlayerInside = true;

            if (fadeScript != null)
            {
                fadeScript.FadeIn();
            }
            else
            {
                sharedTextUI.gameObject.SetActive(true);
            }

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
            {
                fadeScript.FadeOut();
            }
            else
            {
                sharedTextUI.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(displayTime);

        if (isPlayerInside)
        {
            if (fadeScript != null)
            {
                fadeScript.FadeOut();
            }
            else
            {
                sharedTextUI.gameObject.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}
