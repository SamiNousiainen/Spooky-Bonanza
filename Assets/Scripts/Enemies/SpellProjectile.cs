using UnityEngine;

/// <summary>
/// Class containing Wizard enemy spell functionality
/// </summary>
public class SpellProjectile : MonoBehaviour {

    [SerializeField] private WizardProperties wizardProperties;
    [SerializeField] private GameObject hitVFX;

    private void Update() {
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision) {
        Instantiate(hitVFX, transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));
        SoundManager.instance.PlaySFX(SFXType.WizardAttackHit, transform, 0.8f);
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && GameUIManager.infiniteLives == false) {
            playerHealth.TakeDamage(wizardProperties.damage);
            SoundManager.instance.PlaySFX(SFXType.PlayerTakeDamage, Player.instance.transform, 0.8f);
        }

        //Vase breaking (adjust collision layers for friendly fire)
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null) {
            damageable.TakeDamage(wizardProperties.damage);
        }

        Destroy(gameObject);
    }
}
