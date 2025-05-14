using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager instance;

    public InventoryData Data { get; private set; } = new InventoryData();

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void AddCandy() {
        Data.candyCount++;
        if (GameUIManager.instance != null) {
            GameUIManager.instance.UpdateCandyAmount();
        }
    }

    public void RemoveCandy(int amount) {
        Data.candyCount -= amount;
        if (GameUIManager.instance != null) {
            GameUIManager.instance.UpdateCandyAmount();
        }
    }

    public void AddPumpkin(string id) {
        if (Data.collectedPumpkins.Contains(id) == false) {
            Data.collectedPumpkins.Add(id);
            Debug.Log("pumpkin ID " + id + " collected");
            if (GameUIManager.instance != null) {
                GameUIManager.instance.UpdatePumpkinAmount();
            }
        }
    }

    public void LoadData(InventoryData data) {
        Data = data;
    }
}
