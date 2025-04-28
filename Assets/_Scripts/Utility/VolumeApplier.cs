using UnityEngine;

public class VolumeApplier : MonoBehaviour {
    [SerializeField] private bool isSFX = true;
    private AudioSource audioSource;

    void Start() {
        this.audioSource = GetComponent<AudioSource>();

        if (isSFX) {
            this.audioSource.volume = SoundManager.Instance.SFXVolume;
        } else {
            this.audioSource.volume = SoundManager.Instance.MusicVolume;
        }
    }

}
