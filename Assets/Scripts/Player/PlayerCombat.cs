using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using KBCore.Refs;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField, Self] private PlayerMovement playerMovement;
    [SerializeField] private PlayerProperties playerProperties;
    [SerializeField, Self] private CharacterController characterController;
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private GameObject umbrella;

    private bool isGliding;
    private bool isAttacking;

    [SerializeField] private float glideGravity = 2.0f;

    private void OnValidate()
    {
        this.ValidateRefs();
    } // OnValidate

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    } // Awake

    void Update()
    {
        isGliding = inputReader.IsGlidePressed;
        isAttacking = inputReader.AttackPressed;
        HandleGlide();
        //Attack();
    } // Update

    private void HandleGlide()
    {
        bool isFalling = playerMovement.velocity.y < 0f;

        if (isGliding && playerMovement.isGrounded == false && isFalling)
        {
            playerMovement.ApplyGlide(glideGravity);
        }

        umbrella.SetActive(isGliding && playerMovement.isGrounded == false);

    } // HandleGlide

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

    /*private void Attack()
    {
        if (isAttacking)
        {
            foreach ()
            {
                PlayerHealth enemy = enemyCollider.GetComponent<PlayerHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1f);
                }
            }
        }
    }*/

} // Class
