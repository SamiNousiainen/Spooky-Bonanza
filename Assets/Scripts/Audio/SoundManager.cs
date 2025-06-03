using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource sfxObject2D;
    [SerializeField] private AudioLibrary audioLibrary;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(SFXType sfxType, Transform transform, float volume) {

        AudioClip clip = GetSFXClip(sfxType);

        float randomPitch = Random.Range(0.95f, 1.1f);

        AudioSource audioSource = Instantiate(sfxObject, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = randomPitch;
        audioSource.Play();

        Destroy(audioSource.gameObject, clip.length);
    }

    public void Play2DSFX(SFXType sfxType, float volume) {

        AudioClip clip = GetSFXClip(sfxType);

        AudioSource audioSource = Instantiate(sfxObject2D);
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
