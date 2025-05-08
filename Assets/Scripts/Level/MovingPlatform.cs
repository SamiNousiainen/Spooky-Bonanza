using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform[] points;

    private Vector3 targetPosition;
    private int targetIndex = 0;

    private void Awake() {
        if (points.Length > 0) {
            transform.position = points[0].position;
        }
    }

    void FixedUpdate() {    
        Move();
        CheckDirection();
    }

    void Move() {
        if (points.Length == 0) return;
        targetPosition = points[targetIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void CheckDirection() {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            targetIndex = (targetIndex + 1) % points.Length;
        }
    }

    private void OnDrawGizmos() {
        if (points != null) {
            for (int i = 0; i < points.Length - 1; i++) {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            Gizmos.DrawLine(points[points.Length - 1].position, points[0].position);
        }
    }
}
