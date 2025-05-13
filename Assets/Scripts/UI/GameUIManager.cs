using System.Collections;
using TMPro;
using UnityEngine;
/// <summary>
/// TODO, kaikki lev‰ll‰‰n
/// </summary>
public class GameUIManager : MonoBehaviour {

    public static GameUIManager instance;

    [SerializeField] private TMP_Text candyAmountText;
    [SerializeField] private TMP_Text pumpkinAmountText;
    //[Header("Pause Menu")]
    //[SerializeField] private AudioMixer m_audioMixer;
    //[SerializeField] private VolumeProfile m_globalVolumeProfile;
    //[SerializeField] private GameObject m_settingsPanel;
    //[SerializeField] private GameObject m_pausePanel;
    //[SerializeField] private GameObject m_pauseMenu;
    //[SerializeField] private Image m_backgroundDim;
    //[SerializeField] private Toggle m_sprintToggle;
    //[SerializeField] private Toggle m_sneakToggle;
    //[SerializeField] private Toggle m_cameraDampingToggle;
    //[SerializeField] private InputReader m_inputReader;
    //[SerializeField] private CinemachineCamera m_cinemachineCamera;
    //private CinemachineRotationComposer m_rotationComposer;
    //private LiftGammaGain m_liftGammaGain;

    //[Space(5), Header("Level End")]
    //[SerializeField] private GameObject m_levelEndPanel;
    //[SerializeField] private TMP_Text m_levelEndTimerText;





    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        //InitializeSettings();
    }

    private void OnDisable() {

    }

    private void Start() {
        UpdatePumpkinAmount();
    }

    public void UpdatePumpkinAmount() {
        //pumpkinAmountText.DOFade(1f, 0f);
        pumpkinAmountText.text = "Pumpkins: " + InventoryManager.instance.Data.collectedPumpkins.Count;
        //purkkaratkasu
        //pumpkinAmountText.DOFade(0f, 5f);

    }

    public void UpdateCandyAmount() {
        candyAmountText.text = "Candy: " + InventoryManager.instance.Data.candyCount;

    }

    private IEnumerator FadeUI() {
        //TODO
        yield return new WaitForSeconds(2f);

    }

    #region UI Callbacks

    //public void ExitGame() {
    //    var fade = m_backgroundDim.DOFade(1f, 1f);
    //    fade.SetUpdate(true);
    //    fade.onComplete += () => {
    //        Time.timeScale = 1f;
    //        SceneManager.LoadSceneAsync(0);
    //    };
    //}

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
    //public void SetPauseGame(bool value) {
    //    m_pauseMenu.SetActive(value);
    //    Time.timeScale = value ? 0f : 1f;
    //    Cursor.visible = value;
    //    Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    //}

    /// <summary>
    /// Show checkpoint reached text
    /// </summary>
    //public void ShowCheckpointReachedText() {
    //    m_checkpointText.color = Color.white;
    //    m_checkpointText.DOFade(0f, 2f);
    //}

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
