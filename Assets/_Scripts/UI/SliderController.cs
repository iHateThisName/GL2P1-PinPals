using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private EnumSliderType sliderType = EnumSliderType.MasterVolume;
    void Start() {
        if (slider == null) {
            slider = GetComponent<Slider>();
        }

        switch (sliderType) {
            case EnumSliderType.MasterVolume:
                if (SoundManager.Instance.masterMute) {
                    slider.SetValueWithoutNotify(PlayerSettings.RawMasterVolume);
                } else {
                    slider.SetValueWithoutNotify(SoundManager.Instance.MasterVolume);
                }
                break;

            case EnumSliderType.MusicVolume:
                if (SoundManager.Instance.musicMute) {
                    slider.SetValueWithoutNotify(PlayerSettings.RawMusicVolume);
                } else {
                    slider.SetValueWithoutNotify(SoundManager.Instance.MusicVolume);
                }
                break;

            case EnumSliderType.SFXVolume:
                if (SoundManager.Instance.sfxMute) {
                    slider.SetValueWithoutNotify(PlayerSettings.RawSFXVolume);
                } else {
                    slider.SetValueWithoutNotify(SoundManager.Instance.SFXVolume);
                }
                break;
        }

    }

    public enum EnumSliderType {
        MasterVolume,
        MusicVolume,
        SFXVolume
    }
}
