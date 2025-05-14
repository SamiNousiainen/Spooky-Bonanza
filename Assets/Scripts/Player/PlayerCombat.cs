using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using KBCore.Refs;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerProperties playerProperties;
    [SerializeField, Self] private CharacterController characterController;

    private bool isGliding;
    [SerializeField] private float glideGravity = 2.0f;

    private void OnValidate()
    {
        this.ValidateRefs();
    } // OnValidate

    void Awake()
    {

    } // Awake

    void Update()
    {
        isGliding = inputReader.IsGlidePressed;
        HandleGlide();
    } // Update

    private void HandleGlide()
    {
        bool isFalling = playerMovement.velocity.y < 0f;

        if (isGliding && !characterController.isGrounded && isFalling)
        {
            playerMovement.ApplyGlide(glideGravity);
        }

    } // HandleGlide

} // Class
