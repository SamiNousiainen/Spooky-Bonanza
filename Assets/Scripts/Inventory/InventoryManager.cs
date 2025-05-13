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

    public void AddCandy(int amount) {

        Data.candyCount += amount;
        //Data.candyCount++;
    }

    public void AddPumpkin(string id) {
        if (Data.collectedPumpkins.Contains(id) == false) {
            Data.collectedPumpkins.Add(id);

        }
    }

    public void LoadData(InventoryData data) {
        Data = data;
    }
}
