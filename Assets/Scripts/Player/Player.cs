using UnityEngine;

/// <summary>
/// Main Player class
/// </summary>
public class Player : MonoBehaviour {

    public static Player instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        
    }
}
