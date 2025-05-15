using System.Collections;
using UnityEngine;

public class Trapdoor : MonoBehaviour {
    [SerializeField] private Transform door1;
    [SerializeField] private Transform door2;

    private void Start() {
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(TriggerSceneChange());          
        }
    }

    private IEnumerator TriggerSceneChange() {
        StartCoroutine(OpenDoors());
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.SceneChange();

    }

    private IEnumerator OpenDoors() {
        float duration = 0.5f;
        float elapsed = 0f;
        Quaternion door1Start = door1.rotation;
        Quaternion door1End = Quaternion.Euler(0, 0, -86) * door1.rotation;
        Quaternion door2Start = door2.rotation;
        Quaternion door2End = Quaternion.Euler(0, 0, 86) * door2.rotation;

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
