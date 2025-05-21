using UnityEngine;

/// <summary>
/// TODO
/// </summary>
public class SpellProjectile : MonoBehaviour {

    [SerializeField] private WizardProperties wizardProperties;

    private void Update() {
        //purkkaratkasu
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision) {
        //TODO
        //spawn particles 
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null) {
            playerHealth.TakeDamage(wizardProperties.damage);
        }

        Destroy(gameObject);
    }
}
