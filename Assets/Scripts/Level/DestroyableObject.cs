using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[SelectionBase]
public class DestroyableObject : MonoBehaviour, IDamageable {


    [SerializeField] private GameObject brokenObject;
    [SerializeField] private GameObject fullObject;
    [SerializeField] private float hitPoints = 1f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float objectBreakSpeed = 3f;

    [Header("Candy Drop Settings")]
    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private int candyDropAmount = 3;
    [SerializeField] private float spawnForce = 2f;
    [SerializeField] private float spawnRadius = 2f;


    private bool hasExploded = false;
    public void Start() {

    }

    public void TakeDamage(float damage) {
        hitPoints -= damage;
        if (hitPoints <= 0f && hasExploded == false) {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode() {
        hasExploded = true;

        Collider collider = GetComponent<Collider>();

        collider.enabled = false;   

        fullObject.SetActive(false);
        brokenObject.SetActive(true);

        DropCandy();

        Rigidbody[] childRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in childRigidbodies) {
            Vector3 direction = rb.transform.position - transform.position;
            rb.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(7f);

        brokenObject.SetActive(false);
    }

    private void DropCandy() {

        for (int i = 0; i < candyDropAmount; i++) {

            Vector3 offset = Random.insideUnitSphere * spawnRadius;
            offset.y = Mathf.Abs(offset.y);

            GameObject candy = Instantiate(candyPrefab, transform.position + offset, Quaternion.identity);

            Rigidbody rb = candy.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 direction = (offset + Vector3.up).normalized;
                rb.AddForce(direction * spawnForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        
        if (collision == null) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Walkable Surface")) {
            if (collision.relativeVelocity.magnitude >= objectBreakSpeed) {
                TakeDamage(hitPoints);
            }
        }
    }

}
