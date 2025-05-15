using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;
    private CharacterController controller;
    private PlayerMovement playerMovement;

    private void Start() {
        if (Player.instance != null) {
            controller = Player.instance.GetComponent<CharacterController>();
            playerMovement = Player.instance.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            //damage player
            controller.transform.position = respawnPoint.position;
            Physics.SyncTransforms();
        }
    }

    //private IEnumerator TeleportPlayer() {
    //    controller.transform.position = respawnPoint.position;
    //    Physics.SyncTransforms();
    //    yield return new WaitForSeconds(0.5f); 
    //    //playerMovement.enabled = true;
    //    //controller.enabled = true;
    //}
}
