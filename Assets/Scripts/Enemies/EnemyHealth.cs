using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {


    //health settings
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth;

    [Header("Candy drop settings")]
    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private int candyDropAmount = 3;
    [SerializeField] private float spawnForce = 2f;
    [SerializeField] private float spawnRadius = 2f;

    public bool HasTakenDamage { get; set; }


    void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        HasTakenDamage = true;
        if (currentHealth <= 0) {
            //vois olla omassa funktiossa
            DropCandy();
            gameObject.SetActive(false);
            //death anim + particle effect
        }
    }

    private void DropCandy() {
        //if (candyPrefabs.Length == 0) return;

        for (int i = 0; i < candyDropAmount; i++) {
            //GameObject prefab = candyPrefabs[Random.Range(0, candyPrefabs.Length)];

            Vector3 offset = Random.insideUnitSphere * spawnRadius;
            offset.y = Mathf.Abs(offset.y); // make sure it's above ground

            GameObject candy = Instantiate(candyPrefab, transform.position + offset, Quaternion.identity);

            Rigidbody rb = candy.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 direction = (offset + Vector3.up).normalized;
                rb.AddForce(direction * spawnForce, ForceMode.Impulse);
            }
        }
    }
}
