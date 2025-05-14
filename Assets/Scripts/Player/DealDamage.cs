using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerHealth enemy = other.GetComponent<PlayerHealth>();
            enemy.TakeDamage(damage);
        }
    }

}
