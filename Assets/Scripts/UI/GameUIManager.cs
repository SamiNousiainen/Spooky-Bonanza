using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine.SceneManagement;
/// <summary>
/// TODO, kaikki lev‰ll‰‰n
/// </summary>
public class GameUIManager : MonoBehaviour {

    public static GameUIManager instance;

    [Header("HUD")]

    [SerializeField] private TMP_Text candyAmountText;
    [SerializeField] private TMP_Text pumpkinAmountText;
    [SerializeField] private TMP_Text currentHealthText;
    private PlayerHealth playerHp;

    [Header("Pause Menu")]

    //[SerializeField] private AudioMixer m_audioMixer;
    //[SerializeField] private VolumeProfile m_globalVolumeProfile;
    //[SerializeField] private GameObject m_settingsPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image backgroundDim;
    //[SerializeField] private Toggle m_sprintToggle;
    //[SerializeField] private Toggle m_sneakToggle;
    //[SerializeField] private Toggle m_cameraDampingToggle;
    //[SerializeField] private InputReader m_inputReader;
    //[SerializeField] private CinemachineCamera m_cinemachineCamera;
    private PlayerInput playerInput;
    //private LiftGammaGain m_liftGammaGain;

    //[Space(5), Header("Level End")]
    //[SerializeField] private GameObject m_levelEndPanel;
    //[SerializeField] private TMP_Text m_levelEndTimerText;





    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        if (playerInput == null) playerInput = new PlayerInput();

        playerInput.Enable();
        playerInput.UI.Pause.performed += context => SetPauseGame(true);
    }

    private void OnDisable() {
        if (playerInput == null) { playerInput = new PlayerInput(); }
        playerInput.Disable();
        playerInput.UI.Pause.performed -= context => SetPauseGame(true);
    }

    private void Start() {
        UpdatePumpkinAmount();
        UpdateCandyAmount();
        UpdatePlayerHp();
    }

    private void Update() {
    }

    public void UpdatePlayerHp() {
        currentHealthText.text = Player.instance.GetComponent<PlayerHealth>().currentHealth.ToString();
    }

    public void UpdatePumpkinAmount() {
        //pumpkinAmountText.DOFade(1f, 0f);
        pumpkinAmountText.text = InventoryManager.instance.Data.collectedPumpkins.Count.ToString();
        //purkkaratkasu
        //pumpkinAmountText.DOFade(0f, 5f);

    }

    public void UpdateCandyAmount() {
        candyAmountText.text = InventoryManager.instance.Data.candyCount.ToString();

    }

    private IEnumerator FadeUI() {
        //TODO
        yield return new WaitForSeconds(2f);

    }

    #region UI Callbacks

    public void ExitGame() {
        var fade = backgroundDim.DOFade(1f, 1f);
        fade.SetUpdate(true);
        fade.onComplete += () => {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("MainMenuScene");
            backgroundDim.DOFade(0f, 1f);
        };
    }

    //public void ToggleSettings(bool value) {
    //    m_pausePanel.SetActive(!value);
    //    m_settingsPanel.SetActive(value);
    //}

    //public void OnMasterVolumeChanged(float value) {
    //    m_audioMixer.SetFloat("MasterVolume", ToLogarithmicVolume(value));
    //}

    //public void OnAmbientVolumeChanged(float value) {
    //    m_audioMixer.SetFloat("AmbientVolume", ToLogarithmicVolume(value));
    //}

    //public void OnSFXVolumeChanged(float value) {
    //    m_audioMixer.SetFloat("SFXVolume", ToLogarithmicVolume(value));
    //}

    //public void OnGammaChanged(float value) {
    //    m_liftGammaGain.gamma.Override(new Vector4(1f, 1f, 1f, value));
    //}



    //private void InitializeSettings() {

    //}

    //TODO
    //bindings

    #endregion

    /// <summary>
    /// Show/hide pause menu
    /// </summary>
    /// <param name="value"></param>
    public void SetPauseGame(bool value) {
        pauseMenu.SetActive(value);
        Time.timeScale = value ? 0f : 1f;
        //Cursor.visible = value;
        //Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    /// <summary>
    /// Show checkpoint reached text
    /// </summary>
    public void ShowCheckpointReachedText() {
        //m_checkpointText.color = Color.white;
        //m_checkpointText.DOFade(0f, 2f);
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
