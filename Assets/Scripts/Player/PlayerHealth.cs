using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    public float currentHealth { get; private set; }

    void Awake()
    {
        currentHealth = maxHealth;
    } // Awake

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

    public void ResetHP() {
        currentHealth = maxHealth;
    }
} // Class