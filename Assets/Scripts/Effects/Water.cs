using UnityEngine;

public class Water : MonoBehaviour {

    [SerializeField] private GameObject waterParticles;

    private float cooldown = 0.2f;
    private float timer = 0f;

    private void Update() {
        timer -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (timer < 0 && Player.instance.GetComponent<CharacterController>().velocity.magnitude > 0) {
                Vector3 spawnPosition = other.transform.position;
                spawnPosition.y = transform.position.y;
                Instantiate(waterParticles, spawnPosition, Quaternion.identity);
                SoundManager.instance.PlaySFX(SFXType.PlayerStepWater, other.transform, 0.4f);
                timer = cooldown;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (timer < 0 && Player.instance.GetComponent<CharacterController>().velocity.magnitude > 0) {
                Vector3 spawnPosition = other.transform.position;
                spawnPosition.y = transform.position.y;
                Instantiate(waterParticles, spawnPosition, Quaternion.identity);
                SoundManager.instance.PlaySFX(SFXType.PlayerStepWater, other.transform, 0.4f);
                timer = cooldown;
            }
        }
    }
}
