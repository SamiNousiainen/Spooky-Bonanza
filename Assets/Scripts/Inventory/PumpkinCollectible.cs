using UnityEngine;

public class PumpkinCollectible : MonoBehaviour {
    [Tooltip("Unique ID for each collectible pumpkin")]
    [SerializeField] private string pumpkinID;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            InventoryManager.instance.AddPumpkin(pumpkinID);
            Destroy(gameObject);
        }
    }
}
