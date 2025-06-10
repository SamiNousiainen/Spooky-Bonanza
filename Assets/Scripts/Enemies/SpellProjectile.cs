using System.Collections.Generic;
using System.Collections;
using UnityEngine;


/// <summary>
/// Class containing Wizard enemy spell functionality
/// </summary>
public class SpellProjectile : MonoBehaviour {

    [SerializeField] private WizardProperties wizardProperties;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject blockVFX;

    private void Update() {
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision) {

        SoundManager.instance.PlaySFX(SFXType.WizardAttackHit, transform, 0.8f);
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && GameUIManager.infiniteLives == false) {
            playerHealth.TakeDamage(wizardProperties.damage);
            SoundManager.instance.PlaySFX(SFXType.PlayerTakeDamage, Player.instance.transform, 0.8f);
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null) {
            damageable.TakeDamage(wizardProperties.damage);
            damageable.HasTakenDamage = false;
        }

        //Bounce if the projectile hits umbrella
        if (collision.gameObject.layer == LayerMask.NameToLayer("Block")) {

            Rigidbody rb = GetComponent<Rigidbody>();

            SoundManager.instance.PlaySFX(SFXType.PlayerBlock, transform, 0.8f);
            Instantiate(blockVFX, transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));

            //Bounce based on umbrella collider normal
            //Vector3 velocity = rb.linearVelocity;
            //Vector3 normal = collision.GetContact(0).normal;
            //rb.linearVelocity = Vector3.Reflect(velocity, normal).normalized * wizardProperties.projectileSpeed;

            //Bounce based on player rotation
            //Vector3 direction = Player.instance.transform.forward;


            //Bounce towards closest enemy
            WizardBehaviour closestEnemy = FindClosestEnemy(transform.position, 10f);
            Vector3 direction = closestEnemy.transform.position - transform.position;

            //Slight offset to possibly make wizards spin when hit (funny)
            Vector3 offset = new Vector3(Random.Range(0.1f, 0.3f), 0f, Random.Range(0.1f, 0.3f));
            direction += offset;

            direction.Normalize();

            rb.linearVelocity = direction * wizardProperties.projectileSpeed;
            transform.rotation = Quaternion.LookRotation(-direction);

        } else {
            //Destroy projectile and spawn vfx
            Instantiate(hitVFX, transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));
            Destroy(gameObject);
        }
    }

    public WizardBehaviour FindClosestEnemy(Vector3 position, float radius) {
        Collider[] enemies = Physics.OverlapSphere(position, radius);
        WizardBehaviour closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider collider in enemies) {
            WizardBehaviour enemy = collider.GetComponent<WizardBehaviour>();
            if (enemy != null) {
                float dist = Vector3.Distance(position, collider.transform.position);
                if (dist < shortestDistance) {
                    shortestDistance = dist;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
