using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource sfxObject2D;
    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {

        var data = InventoryManager.instance.Data;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(data.masterVolume) * 20f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(data.musicVolume) * 20f);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(data.sfxVolume) * 20f);
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

    public void Play2DSFX(SFXType sfxType/*, float volume*/) {

        AudioClip clip = GetSFXClip(sfxType);

        AudioSource audioSource = Instantiate(sfxObject2D);
        audioSource.clip = clip;
        //audioSource.volume = volume;
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
