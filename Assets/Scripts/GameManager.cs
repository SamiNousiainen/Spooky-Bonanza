
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public UnityEvent OnDeath;
    public UnityEvent OnSceneChange;
    [SerializeField] private Image dim;
    public Image Dim {
        get {
            return dim;
        }
    }

    public float GameTime { get; private set; }

    public void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 120;
    }

    private void Start() {

    }

    private void Update() {
        //GameTime += Time.deltaTime;
    }

    /// <summary>
    /// Manage player death
    /// </summary>
    public void PlayerDeath() {
        Time.timeScale = 0f;
        
        //purkka, korjaa koko flow
        if (dim == null) {
            GameObject newDim = GameObject.Find("Dim");
            dim = newDim.GetComponent<Image>();
        }

        var tween = dim.DOFade(1f, 1f);
        tween.SetUpdate(true); //independent from timescale
        tween.OnComplete(() => {

            OnDeath.Invoke();

            if (CheckpointManager.instance.LastCheckpoint != null) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Player.instance.transform.position = CheckpointManager.instance.LastCheckpoint.transform.position;
                Physics.SyncTransforms();//sync transforms to hopefully avoid CharacterController.Move having incorrect position data after last line

                dim.DOFade(0f, 2f);
                //dim.color = new Color(0f, 0f, 0f, 0f);
                Time.timeScale = 1f;
                

                PlayerHealth playerHealth = Player.instance.GetComponent<PlayerHealth>();

                if (playerHealth != null) {
                    playerHealth.ResetHP();
                }

            } else {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //dim.color = new Color(0f, 0f, 0f, 0f);
                dim.DOFade(0f, 2f);
            }
        });
    }

    public void SceneChange(string sceneName) {
        Time.timeScale = 0f;

        //purkka, korjaa koko flow
        if (dim == null) {
            GameObject newDim = GameObject.Find("Dim");
            dim = newDim.GetComponent<Image>();
        }
        var tween = dim.DOFade(1f, 1f);
        tween.SetUpdate(true);
        tween.OnComplete(() => {

            OnSceneChange.Invoke();
            SceneManager.LoadScene(sceneName);
            dim.DOFade(0f, 2f);
            Time.timeScale = 1f;

        });
    }
}