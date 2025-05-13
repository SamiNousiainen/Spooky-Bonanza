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
        Debug.Log("total score:" + Data.candyCount);
        //update score UI here
        GameUIManager.instance.UpdateUI();
    }

    public void RemoveCandy(int amount) {
        Data.candyCount -= amount;
        Debug.Log("Candy removed! total score:" + Data.candyCount);
        //update score UI here
        GameUIManager.instance.UpdateUI();
    }

    public void AddPumpkin(string id) {
        if (Data.collectedPumpkins.Contains(id) == false) {
            Data.collectedPumpkins.Add(id);
            Debug.Log("pumpkin ID " + id + " collected");
            Debug.Log("total pumpkins collected: " + Data.collectedPumpkins.Count);
            //update collectible UI here
            GameUIManager.instance.UpdateUI();
        }
    }

    public void LoadData(InventoryData data) {
        Data = data;
    }
}
