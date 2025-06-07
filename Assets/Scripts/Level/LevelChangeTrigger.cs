using UnityEngine;
using System.Collections;

public class LevelChangeTrigger : MonoBehaviour {

    [SerializeField] private string sceneToLoad;

    void Start() {
        
    }

    private void OnTriggerEnter(Collider other) {
       if (other.CompareTag("Player")) {
            StartCoroutine(TriggerSceneChange());
        }
    }

    private IEnumerator TriggerSceneChange() {
        SoundManager.instance.PlaySFX(SFXType.DoorOpen, transform, 0.8f);
        yield return new WaitForSeconds(0.5f);
        if (GameManager.instance != null) {
            GameManager.instance.SceneChange(sceneToLoad);
        }
    }
}
