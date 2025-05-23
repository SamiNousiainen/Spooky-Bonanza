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
        // start game on any button press
        //InputSystem.onAnyButtonPress.CallOnce(ctx => LoadAndStartGame());
        //InputSystem.onAnyButtonPress.CallOnce(ctx => StartNewGame());
    }

    /// <summary>
    /// Load the game scene
    /// </summary>
    public void LoadGameScene() {
        SceneManager.LoadSceneAsync(gameScene);
    }

    public void StartNewGame() {
        backgroundDim.DOFade(1f, 1f).onComplete += () => {
            mainMenuPanel.SetActive(false);

            string savePath = Application.persistentDataPath + "/save.json";

            if (File.Exists(savePath)) {
                //if player has any saved progress
                SaveSystem.DeleteSave();
                LoadGameScene();
            } else {
                LoadGameScene();
            }
        };
    }

    /// <summary>
    /// fade screen and load level asynchronously
    /// </summary>
    public void LoadAndStartGame() {
        backgroundDim.DOFade(1f, 1f).onComplete += () => {
            mainMenuPanel.SetActive(false);

            string savePath = Application.persistentDataPath + "/save.json";

            if (File.Exists(savePath)) {
                //if player has any saved progress
                SaveSystem.Load();
            } else {
                LoadGameScene();
            }    
        };
    }

}