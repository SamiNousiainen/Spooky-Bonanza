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

    private void Start() {
        var data = InventoryManager.instance.Data;

        if (!string.IsNullOrEmpty(data.checkpointID)) {
            RestoreCheckpointByID(data.checkpointID);

            var player = Player.instance;
            if (LastCheckpoint != null) {
                player.transform.position = LastCheckpoint.transform.position;
            }
        }
    }
    public void ActivateCheckpoint(CheckpointBehaviour checkpointBehaviour) {
        LastCheckpoint = checkpointBehaviour;
        //GameUIManager.instance.ShowCheckpointReachedText();
    }

    public void RestoreCheckpointByID(string id) {
        CheckpointBehaviour[] allCheckpoints = FindObjectsByType<CheckpointBehaviour>(FindObjectsSortMode.None);
        foreach (var checkpoint in allCheckpoints) {
            if (checkpoint.checkpointID == id) {
                LastCheckpoint = checkpoint;
                break;
            }
        }
    }

}
