using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;
    private CharacterController controller;

    private void Start() {
        if (Player.instance != null) {
            controller = Player.instance.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //damage player
            StartCoroutine(TeleportPlayer());
        }
    }

    private IEnumerator TeleportPlayer() {
        yield return new WaitForSeconds(0.1f);
        controller.transform.position = respawnPoint.position;
        Physics.SyncTransforms();
    }
}
