using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    [SerializeField] private Transform door1;
    [SerializeField] private Transform door2;
    [SerializeField] private Quaternion door1TargetRotation;
    [SerializeField] private Quaternion door2TargetRotation;

    private void Start() {
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(OpenDoors());
        }
    }

    private IEnumerator OpenDoors() {
        float duration = 0.5f;
        float elapsed = 0f;
        Quaternion door1Start = door1.rotation;
        Quaternion door1End = door1TargetRotation * door1.rotation;
        Quaternion door2Start = door2.rotation;
        Quaternion door2End = door2TargetRotation * door2.rotation;

        while (elapsed < duration) {
            float t = elapsed / duration;
            door1.rotation = Quaternion.Slerp(door1Start, door1End, t);
            door2.rotation = Quaternion.Slerp(door2Start, door2End, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        door1.rotation = door1End;
        door2.rotation = door2End;
    }

}
