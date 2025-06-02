using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/Audio Library")]
public class AudioLibrary : ScriptableObject {
    [System.Serializable]
    public struct SFXEntry {
        public SFXType type;
        public AudioClip[] clips;
    }

    [System.Serializable]
    public struct MusicEntry {
        public MusicType type;
        public AudioClip clip;
    }

    public SFXEntry[] sfxClips;
    public MusicEntry[] musicClips;
}
