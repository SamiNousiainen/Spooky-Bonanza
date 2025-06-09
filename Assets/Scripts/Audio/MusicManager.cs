using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class MusicManager : MonoBehaviour {
    public static MusicManager instance;

    [SerializeField] private AudioSource[] musicLayers;
    [SerializeField] private float fadeDuration = 2f;

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        //Set all volumes to 0 initially
        foreach (var source in musicLayers) {
            source.volume = 0f;
            source.loop = true;
        }

        //Start all tracks at the same time
        foreach (var source in musicLayers) {
            source.Play();
        }

        //Fade in the base layer
        FadeInLayer(0);
    }

    public void FadeInLayer(int layerIndex) {
        if (layerIndex >= 0 && layerIndex < musicLayers.Length) {
            musicLayers[layerIndex].DOFade(1f, fadeDuration);
        } else {
            Debug.LogWarning("Music layer index out of bounds.");
        }
    }

    public void FadeOutLayer(int layerIndex) {
        if (layerIndex >= 0 && layerIndex < musicLayers.Length) {
            musicLayers[layerIndex].DOFade(0f, fadeDuration);
        }
    }

    public void SetLayerVolume(int layerIndex, float targetVolume) {
        if (layerIndex >= 0 && layerIndex < musicLayers.Length) {
            musicLayers[layerIndex].DOFade(targetVolume, fadeDuration);
        }
    }

    /// <summary>
    /// T‰‰ on iha hirvee ratkasu mutta on v‰h‰ kiire
    /// 
    /// </summary>
    public void PlayOnlyBaseLayer() {
        FadeOutLayer(1);
        FadeOutLayer(2);
        FadeOutLayer(3);
        FadeOutLayer(4);
        FadeOutLayer(5);
    }
}
