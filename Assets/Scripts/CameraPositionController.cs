using UnityEngine;
using Unity.Cinemachine;
using System.Runtime.CompilerServices;

public class CameraPositionController : MonoBehaviour {

    private CinemachinePositionComposer positionComposer;
    private CharacterController characterController;

    [SerializeField] private float defaultOffsetY = 0f;
    [SerializeField] private float fallOffsetY = -1f;

    [SerializeField] private float defaultDampingY = 1f;
    [SerializeField] private float fallDampingY = 0f;

    [SerializeField] private float defaultScreenPosY = 0.18f;
    [SerializeField] private float fallScreenPosY = 0f;

    [SerializeField] private float lerpSpeed = 5f;

    private void Start() {
        positionComposer = GetComponent<CinemachinePositionComposer>();
        
        var player = Player.instance != null ? Player.instance : FindObjectOfType<Player>();
        if (player != null) {
            characterController = player.GetComponent<CharacterController>();
        } else {
            Debug.LogWarning("Player not found");
        }
    }

    private void Update() {
        if (positionComposer == null || characterController == null) return;

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
}
