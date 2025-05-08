using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using KBCore.Refs;

/// <summary>
/// Ghost enemy AI
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[SelectionBase]
public class GhostBehaviour : ValidatedMonoBehaviour {

    [SerializeField] private GhostProperties ghostProperties;

    private EnemyState currentState = EnemyState.Default;
    private GhostPatrolRoute ghostPatrolRoute;

    private bool candyStolen = false;

    //components
    [HideInInspector, SerializeField, Self] private NavMeshAgent agent;

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
                Vector3 fleeDirection = (transform.position - playerPos).normalized;

                int maxAttempts = 10;
                float sampleRadius = 4f;
                
                for (int i = 0; i < maxAttempts; i++) {
                    float fleeDistance = Random.Range(20f, 30f);
                    Vector3 targetPos = transform.position + fleeDirection * fleeDistance;

                    targetPos += Random.insideUnitSphere * 2f;

                    if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, sampleRadius, NavMesh.AllAreas)) {
                        agent.SetDestination(hit.position);
                        currentState = EnemyState.Default;
                        break;
                    }
                }
                break;
        }
    }


    private void Attack() {
        Debug.Log("yoink! Hit the bricks!!");
        currentState = EnemyState.Flee;
        candyStolen = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ghostProperties.detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ghostProperties.attackRange);
    }
}
