using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;
    
    void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
