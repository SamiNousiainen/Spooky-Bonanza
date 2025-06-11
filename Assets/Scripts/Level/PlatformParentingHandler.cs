using UnityEngine;

public class PlatformParentingHandler : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (other.transform.parent == null) {
                other.transform.SetParent(transform);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (other.transform.parent == null) {
                other.transform.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
            other.transform.SetParent(null);
    }
}
