using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TransformAnimation : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 0f;
    [SerializeField] private float moveDurationMin = 0f;
    [SerializeField] private float moveDurationMax = 0f;
    [SerializeField] private float moveAmountMin = 0f;
    [SerializeField] private float moveAmountMax = 0f;
    [SerializeField] private int startRandomnessSeconds = 0;
    
    async void Start()
    {
        await Task.Delay(Random.Range(0, startRandomnessSeconds*1000));
        transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.WorldAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOLocalMoveY(Random.Range(moveAmountMin, moveAmountMax), Random.Range(moveDurationMin, moveDurationMax)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
