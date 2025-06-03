using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioLibrary audioLibrary;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(SFXType sfxType, Transform transform, float volume = 1f) {

        AudioClip clip = GetSFXClip(sfxType);

        AudioSource audioSource = Instantiate(sfxObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, clip.length);
    }


    private AudioClip GetSFXClip(SFXType sfxType) {
        foreach (var entry in audioLibrary.sfxClips) {
            if (entry.type == sfxType && entry.clips != null && entry.clips.Length > 0) {
                return entry.clips[Random.Range(0, entry.clips.Length)];
            }
        }
        return null;
    }
}
