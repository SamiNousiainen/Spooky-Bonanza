using UnityEngine;

/// <summary>
/// Class for triggers controlling which music layers are audible
/// </summary>
public class MusicTrigger : MonoBehaviour {

    [System.Serializable]
    public class MusicLayerAction {
        public int layerIndex;
    }

    [Header("Fade in layers")]
    public MusicLayerAction[] layersToFadeIn;

    [Header("Fade out layers")]
    public MusicLayerAction[] layersToFadeOut;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))

        if (MusicManager.instance != null) {
            foreach (var layer in layersToFadeIn) {
                MusicManager.instance.FadeInLayer(layer.layerIndex);
            }

            foreach (var layer in layersToFadeOut) {
                MusicManager.instance.FadeOutLayer(layer.layerIndex);
            }
        }
    }
}
