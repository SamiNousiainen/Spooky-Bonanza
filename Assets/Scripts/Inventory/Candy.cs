using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Candy : MonoBehaviour {

    //[Tooltip("How much score is this candy worth?")]
    //[SerializeField] private int candyValue = 1;

    [Header("Magnet behaviour settings")]
    public float attractionRange = 5f;
    public float attractionSpeed = 7f;
    public float snapDistance = 0.5f;

    [Header("Visual randomization")]
    [SerializeField] private Mesh[] possibleMeshes;
    [SerializeField] private Material[] possibleMaterials;

    private Transform player;

    void Start() {
        player = Player.instance.transform;

        //pick a random mesh
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (possibleMeshes.Length > 0 && meshFilter != null) {
            meshFilter.mesh = possibleMeshes[Random.Range(0, possibleMeshes.Length)];
        }

        //pick a random material?
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (possibleMaterials.Length > 0 && meshRenderer != null) {
            meshRenderer.material = possibleMaterials[Random.Range(0, possibleMaterials.Length)];
        }
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

}
