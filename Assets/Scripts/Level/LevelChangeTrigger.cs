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
        yield return new WaitForSeconds(1f);
        if (GameManager.instance != null) {
            GameManager.instance.SceneChange(sceneToLoad);
        }
    }
}
