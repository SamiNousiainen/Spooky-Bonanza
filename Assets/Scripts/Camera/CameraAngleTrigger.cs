using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraAngleTrigger : MonoBehaviour {

    [Header("Target Camera Settings")]
    [SerializeField] private float targetDistance = 10f;
    [SerializeField] private Vector3 targetRotation = new Vector3(60f, 0f, 0f);
    [SerializeField] private float transitionDuration = 1f;

    private CinemachineCamera cinemachineCamera;
    private CinemachinePositionComposer positionComposer;

    private float originalDistance;
    private Quaternion originalRotation;

    private Coroutine transitionCoroutine;

    private void Start() {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        positionComposer = cinemachineCamera?.GetComponent<CinemachinePositionComposer>();

        if (cinemachineCamera != null) {
            originalRotation = cinemachineCamera.transform.rotation;
        }

        if (positionComposer != null) {
            originalDistance = positionComposer.CameraDistance;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || cinemachineCamera == null) return;

        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(SmoothTransition(
            targetDistance,
            Quaternion.Euler(targetRotation)
        ));
    }

    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player") || cinemachineCamera == null) return;

        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(SmoothTransition(
            originalDistance,
            originalRotation
        ));
    }

    private IEnumerator SmoothTransition(float distance, Quaternion rotation/*, Vector3 offset*/) {
        float t = 0f;

        float startDistance = positionComposer.CameraDistance;
        Quaternion startRotation = cinemachineCamera.transform.rotation;

        while (t < transitionDuration) {
            t += Time.deltaTime;
            float progress = t / transitionDuration;

            positionComposer.CameraDistance = Mathf.Lerp(startDistance, distance, progress);

            cinemachineCamera.transform.rotation = Quaternion.Slerp(startRotation, rotation, progress);

            yield return null;
        }

        positionComposer.CameraDistance = distance;
        cinemachineCamera.transform.rotation = rotation;
    }
}