using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Ghost enemy AI
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[SelectionBase]
public class GhostBehaviour : ValidatedMonoBehaviour {

    [SerializeField] private GhostProperties ghostProperties;
    [SerializeField] private Transform fleeTarget;
    [SerializeField] private int stealAmount;

    private EnemyState currentState = EnemyState.Default;
    private GhostPatrolRoute ghostPatrolRoute;

    //components
    [HideInInspector, SerializeField, Self] private NavMeshAgent agent;

    [Tooltip("How many candies does this ghost steal?")]

    private bool candyStolen = false;

    private void Awake() {
        agent.isStopped = true;
        ghostPatrolRoute = GetComponent<GhostPatrolRoute>();
    }

    private void Update() {

        Vector3 playerPos = Player.instance.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, playerPos);

        switch (currentState) {

            case EnemyState.Default:
                if (agent.remainingDistance <= agent.stoppingDistance || agent.isStopped) {
                    agent.speed = ghostProperties.partolMoveSpeed;
                    Vector3 targetPoint = ghostPatrolRoute.GetNextPatrolPoint();
                    agent.SetDestination(targetPoint);
                    Debug.DrawLine(transform.position, targetPoint);
                    agent.isStopped = false;
                }

                if (distanceToPlayer <= ghostProperties.detectionRange && candyStolen == false) {
                    currentState = EnemyState.Chase;
                    Debug.Log("player detected");
                }
                break;

            case EnemyState.Chase:
                agent.speed = ghostProperties.chaseMoveSpeed;
                agent.SetDestination(playerPos);

                if (distanceToPlayer > ghostProperties.detectionRange) {
                    currentState = EnemyState.Default;
                } else if (distanceToPlayer <= ghostProperties.attackRange) {
                    Attack();
                }
                break;

            case EnemyState.Flee:
                agent.speed = ghostProperties.fleeMoveSpeed;

                if (fleeTarget != null) {
                    agent.SetDestination(fleeTarget.position);
                    if (agent.remainingDistance <= agent.stoppingDistance) {
                        gameObject.SetActive(false);
                    }
                } else {
                    Debug.Log("Flee target not assigned!");
                    currentState = EnemyState.Default;
                }
                break;

        }
    }


    private void Attack() {
        if (InventoryManager.instance.Data.candyCount < stealAmount) {
            currentState = EnemyState.Flee;
            Debug.Log("no candy found, escape!");
        } else {
            InventoryManager.instance.RemoveCandy(stealAmount);
            Debug.Log("yoink! Hit the bricks!!");
            currentState = EnemyState.Flee;
        }
        candyStolen = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ghostProperties.detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ghostProperties.attackRange);
    }
}
