using UnityEngine;

/// <summary>
/// TODO
/// </summary>
public class SpellProjectile : MonoBehaviour {

    [SerializeField] private WizardProperties wizardProperties;
    [SerializeField] private GameObject hitVFX;

    private void Update() {
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision) {
        Instantiate(hitVFX, transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && GameUIManager.infiniteLives == false) {
            playerHealth.TakeDamage(wizardProperties.damage);
            SoundManager.instance.PlaySFX(SFXType.PlayerTakeDamage, Player.instance.transform, 0.8f);
        }

        Destroy(gameObject);
    }
}
