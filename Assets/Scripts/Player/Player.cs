using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Main Player class
/// </summary>
public class Player : MonoBehaviour {

    public static Player instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {

    }

    /// <summary>
    /// TEMPORARY, DELETE LATER
    /// </summary>
    public void Attack() {
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage() {
        Debug.Log("Attack!");

        float attackRange = 2f;
        LayerMask enemyMask = LayerMask.GetMask("Enemy");

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyMask);

        foreach (Collider hit in hits) {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.TakeDamage(1f);
                Debug.Log($"Damaged: {hit.name}");
            }
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Stopped attacking");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

}
