using UnityEngine;

public class EnemyHurtBox : MonoBehaviour {

    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float damage = 1f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            SoundManager.instance.PlaySFX(SFXType.PlayerTakeDamage, Player.instance.transform, 0.8f);
            Vector3 knockbackDirection = other.transform.position - transform.position;
            other.gameObject.GetComponent<PlayerMovement>().Knockback(new Vector3(knockbackDirection.x, 1f, knockbackDirection.z) * knockbackForce);
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
