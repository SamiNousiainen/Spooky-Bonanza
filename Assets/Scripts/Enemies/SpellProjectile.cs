using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

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
            damageable.HasTakenDamage = false;
        }

        //Bounce if the projectile hits umbrella
        if (collision.gameObject.layer == LayerMask.NameToLayer("Block")) {

            Rigidbody rb = GetComponent<Rigidbody>();

            //Block sfx

            //Sateenvarjon pinnan mukaan
            //Vector3 velocity = rb.linearVelocity;
            //Vector3 normal = collision.GetContact(0).normal;
            //rb.linearVelocity = Vector3.Reflect(velocity, normal).normalized * wizardProperties.projectileSpeed;

            //Pelaajan rotaation mukaan
            Vector3 direction = Player.instance.transform.forward;


            //Lähintä vihollista päin

            //TODO
            //Collider[] enemies = Physics.OverlapSphere(transform.position, 10f);

            
            direction.Normalize();

            rb.linearVelocity = direction * wizardProperties.projectileSpeed;
            Quaternion.LookRotation(direction);

        } else {
            //Destroy projectile and spawn vfx
            Instantiate(hitVFX, transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));
            Destroy(gameObject);
        }
    }

    //private Transform GetClosestEnemy(Transform[] enemies) {
        
    //}
}
