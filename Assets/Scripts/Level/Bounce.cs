using UnityEngine;

public class Bounce : MonoBehaviour {

    [SerializeField] private float launchForce = 15f;

    private void OnTriggerEnter(Collider other) {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null) {
            player.LaunchPlayer(launchForce);
        }
    }
}