using DG.Tweening;
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
        InputSystem.onAnyButtonPress.CallOnce(ctx => StartGame());
    }

    /// <summary>
    /// Load the game scene
    /// </summary>
    public void LoadGameScene() {
        SceneManager.LoadSceneAsync(gameScene);

    }

    /// <summary>
    /// fade screen and load level asynchronously
    /// </summary>
    public void StartGame() {
        backgroundDim.DOFade(1f, 1f).onComplete += () => {
            mainMenuPanel.SetActive(false);
            LoadGameScene();
        };
    }
}