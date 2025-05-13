using UnityEngine;

public class PumpkinCollectible : MonoBehaviour {
    [Tooltip("Unique ID for each collectible pumpkin")]
    [SerializeField] private string pumpkinID;

    private float collectionRadius = 1.5f;
    private Transform player;

    private void Start() {
        if (Player.instance != null) {
            player = Player.instance.transform;
        }
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (other.CompareTag("Player")) {
    //        InventoryManager.instance.AddPumpkin(pumpkinID);
    //        Destroy(gameObject);
    //    }
    //}

    private void Update() {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= collectionRadius) {
            CollectPumpkin();
        }
    }

    private void CollectPumpkin() {
        InventoryManager.instance.AddPumpkin(pumpkinID);
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, collectionRadius);
    }
}
