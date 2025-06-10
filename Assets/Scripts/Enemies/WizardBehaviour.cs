using KBCore.Refs;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class WizardBehaviour : MonoBehaviour {

    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform castPoint;
    [SerializeField] private WizardProperties wizardProperties;
    [SerializeField] private Animator animator;
    private Transform player;
    private float attackTimer;

    //Components
    [HideInInspector, SerializeField, Self] private NavMeshAgent agent;
    [SerializeField, Self] private Rigidbody rb;

    private EnemyState currentState = EnemyState.Default;
    void Start() {
        if (Player.instance != null) {
            player = Player.instance.transform;
        }
    }

    void Update() {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState) {

            case EnemyState.Default:
                if (distanceToPlayer <= wizardProperties.detectionRange) {
                    currentState = EnemyState.Attack;
                    rb.rotation = Quaternion.LookRotation((player.position - transform.position).normalized);
                    attackTimer = wizardProperties.attackRate;
                }
                break;

            case EnemyState.Attack:

                float rotationSpeed = 8f;
                Vector3 direction = (player.position - transform.position).normalized;            

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0) {
                    animator.SetTrigger("attack");
                    Attack();
                    attackTimer = wizardProperties.attackRate;
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    rb.linearVelocity = Vector3.zero;
                }

                if (distanceToPlayer > wizardProperties.detectionRange) {
                    currentState = EnemyState.Default;
                }
                break;
        }
    }

    private void Attack() {
        Vector3 direction = (player.position - castPoint.position).normalized;
        SoundManager.instance.PlaySFX(SFXType.WizardAttack, transform, 0.8f);
        GameObject spell = Instantiate(spellPrefab, castPoint.position, Quaternion.LookRotation(castPoint.position - player.position));
        spell.transform.parent = castPoint;
        Rigidbody spellRb = spell.GetComponent<Rigidbody>();

        StartCoroutine(LaunchSpell(spellRb, direction));
        

    }

    private IEnumerator LaunchSpell(Rigidbody rigidbody, Vector3 direction) {
        yield return new WaitForSeconds(0.5f);
        if (rigidbody != null) {
            rigidbody.transform.parent = null;
            rigidbody.GetComponent<Collider>().enabled = true;
            rigidbody.linearVelocity = direction * wizardProperties.projectileSpeed;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wizardProperties.detectionRange);
    }
}
