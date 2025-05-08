using UnityEngine;

public class PlatformParentingHandler : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(null);
    }
}
