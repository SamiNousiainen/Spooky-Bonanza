using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {


    //health settings
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth;

    [SerializeField] private GameObject poof;
    [SerializeField] private ParticleSystem damageVFX;

    [Header("Candy drop settings")]
    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private int candyDropAmount = 3;
    [SerializeField] private float spawnForce = 2f;
    [SerializeField] private float spawnRadius = 2f;

    public bool HasTakenDamage { get; set; }


    void Awake() {
        currentHealth = maxHealth;
    }

    void Update() {

    }

    public void TakeDamage(float damage) {
        HasTakenDamage = true;

        currentHealth -= damage;
        PlayDamageSound();
        Knockback();
        if (damageVFX != null) {
            damageVFX.Play();
        }

        if (currentHealth <= 0) {

            DropCandy();
            gameObject.SetActive(false);

            if (poof != null) {
                Instantiate(poof, transform.position, Quaternion.identity);
            }
        }
    }

    private void DropCandy() {

        //Tää on varmaa aika tyhmä ratkasu
        TryGetComponent(out GhostBehaviour ghost);
        if (ghost != null && ghost.CandyStolen == true) {
            candyDropAmount += 3;
        }

        for (int i = 0; i < candyDropAmount; i++) {

            Vector3 offset = Random.insideUnitSphere * spawnRadius;
            offset.y = Mathf.Abs(offset.y);

            GameObject candy = Instantiate(candyPrefab, transform.position + offset, Quaternion.identity);

            Rigidbody rb = candy.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 direction = (offset + Vector3.up).normalized;
                rb.AddForce(direction * spawnForce, ForceMode.Impulse);
            }
        }
    }


    private void PlayDamageSound() {
        if (TryGetComponent(out GhostBehaviour ghost)) {
            SoundManager.instance.PlaySFX(SFXType.GhostDeath, transform, 0.8f);     
        } 

        if (TryGetComponent(out WizardBehaviour wizard)) {
            SoundManager.instance.PlaySFX(SFXType.WizardTakeDamage, transform, 0.8f);
        }
    }

    private void Knockback() {
        Vector3 knockbackDirection = transform.position - Player.instance.transform.position;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) {
            rb.AddForce(knockbackDirection * 3f, ForceMode.Impulse);
        }
    }
}