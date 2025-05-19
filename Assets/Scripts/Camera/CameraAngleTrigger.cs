using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraAngleTrigger : MonoBehaviour {

    [Header("Target Camera Settings")]
    [SerializeField] private float targetDistance = 10f;
    [SerializeField] private Vector3 targetRotation = new Vector3(60f, 0f, 0f);
    [SerializeField] private float transitionDuration = 1f;

    //[Header("Optional: Tracked Offset")]
    //[SerializeField] private Vector3 targetOffset = Vector3.zero;
    //[SerializeField] private bool useOffset = false;

    private CinemachineCamera cinemachineCamera;
    private CinemachinePositionComposer positionComposer;

    private float originalDistance;
    private Quaternion originalRotation;
    //private Vector3 originalOffset;

    private Coroutine transitionCoroutine;

    private void Start() {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        positionComposer = cinemachineCamera?.GetComponent<CinemachinePositionComposer>();

        if (cinemachineCamera != null) {
            originalRotation = cinemachineCamera.transform.rotation;
        }

        if (positionComposer != null) {
            originalDistance = positionComposer.CameraDistance;
            //originalOffset = positionComposer.TargetOffset;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || cinemachineCamera == null) return;

        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(SmoothTransition(
            targetDistance,
            Quaternion.Euler(targetRotation)
        //useOffset ? targetOffset : positionComposer.TargetOffset
        ));
    }

    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player") || cinemachineCamera == null) return;

        if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        transitionCoroutine = StartCoroutine(SmoothTransition(
            originalDistance,
            originalRotation
        //originalOffset
        ));
    }

    private IEnumerator SmoothTransition(float distance, Quaternion rotation/*, Vector3 offset*/) {
        float t = 0f;

        float startDistance = positionComposer.CameraDistance;
        Quaternion startRotation = cinemachineCamera.transform.rotation;
        //Vector3 startOffset = positionComposer.TargetOffset;

        while (t < transitionDuration) {
            t += Time.deltaTime;
            float progress = t / transitionDuration;

            //Lerp distance
            positionComposer.CameraDistance = Mathf.Lerp(startDistance, distance, progress);

            //Lerp camera rotation
            cinemachineCamera.transform.rotation = Quaternion.Slerp(startRotation, rotation, progress);

            //Lerp tracked object offset
            //if (useOffset) {
            //    positionComposer.TargetOffset = Vector3.Lerp(startOffset, offset, progress);
            //}

            yield return null;
        }

        positionComposer.CameraDistance = distance;
        cinemachineCamera.transform.rotation = rotation;
        //if (useOffset) positionComposer.TargetOffset = offset;
    }
}
