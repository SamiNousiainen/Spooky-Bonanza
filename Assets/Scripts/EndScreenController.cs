using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// TODO
/// </summary>
public class EndScreenController : MonoBehaviour {

    [SerializeField] private Image image;
    [SerializeField] private TMP_Text totalCandyText;

    private void Update() {
        //target
        //start
        totalCandyText.text = Mathf.Lerp(0f, 250f, Time.deltaTime * 2f).ToString();

    }
}
