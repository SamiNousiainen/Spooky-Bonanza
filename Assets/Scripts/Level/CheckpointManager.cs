using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public static CheckpointManager instance;

    public CheckpointBehaviour LastCheckpoint { get; private set; }


    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void ActivateCheckpoint(CheckpointBehaviour checkpointBehaviour) {
        LastCheckpoint = checkpointBehaviour;
        //GameUIManager.instance.ShowCheckpointReachedText();
    }
}
