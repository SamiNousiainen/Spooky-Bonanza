using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using KBCore.Refs;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField, Self] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerProperties playerProperties;

    //private Vector2 currentMovementInput;
    //private Vector3 currentMovement;
    private bool isMovementPressed;
    private Vector3 velocity;

    //private float rotationFactorPerFrame = 15f;
    private float gravity = -9.8f;
    private float groundedGravity = -.05f;

    private bool isJumpPressed = false;
    private float initialJumpVelocity;
    private float maxJumpHeight = 4.0f;
    private float maxJumpTime = 0.75f;
    //private bool isJumping = false;
    //private float timeToApex;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        //animator = GetComponent<Animator>();

        //isWalkingHash = Animator.StringToHash("isWalking");
        //isRunningHash = Animator.StringToHash("isRunning");
        // = Animator.StringToHash("isJumping");

        SetUpJumpVariable();
    } // Awake

    public void LaunchPlayer(float jumpForce)
    {
        velocity.y = jumpForce;
    }

    void HandleGravity()
    {
        bool isFalling = velocity.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (characterController.isGrounded && velocity.y < 0f)
        {
            /*if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }*/
            velocity.y = -2f;
        }
        velocity.y += playerProperties.gravityModifier * Time.deltaTime;
        characterController.Move(new Vector3(0f, velocity.y, 0f) * Time.deltaTime);

        /*else if (isFalling)
        {
            float previousYVelocity = velocity.y;
            float newYVelocity = velocity.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * .5f, -20.0f);
            velocity.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = velocity.y;
            float newYVelocity = velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            velocity.y = nextYVelocity;
        }*/
    } //HandleGravity

    void Update()
    {
        HandleMovement();
        //HandleAnimation();
        HandleGravity();
        HandleJump();
    } // Update
    void SetUpJumpVariable()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    } // SetUpJumpVariable

    void HandleMovement()
    {
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

        if (direction.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    } // HandleRotation
    void HandleJump()
    {
        if (inputReader.ConsumeJumpInput() == true && characterController.isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(playerProperties.jumpHeight * -2f * playerProperties.gravityModifier);
        }

        /*if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            //animator.SetBool(isJumpingHash, true);
            //isJumpAnimating = true;
            isJumping = true;
            velocity.y = initialJumpVelocity * .5f;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;*/
        }
    } // HandleJump

    /*void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    } // OnMovementInput*/
    

/*using KBCore.Refs;
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
        RoofCheck();
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
         isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, LayerMask.GetMask("Walkable Surface"));
     }

     private void RoofCheck() {
         RaycastHit hit;
         if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.6f)) {
             velocity.y = -2f;
         }
     }

     private void OnDrawGizmos() {
         Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
     }
}*/
