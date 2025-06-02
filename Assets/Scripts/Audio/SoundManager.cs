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
        if (clip == null) {
            Debug.LogWarning($"SFXType {sfxType} not found in AudioLibrary!");
            return;
        }

        AudioSource audioSource = Instantiate(sfxObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, clip.length);
    }

    public void PlayRandomSFX(SFXType[] sfxTypes, Transform transform, float volume = 1f) {
        if (sfxTypes == null || sfxTypes.Length == 0) {
            Debug.LogWarning("No SFXTypes provided to PlayRandomSFX.");
            return;
        }

        int randomIndex = Random.Range(0, sfxTypes.Length);
        SFXType chosenType = sfxTypes[randomIndex];

        AudioClip clip = GetSFXClip(chosenType);
        if (clip == null) {
            Debug.LogWarning($"SFXType {chosenType} not found in AudioLibrary!");
            return;
        }

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
