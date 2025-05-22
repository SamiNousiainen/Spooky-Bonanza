using System;
using DG.Tweening;
using UnityEngine;


/// <summary>
/// Checkpoint component
/// </summary>
[RequireComponent(typeof(Collider))]
//[RequireComponent(typeof(Renderer))]
public class CheckpointBehaviour : MonoBehaviour {
    private bool checkPointReached;
    //private Material material;

    private void Awake() {
        //material = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && checkPointReached == false) {
            CheckpointManager.instance.ActivateCheckpoint(this);
            checkPointReached = true;
            PlayerSaveData.Save();
            //var color = Color.green;
            //color.a = 0.1f;
            //material.DOColor(color, 0.5f);
            //TODO checkpoint reached fx
            Debug.Log(checkPointReached);
        }
    }
}