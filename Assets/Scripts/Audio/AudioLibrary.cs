using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/Audio Library")]
public class AudioLibrary : ScriptableObject {
    [System.Serializable]
    public struct SFXEntry {
        public SFXType type;
        public AudioClip[] clips;
    }
    public SFXEntry[] sfxClips;
}
