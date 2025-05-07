using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float speed = 1f;

    private Vector3 targetPosition;
    private int direction = 1;

    private void Awake() {
        transform.position = point1.position;
    }

    void FixedUpdate() {
        Move();
        CheckDirection();
    }

    void Move() {
        targetPosition = currentTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void CheckDirection() {
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f) {
            direction *= -1;
        }
    }

    private Vector3 currentTargetPosition() {
        if (direction == 1) {
            return point1.position;
        } else {
            return point2.position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(null);

    }
}
