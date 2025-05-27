using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraAngleTrigger : MonoBehaviour {

    [System.Serializable]
    private class CameraSettings {
        public float targetDistance = 10f;
        public Vector3 targetRotation = new Vector3(60f, 0f, 0f);
        public float minX = 1f;
        public float maxX = 5f;
        public float minY = 1;
        public float maxY = 6f;
    }

    [SerializeField] private float transitionDuration = 1f;

    [Header("Camera Settings")]
    [SerializeField] private CameraSettings frontEntrySettings;
    [SerializeField] private CameraSettings backEntrySettings;

    private CinemachineCamera cinemachineCamera;
    private CinemachinePositionComposer positionComposer;
    private CameraPositionController cameraPositionController;

    private float originalDistance;
    private Quaternion originalRotation;
    private float originalMinX;
    private float originalMaxX;
    private float originalMinY;
    private float originalMaxY;

    private Coroutine transitionCoroutine;

    private void Start() {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        positionComposer = cinemachineCamera?.GetComponent<CinemachinePositionComposer>();
        cameraPositionController = cinemachineCamera?.GetComponent<CameraPositionController>();

        if (cinemachineCamera != null) {
            originalRotation = cinemachineCamera.transform.rotation;
        }

        if (positionComposer != null) {
            originalDistance = positionComposer.CameraDistance;
        }

        if (cameraPositionController != null) {
            originalMinX = cameraPositionController.MinX;
            originalMaxX = cameraPositionController.MaxX;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        Vector3 toPlayer = other.transform.position - transform.position;
        float dot = Vector3.Dot(transform.forward, toPlayer.normalized);

        // dot > 0 => entered from front, dot < 0 => entered from back
        CameraSettings settings = dot > 0f ? frontEntrySettings : backEntrySettings;

        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(SmoothTransition(
            settings.targetDistance,
            Quaternion.Euler(settings.targetRotation)
        ));

        if (cameraPositionController != null) {
            cameraPositionController.SetXBounds(settings.minX, settings.maxX);
            cameraPositionController.SetYBounds(settings.minY, settings.maxY);
        }
    }

    //private void OnTriggerExit(Collider other) {
    //    if (!other.CompareTag("Player")) return;

    //    if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
    //    transitionCoroutine = StartCoroutine(SmoothTransition(
    //        originalDistance,
    //        originalRotation
    //    ));

    //    if (cameraPositionController != null) {
    //        cameraPositionController.SetXBounds(originalMinX, originalMaxX);
    //        cameraPositionController.SetYBounds(originalMinY, originalMaxY);
    //    }
    //}

    private IEnumerator SmoothTransition(float distance, Quaternion rotation) {
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
