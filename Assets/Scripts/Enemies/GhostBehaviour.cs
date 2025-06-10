using KBCore.Refs;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

/// <summary>
/// Ghost enemy AI
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[SelectionBase]
public class GhostBehaviour : ValidatedMonoBehaviour {

    [Tooltip("How many candies does this ghost steal?")]
    [SerializeField] private int StealAmount;
    [SerializeField] private GhostProperties ghostProperties;
    [SerializeField] private Transform fleeTarget;
    [SerializeField] private GameObject candyStealVFX;
    [SerializeField] private GameObject munchVFX;
    [SerializeField] private GameObject poof;

    [SerializeField] private Material attacking;
    [SerializeField] private Material normal;

    private EnemyState currentState = EnemyState.Default;
    private GhostPatrolRoute ghostPatrolRoute;

    //components
    [HideInInspector, SerializeField, Self] private NavMeshAgent agent;
    
    [HideInInspector] public bool CandyStolen { get; private set; } = false;

    private void Awake() {
        agent.isStopped = true;
        ghostPatrolRoute = GetComponent<GhostPatrolRoute>();
        GetComponentInChildren<Renderer>().material = normal;
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

                if (distanceToPlayer <= ghostProperties.detectionRange && CandyStolen == false) {
                    currentState = EnemyState.Chase;
                    GetComponentInChildren<Renderer>().material = attacking;
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
                    GetComponentInChildren<SuckCandy>().SuckingCandy = true;
                    StartCoroutine(StopAttack());

                    if (agent.remainingDistance <= agent.stoppingDistance) {
                        currentState = EnemyState.Eating;
                        SoundManager.instance.PlaySFX(SFXType.GhostMunch, transform, 0.5f);
                        StartCoroutine(Vanish());
                    }

                } else {
                    Debug.Log("Flee target not assigned!");
                    currentState = EnemyState.Default;
                }

                break;

            case EnemyState.Eating:
                //Menacingly stare at the player while eating
                Look();               
                break;
        }
    }

    private void Look() {

        Vector3 playerPos = Player.instance.transform.position;
        float rotationSpeed = 8f;
        Vector3 direction = (playerPos - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void Attack() {
        if (InventoryManager.instance.Data.candyCount < StealAmount) {
            currentState = EnemyState.Flee;
            Debug.Log("no candy found, escape!");
        } else {
            InventoryManager.instance.RemoveCandy(StealAmount);
            SoundManager.instance.PlaySFX(SFXType.GhostAttack, transform, 0.8f);
            candyStealVFX.SetActive(true);
            Debug.Log("yoink! Hit the bricks!!");
            currentState = EnemyState.Flee;
        }

        CandyStolen = true;
    }

    private IEnumerator StopAttack() {

        yield return new WaitForSeconds(1f);
        GetComponentInChildren<SuckCandy>().SuckingCandy = false;
        GetComponentInChildren<Renderer>().material = normal;
    }

    private IEnumerator Vanish() {
        munchVFX.SetActive(true);
        yield return new WaitForSeconds(3f);
        SoundManager.instance.PlaySFX(SFXType.Poof, transform, 0.8f);
        Instantiate(poof, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ghostProperties.detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ghostProperties.attackRange);
    }
}
