using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Checkpoint component
/// </summary>
[RequireComponent(typeof(Collider))]
public class CheckpointBehaviour : MonoBehaviour {

    [SerializeField] private GameObject activationVFX;
    [SerializeField] private GameObject highlightVFX;

    public string checkpointID;
    private bool checkPointReached;

    private void Awake() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && checkPointReached == false) {
            CheckpointManager.instance.ActivateCheckpoint(this);
            checkPointReached = true;

            activationVFX.SetActive(true);
            highlightVFX.SetActive(false);

            SoundManager.instance.PlaySFX(SFXType.Checkpoint, Player.instance.transform, 0.8f);

            InventoryManager.instance.SetMaxHP();
            InventoryManager.instance.Data.sceneName = SceneManager.GetActiveScene().name;
            InventoryManager.instance.Data.checkpointID = checkpointID;

            SaveSystem.Save();
        }
    }
}