using UnityEngine;
using UnityEngine.UI;

public class ToggleJump : MonoBehaviour
{
    public Toggle toggle;


    void Start()
    {
        toggle.isOn = GameUIManager.alwaysMaxJump;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        GameUIManager.alwaysMaxJump = isOn;
        Debug.Log("alwaysMaxJump asetettu arvoon: " + isOn);
    }
}
