using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GhostBehaviour))]
public class GhostPatrolRoute : MonoBehaviour {

    [Tooltip("Holds transforms that make up the patrol route for this enemy"), SerializeField] private Transform patrolRouteParent;

    private int currentPatrolPoint = -1;
    private bool reversing = false;

    public Vector3 GetNextPatrolPoint() {
        if (reversing == false) {
            currentPatrolPoint++;
        } else {
            currentPatrolPoint--;
            }

        if (currentPatrolPoint == patrolRouteParent.childCount - 1) {
            reversing = true;
        } else if (currentPatrolPoint == 0) {
            reversing = false;
        }

        NavMesh.SamplePosition(patrolRouteParent.GetChild(currentPatrolPoint).position, out NavMeshHit hit, 4f, NavMesh.AllAreas);
        return hit.position;
    }
}
