using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TransformAnimation : MonoBehaviour {
    [SerializeField] private float rotationDuration = 0f;
    [SerializeField] private float moveDurationMin = 0f;
    [SerializeField] private float moveDurationMax = 0f;
    [SerializeField] private float moveAmountMin = 0f;
    [SerializeField] private float moveAmountMax = 0f;
    [SerializeField] private int startRandomnessSeconds = 0;

    private Tween rotationTween;
    private Tween moveTween;

    async void Start() {
        await Task.Delay(Random.Range(0, startRandomnessSeconds * 1000));

        DOTween.SetTweensCapacity(500, 50);

        //Safely start tweens
        if (this != null && gameObject != null) {
            rotationTween = transform.DORotate(
                new Vector3(0, 360, 0),
                rotationDuration,
                RotateMode.WorldAxisAdd
            ).SetLoops(-1).SetEase(Ease.Linear);

            moveTween = transform.DOLocalMoveY(
                Random.Range(moveAmountMin, moveAmountMax),
                Random.Range(moveDurationMin, moveDurationMax)
            ).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }

    void OnDestroy() {
        //Stop tween if active
        if (rotationTween != null && rotationTween.IsActive()) rotationTween.Kill();
        if (moveTween != null && moveTween.IsActive()) moveTween.Kill();
    }
}
