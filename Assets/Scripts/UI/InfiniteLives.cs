using UnityEngine;
using UnityEngine.UI;

public class InfiniteLives : MonoBehaviour
{
    public Toggle toggle;


    void Start()
    {
        toggle.isOn = InventoryManager.instance.Data.infiniteLives;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        GameUIManager.infiniteLives = isOn;
        InventoryManager.instance.Data.infiniteLives = isOn;
        SaveSystem.Save();
        Debug.Log("Infinite Lives asetettu arvoon: " + isOn);
    }
}
