using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Candy : MonoBehaviour {

    [Tooltip("How much score is this candy worth?")]
    [SerializeField] private int candyValue = 1;

    [Header("Magnet behaviour settings")]
    public float attractionRange = 5f;
    public float attractionSpeed = 7f;
    public float snapDistance = 0.5f;

    private Transform player;

    void Start() {
        player = Player.instance.transform;
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
        InventoryManager.instance.AddCandy(candyValue);
        Destroy(gameObject);
        Debug.Log("collected candy with score value of " + candyValue);
    }

}
