using UnityEngine;

public class Bounce : MonoBehaviour {

    [SerializeField] private float launchForce = 15f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) { 
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            if (player != null) {
                Vector3 launchVelocity = transform.up * launchForce;
                player.LaunchPlayer(launchVelocity);
            }
        }
    }
}
