using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Main Player class
/// </summary>
public class Player : MonoBehaviour {

    public static Player instance;

    [SerializeField] private Transform attackPoint;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        attackPoint.gameObject.SetActive(false);
        GameUIManager.instance.UpdatePlayerHp();
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

        float attackRange = 0.5f;
        LayerMask enemyMask = LayerMask.GetMask("Enemy");

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
        attackPoint.gameObject.SetActive(true);

        foreach (Collider hit in hits) {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.TakeDamage(1f);
                Debug.Log($"Damaged: {hit.name}");
            }
        }

        yield return new WaitForSeconds(0.1f);
        attackPoint.gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
    }

}
