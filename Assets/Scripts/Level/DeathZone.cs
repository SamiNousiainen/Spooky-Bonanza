using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;
    private CharacterController player;
    private PlayerMovement playerMovement;

    private void Start() {
        if (Player.instance != null) {
            player = Player.instance.GetComponent<CharacterController>();
            playerMovement = Player.instance.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {          
            StartCoroutine(TeleportPlayer());
            //damage player
            playerMovement.enabled = false;
            player.enabled = false;
        }
    }

    private IEnumerator TeleportPlayer() {
        player.transform.position = respawnPoint.position;
        yield return new WaitForSeconds(0.5f); 
        playerMovement.enabled = true;
        player.enabled = true;
    }
}
