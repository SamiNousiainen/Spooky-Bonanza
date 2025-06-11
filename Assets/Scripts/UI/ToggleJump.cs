using UnityEngine;
using UnityEngine.UI;

public class ToggleJump : MonoBehaviour
{
    public Toggle toggle;


    void Start()
    {
        toggle.isOn = InventoryManager.instance.Data.alwaysMaxJump;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        GameUIManager.alwaysMaxJump = isOn;
        InventoryManager.instance.Data.alwaysMaxJump = isOn;
        SaveSystem.Save();
        Debug.Log("alwaysMaxJump asetettu arvoon: " + isOn);
    }
}
