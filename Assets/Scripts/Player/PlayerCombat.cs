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
    [SerializeField] private GameObject shield;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

    private bool isGliding;
    private bool isBlocking;

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
        isGliding = inputReader.IsGlidePressed;
        isBlocking = inputReader.IsBlockPressed;

        HandleBlock();
        HandleGlide();

        if (isBlocking)
        {
            UpdateShieldPosition();
        }
        //Attack();
    } // Update

    public void Attack()
    {
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        Debug.Log("Attack!");

        float attackRange = 0.5f;
        LayerMask enemyMask = LayerMask.GetMask("Enemy");

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask);
        attackPoint.gameObject.SetActive(true);

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null && damageable.HasTakenDamage == false)
            {
                damageable.TakeDamage(playerProperties.damage);
                damagedEnemies.Add(damageable);
                Debug.Log($"Damaged: {hit.name}");
            }
        }

        yield return new WaitForSeconds(0.1f);
        attackPoint.gameObject.SetActive(false);
        ReturnEnemiesToDamageable();
    }

    private void ReturnEnemiesToDamageable() {
        foreach (IDamageable damagedEnemy in damagedEnemies) {
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

        umbrella.SetActive(isGliding && playerMovement.isGrounded == false);

    } // HandleGlide

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
        Gizmos.DrawWireSphere(attackPoint.position, 0.5f);
    }

} // Class