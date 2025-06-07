using DG.Tweening;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Main menu UI controller
/// </summary>
public class MainMenuController : MonoBehaviour {
    [SerializeField] private string gameScene;
    [SerializeField] private Image backgroundDim;
    [SerializeField] private GameObject mainMenuPanel;


    private void Awake() {
        
    }

    /// <summary>
    /// Delete save data and start a new game
    /// </summary>
    public void StartNewGame() {
        backgroundDim.DOFade(1f, 1f).onComplete += () => {
            mainMenuPanel.SetActive(false);

            string savePath = Application.persistentDataPath + "/save.json";

            if (File.Exists(savePath)) {
                //if player has any saved progress
                SaveSystem.DeleteSave();               
            }
            InventoryManager.instance.ResetData();
            SceneManager.LoadSceneAsync(gameScene);
        };
    }

    /// <summary>
    /// Load saved data and start game
    /// </summary>
    public void LoadAndStartGame() {
        backgroundDim.DOFade(1f, 1f).onComplete += () => {
            mainMenuPanel.SetActive(false);

            string savePath = Application.persistentDataPath + "/save.json";

            if (File.Exists(savePath)) {
                //if player has any saved progress
                SaveSystem.Load();
            } else {
                SceneManager.LoadSceneAsync(gameScene);
            }    
        };
    }

    public void QuitGame() {
        Application.Quit();
    }

}