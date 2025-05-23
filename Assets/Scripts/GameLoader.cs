using UnityEngine;

public class GameLoader : MonoBehaviour {

    public static GameLoader instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

    }
    private void Start() {
        var data = InventoryManager.instance.Data;

        if (!string.IsNullOrEmpty(data.checkpointID)) {
            CheckpointManager.instance.RestoreCheckpointByID(data.checkpointID);

            var player = Player.instance;
            if (CheckpointManager.instance.LastCheckpoint != null) {
                player.transform.position = CheckpointManager.instance.LastCheckpoint.transform.position;
            }
        }
    }
}
