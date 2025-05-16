using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    } // Awake

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameManager.instance.PlayerDeath();
        }
    }

    void Update()
    {

    }
} // Class