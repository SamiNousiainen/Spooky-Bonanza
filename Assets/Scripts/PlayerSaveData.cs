using System.IO;
using UnityEngine;

public static class PlayerSaveData {
    private static string SavePath => Application.persistentDataPath + "/save.json";

    public static void Save() {
        var json = JsonUtility.ToJson(InventoryManager.instance.Data);
        File.WriteAllText(SavePath, json);
        Debug.Log("data saved");
    }

    public static void Load() {
        if (File.Exists(SavePath)) {
            var json = File.ReadAllText(SavePath);
            var loadedData = JsonUtility.FromJson<InventoryData>(json);
            InventoryManager.instance.LoadData(loadedData);
            Debug.Log(loadedData);
        }
    }

    public static void DeleteSave() {
        if (File.Exists(SavePath)) File.Delete(SavePath);
    }
}
