using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Checkpoint component
/// </summary>
[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(Renderer))]
public class CheckpointBehaviour : MonoBehaviour {

    public string checkpointID;
    private bool checkPointReached;
    //private Material material;

    private void Awake() {
        //material = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && checkPointReached == false) {
            CheckpointManager.instance.ActivateCheckpoint(this);
            checkPointReached = true;

            InventoryManager.instance.Data.sceneName = SceneManager.GetActiveScene().name;
            InventoryManager.instance.Data.checkpointID = checkpointID;

            PlayerSaveData.Save();
            //var color = Color.green;
            //color.a = 0.1f;
            //material.DOColor(color, 0.5f);
            //TODO checkpoint reached fx
            Debug.Log(checkPointReached);
        }
    }
}