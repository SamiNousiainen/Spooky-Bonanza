using KBCore.Refs;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Self] private CharacterController characterController;
    [SerializeField] private PlayerProperties playerProperties;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private Vector3 velocity;
    private bool isGrounded;

    private void OnValidate() {
        this.ValidateRefs();
    }

    private void Awake() {
        if (cameraTransform == null && Camera.main != null) {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update() {
        HandleMovement();
        HandleJump();
        ApplyGravity();
        GroundCheck();
    }

    private void HandleMovement() {
        Vector2 input = inputReader.MoveInput;

        //Convert input into camera-relative direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = (right * input.x + forward * input.y).normalized;
        Vector3 horizontalVelocity = direction * playerProperties.moveSpeed;

        //Apply horizontal movement only
        characterController.Move(horizontalVelocity * Time.deltaTime);

        if (direction.sqrMagnitude > 0f) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    private void HandleJump() {
        if (inputReader.ConsumeJumpInput() == true && isGrounded == true) {
            velocity.y = Mathf.Sqrt(playerProperties.jumpHeight * -2f * playerProperties.gravityModifier);
        }
    }

    public void LaunchPlayer(float jumpForce) {
        velocity.y = jumpForce;
    }

    private void ApplyGravity() {
        if (isGrounded == true && velocity.y < 0f) {
            velocity.y = -2f;
        }

        velocity.y += playerProperties.gravityModifier * Time.deltaTime;
        characterController.Move(new Vector3(0f, velocity.y, 0f) * Time.deltaTime);
    }

    private void GroundCheck() {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }
}
