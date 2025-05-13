using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[SelectionBase]
public class WizardBehaviour : MonoBehaviour {

    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform castPoint;
    [SerializeField] private WizardProperties wizardProperties;
    private Transform player;
    private float attackTimer;

    //Components
    [HideInInspector, SerializeField, Self] private NavMeshAgent agent;

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
                }
                break;

            case EnemyState.Attack:

                float rotationSpeed = 8f;
                Vector3 direction = (player.position - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0) {
                    Attack();
                    attackTimer = wizardProperties.attackRate;
                }

                if (distanceToPlayer > wizardProperties.detectionRange) {
                    currentState = EnemyState.Default;
                }
                break;
        }
    }

    private void Attack() {

        //use animation trigger when anims are done
        Debug.Log("projectile shot!");
        Vector3 direction = (player.position - castPoint.position).normalized;

        GameObject spell = Instantiate(spellPrefab, castPoint.position, Quaternion.identity);
        Rigidbody spellRb = spell.GetComponent<Rigidbody>();

        spellRb.linearVelocity = direction * wizardProperties.projectileSpeed;

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wizardProperties.detectionRange);
    }
}
