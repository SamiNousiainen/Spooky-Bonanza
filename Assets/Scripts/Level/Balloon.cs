using UnityEngine;

public class Balloon : MonoBehaviour {

    [SerializeField] private GameObject popVFX;
    [SerializeField] private GameObject balloon;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            balloon.SetActive(false);
            popVFX.SetActive(true);
            SoundManager.instance.PlaySFX(SFXType.Balloon, transform, 0.8f);
            other.GetComponent<PlayerMovement>().LaunchPlayer(Vector3.up * 5f);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        balloon.SetActive(false);
        popVFX.SetActive(true);
    }
}
