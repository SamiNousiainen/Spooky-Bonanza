using UnityEngine;
using UnityEngine.UI;

public class InfiniteLives : MonoBehaviour
{
    public Toggle toggle;


    void Start()
    {
        toggle.isOn = GameUIManager.infiniteLives;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        GameUIManager.infiniteLives = isOn;
        Debug.Log("Infinite Lives asetettu arvoon: " + isOn);
    }
}
