using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager instance;

    public PlayerData Data { get; private set; } = new PlayerData();

    private const int StartingMaxHP = 3;
    private const int CappedMaxHP = 9;
    private const int CandyPerHP = 10;

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
        SetMaxHP();
        if (GameUIManager.instance != null) {
            GameUIManager.instance.UpdateCandyAmount();
        }
    }

    public void SetMaxHP() {
        int newMaxHP = Mathf.Clamp(StartingMaxHP + (Data.candyCount / CandyPerHP), StartingMaxHP, CappedMaxHP);
        Data.maxHealth = newMaxHP;
        //Debug.Log(Data.maxHealth);
        if (Player.instance != null) {
            Player.instance.GetComponent<PlayerHealth>().SetMaxHealth(newMaxHP);
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

    public void LoadData(PlayerData data) {
        Data = data;
    }

    public void ResetData() {
        Data = new PlayerData {
            sceneName = "1-Onboarding",
            maxHealth = 3 
        };
    }

}
