using Unity.Cinemachine;
using UnityEngine;

public class CameraAngleTrigger : MonoBehaviour {

    [SerializeField] private float enterDistance;
    [SerializeField] private float exitDistance;
    private CinemachinePositionComposer positionComposer;

    void Start() {
        positionComposer = FindAnyObjectByType<CinemachinePositionComposer>();
    }

    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            positionComposer.CameraDistance = enterDistance;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            positionComposer.CameraDistance = exitDistance;
        }
    }
}
