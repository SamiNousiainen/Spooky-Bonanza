using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem {
    private static string SavePath => Application.persistentDataPath + "/save.json";

    public static void Save() {
        var json = JsonUtility.ToJson(InventoryManager.instance.Data);
        File.WriteAllText(SavePath, json);
        Debug.Log("data saved");
    }

    public static void Load() {
        if (File.Exists(SavePath)) {
            var json = File.ReadAllText(SavePath);
            var loadedData = JsonUtility.FromJson<PlayerData>(json);
            InventoryManager.instance.LoadData(loadedData);
            Debug.Log("inventory data loaded");

            //if (!string.IsNullOrEmpty(loadedData.sceneName)) {
            //    SceneManager.LoadSceneAsync(loadedData.sceneName).completed += (op) => {
            //        //After scene is loaded, player will be repositioned automatically
            //    };
            //}
        }
    }

    public static void DeleteSave() {
        if (File.Exists(SavePath)) File.Delete(SavePath);
    }
}
