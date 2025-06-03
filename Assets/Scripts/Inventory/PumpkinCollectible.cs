using UnityEngine;

public class PumpkinCollectible : MonoBehaviour {
    [Tooltip("Unique ID for each collectible pumpkin")]
    [SerializeField] private string pumpkinID;
    [SerializeField] private GameObject collectVFX;

    private float collectionRadius = 1.5f;
    private Transform player;

    private void Start() {
        if (Player.instance != null) {
            player = Player.instance.transform;
        }
    }

    private void Update() {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= collectionRadius) {
            CollectPumpkin();
        }
    }

    private void CollectPumpkin() {
        SoundManager.instance.Play2DSFX(SFXType.PumpkinCollected, 0.8f);
        InventoryManager.instance.AddPumpkin(pumpkinID);
        gameObject.SetActive(false);
        Instantiate(collectVFX, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, collectionRadius);
    }
}
