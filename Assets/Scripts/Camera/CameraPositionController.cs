using UnityEngine;
using Unity.Cinemachine;

/// <summary>
/// Controls CinemachinePositionComposer values based on player vertical movement,
/// and clamps the camera's X follow position.
/// </summary>
public class CameraPositionController : MonoBehaviour {

    private CinemachinePositionComposer positionComposer;
    private CharacterController characterController;

    [SerializeField] private float defaultOffsetY = 0f;
    [SerializeField] private float fallOffsetY = -1f;

    [SerializeField] private float defaultDampingY = 1f;
    [SerializeField] private float fallDampingY = 0f;

    [SerializeField] private float defaultScreenPosY = 0.18f;
    [SerializeField] private float fallScreenPosY = 0f;

    [SerializeField] private float lerpSpeed = 3f;

    [Header("Clamp Settings")]
    [SerializeField] private Transform followProxy;
    [SerializeField] private float minX = 1f;       // Left
    [SerializeField] private float maxX = 5f;       // Right
    [SerializeField] private float minY = 1f;       // Bottom
    [SerializeField] private float maxY = 6f;       // Top

    private float currentMaxDistance;

    public float MinX => minX;
    public float MaxX => maxX;
    public float MinY => minY;
    public float MaxY => maxY;

    private Transform player;

    private void Start() {
        positionComposer = GetComponent<CinemachinePositionComposer>();
        currentMaxDistance = positionComposer.CameraDistance;

        player = Player.instance != null ? Player.instance.transform : FindAnyObjectByType<Player>()?.transform;
        if (player != null) {
            characterController = player.GetComponent<CharacterController>();
        } else {
            Debug.LogWarning("Player not found");
        }

        //Create follow proxy if not assigned
        if (followProxy == null) {
            GameObject proxyObj = new GameObject("CameraFollowProxy");
            followProxy = proxyObj.transform;
        }

        //Initialize proxy position
        if (player != null) {
            followProxy.position = new Vector3(
                Mathf.Clamp(player.position.x, minX, maxX),
                player.position.y,
                player.position.z
            );
        }

        //Assign proxy as camera follow target
        if (TryGetComponent(out CinemachineCamera cinemachineCamera)) {
            cinemachineCamera.Follow = followProxy;
        } else {
            Debug.LogWarning("CinemachineCamera not found on this GameObject");
        }
    }

    private void Update() {
        if (positionComposer == null || characterController == null || player == null || followProxy == null)
            return;

        //clamp X and Y
        float clampedX = Mathf.Clamp(player.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(player.position.y, minY, maxY);
        followProxy.position = new Vector3(clampedX, clampedY, player.position.z);

        //adjust vertical camera behavior based on falling
        float targetOffset = characterController.velocity.y <= -3f ? fallOffsetY : defaultOffsetY;
        float targetDamping = characterController.velocity.y <= -3f ? fallDampingY : defaultDampingY;
        float targetScreenPos = characterController.velocity.y <= -3f ? fallScreenPosY : defaultScreenPosY;

        Vector3 currentOffset = positionComposer.TargetOffset;
        float currentDamping = positionComposer.Damping.y;
        float currentScreenPos = positionComposer.Composition.ScreenPosition.y;

        currentOffset.y = Mathf.Lerp(currentOffset.y, targetOffset, Time.deltaTime * lerpSpeed);
        currentDamping = Mathf.Lerp(currentDamping, targetDamping, Time.deltaTime * lerpSpeed);
        currentScreenPos = Mathf.Lerp(currentScreenPos, targetScreenPos, Time.deltaTime * 2f);

        positionComposer.TargetOffset = currentOffset;
        positionComposer.Damping.y = currentDamping;
        positionComposer.Composition.ScreenPosition.y = currentScreenPos;

        //prevent camera from going through walls that would get between the player and the camera
        if (followProxy != null) {
            RaycastHit hit;
            Debug.DrawLine(followProxy.transform.position, transform.position, Color.rebeccaPurple);

            Vector3 direction = (transform.position - followProxy.transform.position).normalized;
            float distance = currentMaxDistance;

            if (Physics.Raycast(followProxy.transform.position, direction, out hit, distance, LayerMask.GetMask("Wall"))) {
                positionComposer.CameraDistance = hit.distance;
            } else {
                positionComposer.CameraDistance = Mathf.Lerp(positionComposer.CameraDistance, currentMaxDistance, Time.deltaTime * lerpSpeed);
            }
        }
    }

    public void SetXBounds(float newMinX, float newMaxX) {
        minX = newMinX;
        maxX = newMaxX;
    }

    public void SetYBounds(float newMinY, float newMaxY) {
        minY = newMinY;
        maxY = newMaxY;
    }

    public void SetCameraMaxDistance(float newDistance) {
        currentMaxDistance = newDistance;
    }

}
