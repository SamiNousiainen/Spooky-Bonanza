using UnityEngine;

public class Balloon : MonoBehaviour, IDamageable {

    [SerializeField] private GameObject popVFX;
    [SerializeField] private GameObject balloon;
    [SerializeField] private float hitPoints = 1f;

    public bool HasTakenDamage { get; set; }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Pop();           
            other.GetComponent<PlayerMovement>().LaunchPlayer(Vector3.up * 5f);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Pop();
    }

    public void TakeDamage(float damage) {
        hitPoints -= damage;
        if (hitPoints <= 0f) {
            Pop();
        }
    }

    private void Pop() {
        balloon.SetActive(false);
        popVFX.SetActive(true);
        SoundManager.instance.PlaySFX(SFXType.Balloon, transform, 0.8f);
    }
}
