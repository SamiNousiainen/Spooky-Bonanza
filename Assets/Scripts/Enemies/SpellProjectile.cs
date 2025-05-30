using UnityEngine;

/// <summary>
/// TODO
/// </summary>
public class SpellProjectile : MonoBehaviour {

    [SerializeField] private WizardProperties wizardProperties;
    [SerializeField] private GameObject hitVFX;

    private void Update() {
        //purkkaratkasu
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision) {
        //TODO
        //spawn particles 
        Instantiate(hitVFX, transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && GameUIManager.infiniteLives == false) {
            playerHealth.TakeDamage(wizardProperties.damage);
        }

        Destroy(gameObject);
    }
}
