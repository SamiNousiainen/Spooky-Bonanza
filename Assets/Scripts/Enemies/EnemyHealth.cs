using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;
    
    void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            DropCandy();
            Destroy(gameObject);
        }
    }

    public void DropCandy() {
        if (candyPrefab != null) {
            Instantiate(candyPrefab, transform.position, Quaternion.identity);
        }
    }
}
