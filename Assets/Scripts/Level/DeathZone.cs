using System.Collections;
using UnityEngine;
using UnityEngine.LowLevel;

public class DeathZone : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;
    private PlayerHealth playerHealth;
    private float damage = 1f;
    private void Start() {
        if (Player.instance != null) {
            playerHealth = Player.instance.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerHealth.TakeDamage(damage);
            if (playerHealth.currentHealth > 0) {
                StartCoroutine(TeleportPlayer());
            }
        }
    }

    private IEnumerator TeleportPlayer() {
        yield return new WaitForSeconds(0.5f);
        Player.instance.transform.position = respawnPoint.position;
        Physics.SyncTransforms();
    }
}
