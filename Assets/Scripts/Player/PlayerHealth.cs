using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth;
    public float currentHealth { get; private set; }

    void Awake()
    {
        
    } // Awake

    private void Start() {
        InventoryManager.instance.SetMaxHP();
        GameUIManager.instance.UpdatePlayerHp();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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