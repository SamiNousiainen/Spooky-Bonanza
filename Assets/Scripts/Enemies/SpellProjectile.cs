using UnityEngine;

/// <summary>
/// TODO
/// </summary>
public class SpellProjectile : MonoBehaviour {

    private void Update() {
        //purkkaratkasu
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision) {
        //TODO
        //spawn particles 
        //damage player if hit
        //Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }
}
