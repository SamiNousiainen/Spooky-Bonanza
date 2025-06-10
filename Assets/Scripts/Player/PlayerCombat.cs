using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using KBCore.Refs;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField, Self] private PlayerMovement playerMovement;
    [SerializeField] private PlayerProperties playerProperties;
    [SerializeField, Self] private CharacterController characterController;
    [SerializeField, Child] private Animator animator;

    [SerializeField] private Collider weaponCollider;
    [SerializeField] private GameObject umbrella;
    [SerializeField] private GameObject shield;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private ParticleSystem attackVFX;

    private bool isGliding;
    private bool isBlocking;

    private float glideBlockCooldown = 0.5f;
    private float glideEndedTime = -Mathf.Infinity;
    private bool wasGlidingOnLastFrame = false;

    [SerializeField] private float glideDisableDuration = 1f;
    private float glideDisableUntilTime = -Mathf.Infinity;

    private float glideLock = -Mathf.Infinity;

    private bool canAttack = true;
    public float attackCooldown = 0f;

    public bool isAttacking = false;

    public static PlayerCombat instance;
    private List<IDamageable> damagedEnemies = new List<IDamageable>();

    [SerializeField] private float glideGravity = 2.0f;

    private void OnValidate()
    {
        this.ValidateRefs();
    } // OnValidate

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        attackPoint.gameObject.SetActive(false);

    } // Awake

    void Update()
    {

        HandleGlideAndBlockInput();
        HandleBlock();
        HandleGlide();

        wasGlidingOnLastFrame = isGliding;

        if (isBlocking)
        {
            UpdateShieldPosition();
        }
        //Attack();
    } // Update

    public void Attack()
    {
        if (!GameUIManager.IsGameplayInputAllowed()) return;

        if (canAttack && !isAttacking && !isBlocking)
        {
            if (isGliding)
            {
                isGliding = false;
                Debug.Log("Glide cancelled");
            }
            animator.Play("Armature|Attack 0");
            StartCoroutine(DealDamage());
            SoundManager.instance.PlaySFX(SFXType.PlayerAttack, transform, 0.8f);
        }
    }

    private IEnumerator DealDamage()
    {
        canAttack = false;
        isAttacking = true;

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange);
        attackVFX.Play();

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null && damageable.HasTakenDamage == false)
            {
                damageable.TakeDamage(playerProperties.damage);
                SoundManager.instance.PlaySFX(SFXType.PlayerAttackHit, hit.transform, 0.8f);
                damagedEnemies.Add(damageable);
            }
        }

        yield return new WaitForSeconds(0.2f);
        attackVFX.Stop();
        ReturnEnemiesToDamageable();

        isAttacking = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void ReturnEnemiesToDamageable()
    {
        foreach (IDamageable damagedEnemy in damagedEnemies)
        {
            damagedEnemy.HasTakenDamage = false;
        }
        damagedEnemies.Clear();
    }


    private void UpdateShieldPosition()
    {
        // Suunta johon pelaaja katsoo (forward)
        Vector3 forward = transform.forward;

        // K‰‰nnet‰‰n kilpi samaan suuntaan kuin pelaaja
        shield.transform.rotation = Quaternion.LookRotation(forward);
    }

    private void HandleBlock()
    {
        if (!GameUIManager.IsGameplayInputAllowed() || isAttacking)
        {
            playerMovement.canMove = true;
            shield.SetActive(false);
            return;
        }

        if (isBlocking)
        {
            Debug.Log("Block");
            playerMovement.canMove = false;
            shield.SetActive(true);
            shield.GetComponent<SphereCollider>().isTrigger = false;
        }
        else if (!isBlocking)
        {
            playerMovement.canMove = true;
            shield.SetActive(false);
            shield.GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    private void HandleGlide()
    {
        bool isFalling = playerMovement.velocity.y < 0f;

        if (isGliding && playerMovement.isGrounded == false && isFalling)
        {
            playerMovement.ApplyGlide(glideGravity);
        }

        umbrella.SetActive(isGliding && playerMovement.isGrounded == false && !isAttacking);

    } // HandleGlide

    private void HandleGlideAndBlockInput()
    {
        bool buttonPressed = inputReader.GlidePressed;

        if (wasGlidingOnLastFrame && playerMovement.isGrounded)
        {
            glideEndedTime = Time.time;
        }

        bool glideBlockOnCooldown = Time.time < glideEndedTime + glideBlockCooldown;
        bool glideDisableOn = Time.time < glideDisableUntilTime;
        bool glideLockOn = Time.time < glideLock;

        if (buttonPressed && !playerMovement.isGrounded && playerMovement.velocity.y < 0f && !glideDisableOn && !glideLockOn)
        {
            isGliding = true;
            isBlocking = false;
        }

        else if (buttonPressed && playerMovement.isGrounded && !glideBlockOnCooldown && !glideLockOn)
        {
            isBlocking = true;
            isGliding = false;
        }

        else
        {
            isGliding = false;
            isBlocking = false;
        }
    }

    /*public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void GlideCooldown()
    {
        glideDisableUntilTime = Time.time + glideDisableDuration;
        isGliding = false;
    }

    public void LockGlideTemporarily(float duration)
    {
        glideLock = Time.time + duration;
        isGliding = false;
    }

} // Class
