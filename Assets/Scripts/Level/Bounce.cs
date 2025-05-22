using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

    [SerializeField] private float launchForce = 15f;
    [SerializeField] private Transform mushroom;

    [SerializeField] private float squishAmount = 0.8f;
    [SerializeField] private float squishDuration = 0.2f;

    private Vector3 originalScale;

    private void Awake() {
        if (mushroom == null) {
            mushroom = transform.parent;
        }
        originalScale = mushroom.localScale;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) { 
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            if (player != null) {
                Vector3 launchVelocity = transform.up * launchForce;
                player.LaunchPlayer(launchVelocity);
                StartCoroutine(BounceAnimation());
            }
        }
    }

    private IEnumerator BounceAnimation() {

        Vector3 squishedScale = new Vector3(originalScale.x * 1.1f, originalScale.y * squishAmount, originalScale.z * 1.1f);

        float t = 0f;
        while (t < squishDuration) {
            t += Time.deltaTime;
            mushroom.localScale = Vector3.Lerp(originalScale, squishedScale, t / squishDuration);
            yield return null;
        }

        t = 0f;
        while (t < squishDuration) {
            t += Time.deltaTime;
            mushroom.localScale = Vector3.Lerp(squishedScale, originalScale, t / squishDuration);
            yield return null;
        }

        mushroom.localScale = originalScale;
    }
}
