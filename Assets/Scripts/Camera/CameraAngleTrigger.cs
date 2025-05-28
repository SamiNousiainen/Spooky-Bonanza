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
        public float minY = 1f;
        public float maxY = 6f;
    }

    [SerializeField] private float transitionDuration = 1f;

    [Header("Camera Settings")]
    [SerializeField] private CameraSettings frontEntrySettings;
    [SerializeField] private CameraSettings backEntrySettings;

    private CinemachineCamera cinemachineCamera;
    private CinemachinePositionComposer positionComposer;
    private CameraPositionController cameraPositionController;

    private CameraSettings currentSettings = null;
    private Coroutine transitionCoroutine;

    private enum EntryDirection { None, Front, Back }
    private EntryDirection lastEntryDirection = EntryDirection.None;

    private void Start() {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        positionComposer = cinemachineCamera?.GetComponent<CinemachinePositionComposer>();
        cameraPositionController = cinemachineCamera?.GetComponent<CameraPositionController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        Vector3 toPlayer = other.transform.position - transform.position;
        float dot = Vector3.Dot(transform.forward, toPlayer.normalized);

        CameraSettings settings;
        if (dot > 0f) {
            settings = frontEntrySettings;
            lastEntryDirection = EntryDirection.Front;
        } else {
            settings = backEntrySettings;
            lastEntryDirection = EntryDirection.Back;
        }

        if (currentSettings == settings)
            return;

        ApplySettings(settings);
    }

    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;

        Vector3 toPlayer = other.transform.position - transform.position;
        float dot = Vector3.Dot(transform.forward, toPlayer.normalized);

        EntryDirection exitDirection = dot > 0f ? EntryDirection.Front : EntryDirection.Back;

        //Only revert if player exits in the same direction they entered
        if (exitDirection == lastEntryDirection) {
            CameraSettings revertSettings = currentSettings == frontEntrySettings
                ? backEntrySettings
                : frontEntrySettings;

            ApplySettings(revertSettings);

            lastEntryDirection = EntryDirection.None;
        }
    }

    private void ApplySettings(CameraSettings settings) {
        currentSettings = settings;

        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(SmoothTransition(
            settings.targetDistance,
            Quaternion.Euler(settings.targetRotation)
        ));

        if (cameraPositionController != null) {
            cameraPositionController.SetXBounds(settings.minX, settings.maxX);
            cameraPositionController.SetYBounds(settings.minY, settings.maxY);
        }
    }

    private IEnumerator SmoothTransition(float targetDistance, Quaternion targetRotation) {
        float t = 0f;

        float startDistance = positionComposer.CameraDistance;
        Quaternion startRotation = cinemachineCamera.transform.rotation;

        while (t < transitionDuration) {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / transitionDuration);

            positionComposer.CameraDistance = Mathf.Lerp(startDistance, targetDistance, progress);
            cinemachineCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);

            yield return null;
        }

        positionComposer.CameraDistance = targetDistance;
        cinemachineCamera.transform.rotation = targetRotation;

        transitionCoroutine = null;
    }
}
