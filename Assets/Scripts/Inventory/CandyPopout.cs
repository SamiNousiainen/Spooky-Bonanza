using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CandyPopout : MonoBehaviour {

    private Vector3 moveDirection;
    private float moveSpeed;
    private float gravity = 9f;
    private float lifetime = 1f;
    private float timer;

    private Rigidbody rb;

    public void Initialize(Vector3 direction, float speed) {
        moveDirection = direction;
        moveSpeed = speed;
    }

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        timer += Time.fixedDeltaTime;

        moveDirection.y -= gravity * Time.fixedDeltaTime;

        Vector3 nextPos = transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        // Raycast ahead to prevent going through walls/floor
        if (Physics.Raycast(transform.position, moveDirection.normalized, out RaycastHit hit, moveDirection.magnitude * moveSpeed * Time.fixedDeltaTime)) {
            // Hit something – snap slightly above hit point and stop movement
            transform.position = hit.point + hit.normal * 0.05f;
            enabled = false;
            return;
        }

        rb.MovePosition(nextPos);

        if (timer >= lifetime) {
            enabled = false;
        }
    }
}
