using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
/// <summary>
/// TODO, kaikki lev‰ll‰‰n
/// </summary>
public class GameUIManager : MonoBehaviour
{

    public static GameUIManager instance;

    [Header("HUD")]

    [SerializeField] private TMP_Text checkpointText;
    [SerializeField] private TMP_Text candyAmountText;
    [SerializeField] private TMP_Text pumpkinAmountText;
    [SerializeField] private TMP_Text currentHealthText;

    [Header("Pause Menu")]

    public GameObject m_settingsPanel;
    public GameObject pausePanel;
    public GameObject pauseMenu;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Image backgroundDim;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    private PlayerInput playerInput;
    public static bool alwaysMaxJump = false;
    public static bool infiniteLives = false;

    //[SerializeField] private InputReader m_inputReader;
    //[Space(5), Header("Level End")]
    //[SerializeField] private GameObject m_levelEndPanel;
    //[SerializeField] private TMP_Text m_levelEndTimerText;





    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20f);
    }

    private void OnEnable()
    {
        if (playerInput == null) playerInput = new PlayerInput();

        playerInput.Enable();
        playerInput.UI.Pause.performed += context => SetPauseGame(true);
    }

    private void OnDisable()
    {
        if (playerInput == null) { playerInput = new PlayerInput(); }
        playerInput.Disable();
        playerInput.UI.Pause.performed -= context => SetPauseGame(true);
    }

    private void Start()
    {
        UpdatePumpkinAmount();
        UpdateCandyAmount();
        UpdatePlayerHp();
    }

    private void Update()
    {
    }

    public void UpdatePlayerHp()
    {
        if (Player.instance != null)
            currentHealthText.text = Player.instance.GetComponent<PlayerHealth>().currentHealth.ToString();
    }

    public void UpdatePumpkinAmount()
    {
        pumpkinAmountText.text = InventoryManager.instance.Data.collectedPumpkins.Count.ToString();
    }

    public void UpdateCandyAmount()
    {
        candyAmountText.text = InventoryManager.instance.Data.candyCount.ToString();
    }

    #region UI Callbacks

    public void ExitGame()
    {
        var fade = backgroundDim.DOFade(1f, 1f);
        fade.SetUpdate(true);
        fade.onComplete += () => {
            Time.timeScale = 1f;
            SaveSystem.Save();
            SceneManager.LoadSceneAsync("MainMenuScene");
            backgroundDim.DOFade(0f, 1f);
        };
    }

    public void ToggleSettings(bool value)
    {
        pausePanel.SetActive(!value);
        m_settingsPanel.SetActive(value);
    }

    public void ToggleBack(bool backButton)
    {
        m_settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void onToggleMaxJump(bool jumpValue)
    {
        alwaysMaxJump = jumpValue;
    }

    public void OnToggleInfiniteLives(bool lives)
    {
        infiniteLives = lives;
    }

    public static bool IsGameplayInputAllowed()
    {
        return !(instance.pauseMenu.activeSelf || instance.m_settingsPanel.activeSelf);
    }

    public void OnMasterVolumeChanged(float value) {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
    }

    public void OnMusicVolumeChanged(float value) {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
    }

    public void OnSFXVolumeChanged(float value) {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
    }

    #endregion

    /// <summary>
    /// Show/hide pause menu
    /// </summary>
    /// <param name="value"></param>
    public void SetPauseGame(bool value)
    {
        pauseMenu.SetActive(value);
        Time.timeScale = value ? 0f : 1f;
        //Cursor.visible = value;
        //Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    /// <summary>
    /// Show checkpoint reached text
    /// </summary>
    public void ShowCheckpointReachedText()
    {
        checkpointText.color = Color.white;
        checkpointText.DOFade(0f, 2f);
    }

    /// <summary>
    /// Convert linear slider values to logarithmic decibel values
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    //private static float ToLogarithmicVolume(float value) => Mathf.Log10(value) * 20f;


    /// <summary>
    /// Reached end of level, show ui 'n shit
    /// </summary>
    //public void LevelEndReached() {
    //    Time.timeScale = 0f;
    //    GameManager.instance.Dim.color = new Color(1, 1, 1, 0);

    //    var fade = GameManager.instance.Dim.DOFade(1f, 2f);
    //    fade.SetUpdate(true);
    //    fade.onComplete += () => {
    //        GameManager.instance.Dim.color = new Color(1, 1, 1, 0);
    //        m_levelEndTimerText.text = "Your Time: " + FormatTime(GameManager.instance.GameTime);
    //        m_levelEndPanel.SetActive(true);
    //    };
    //}
    //private string FormatTime(float time) {
    //    int minutes = Mathf.FloorToInt(time / 60f);
    //    int seconds = Mathf.FloorToInt(time % 60f);
    //    int milliseconds = Mathf.FloorToInt((time * 100) % 100);
    //    return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    //}
}

