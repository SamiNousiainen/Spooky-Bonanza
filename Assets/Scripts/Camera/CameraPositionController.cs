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

    [Header("Follow Proxy")]
    [SerializeField] private Transform followProxy; 
    [SerializeField] private float minX = -10f;              //Left bound
    [SerializeField] private float maxX = 10f;               //Right bound

    public float MinX => minX;
    public float MaxX => maxX;

    private Transform player;

    private void Start() {
        positionComposer = GetComponent<CinemachinePositionComposer>();

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

        //Clamp X and follow Y/Z
        float clampedX = Mathf.Clamp(player.position.x, minX, maxX);
        followProxy.position = new Vector3(clampedX, player.position.y, player.position.z);

        //Adjust vertical camera behavior based on falling
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
    }

    public void SetXBounds(float newMinX, float newMaxX) {
        minX = newMinX;
        maxX = newMaxX;
    }
}
