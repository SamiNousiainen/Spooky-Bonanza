using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth;
    public float currentHealth { get; private set; }

    private float damageCooldown = 0.5f;
    private float damageCooldownTimer;

    void Awake()
    {
        
    } // Awake

    private void Start() {
        InventoryManager.instance.SetMaxHP();
        GameUIManager.instance.UpdatePlayerHp();
    }

    public void TakeDamage(float damage)
    {
        if (GameUIManager.infiniteLives) return;

        if (damageCooldownTimer <= 0) {
            currentHealth -= damage;
            damageCooldownTimer = damageCooldown;
        }
        GameUIManager.instance.UpdatePlayerHp();

        if (currentHealth <= 0)
        {
            GameManager.instance.PlayerDeath();
        }
    }

    void Update()
    {

    }

    public void SetMaxHealth(int newMax) {
        maxHealth = newMax;
        currentHealth = maxHealth;
        GameUIManager.instance.UpdatePlayerHp();
    }

    public void ResetHP() {
        currentHealth = maxHealth;
        GameUIManager.instance.UpdatePlayerHp();
    }
} // Class