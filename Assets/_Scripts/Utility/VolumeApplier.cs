using UnityEngine;

public class VolumeApplier : MonoBehaviour {
    [SerializeField] private bool isSFX = true, liveUpdate = false;
    private AudioSource audioSource;

    void Start() {
        this.audioSource = GetComponent<AudioSource>();

        SetVolume();
    }

    private void Update() {
        if (liveUpdate) {
            SetVolume();
        }
    }

    public void SetVolume() {
        if (isSFX) {
            this.audioSource.volume = SoundManager.Instance.SFXVolume;
        } else {
            this.audioSource.volume = SoundManager.Instance.MusicVolume;
        }
    }
}
