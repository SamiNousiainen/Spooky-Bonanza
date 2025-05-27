using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Main Player class
/// </summary>
public class Player : MonoBehaviour {

    public static Player instance;

    [SerializeField] private Transform attackPoint;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        attackPoint.gameObject.SetActive(false);
    }

    private void Start() {
        GameUIManager.instance.UpdatePlayerHp();
    }

    void Update() {

    }
}
