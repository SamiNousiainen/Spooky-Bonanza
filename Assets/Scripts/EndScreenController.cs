using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PumpkinDisplayEntry {
    public string id;
    public GameObject collectedPumpkin;
    public GameObject missingPumpkin;
}

public class EndScreenController : MonoBehaviour {

    [Header("Pumpkin Displays")]
    [SerializeField] private List<PumpkinDisplayEntry> pumpkinDisplays;

    [Header("UI")]
    [SerializeField] private TMP_Text totalCandyText;

    private void Start() {
        totalCandyText.text = "Total candy collected: " + InventoryManager.instance.Data.candyCount.ToString();

        List<string> collectedPumpkins = InventoryManager.instance.Data.collectedPumpkins;

        //Loop through each display entry and enable if collected
        foreach (var entry in pumpkinDisplays) {
            bool isCollected = collectedPumpkins.Contains(entry.id);
            entry.collectedPumpkin.SetActive(isCollected);
            entry.missingPumpkin.SetActive(!isCollected);
        }
    }

    public void ReturnToMenu() {
        GameManager.instance.SceneChange("MainMenuScene");
        MusicManager.instance.PlayOnlyBaseLayer();
    }
}
