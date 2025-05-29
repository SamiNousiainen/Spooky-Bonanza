using System.Collections;
using KBCore.Refs;
using UnityEngine;

public class Candy : MonoBehaviour {

    //[Tooltip("How much score is this candy worth?")]
    //[SerializeField] private int candyValue = 1;

    [Header("Magnet behaviour settings")]
    public float attractionRange = 5f;
    public float attractionSpeed = 7f;
    public float snapDistance = 0.5f;

    [Header("Visual randomization")]
    [SerializeField] private Mesh[] possibleMeshes;
    [HideInInspector, SerializeField, Self] private Rigidbody rb;

    private Transform player;

    private void OnValidate() {
       this.ValidateRefs();
    }

    void Start() {
        player = Player.instance.transform;

        //pick a random mesh
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        if (possibleMeshes.Length > 0 && meshFilter != null) {
            meshFilter.mesh = possibleMeshes[Random.Range(0, possibleMeshes.Length)];
        }
        StartCoroutine(SetToKinematic());
    }

    void Update() {
        if (player != null) {

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attractionRange) {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * attractionSpeed * Time.deltaTime;

                if (distance <= snapDistance) {
                    CollectCandy();
                }
            }
        }
    }

    private void CollectCandy() {
        InventoryManager.instance.AddCandy();
        Destroy(gameObject);
    }

    private IEnumerator SetToKinematic() {
        yield return new WaitForSeconds(1);

        if (rb.isKinematic == false) {
            rb.isKinematic = true;
        }
    }

}