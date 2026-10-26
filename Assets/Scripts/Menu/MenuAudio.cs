using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        if (mixer == null)
        {
            Debug.LogWarning("Assign the Audio Mixer on MenuAudio.", this);
            return;
        }
        Setup(masterSlider, "MasterVolume");
        Setup(musicSlider, "MusicVolume");
        Setup(sfxSlider, "SFXVolume");
    }

    void Setup(Slider slider, string key)
    {
        float value = Mathf.Clamp01(PlayerPrefs.GetFloat(key, 1));
        Apply(key, value);
        if (slider == null) return;
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.wholeNumbers = false;
        slider.SetValueWithoutNotify(value);
        slider.onValueChanged.AddListener(v => Apply(key, v));
    }

    void Apply(string key, float value)
    {
        float decibels = value <= 0 ? -80f : Mathf.Log10(value) * 20f;
        if (!mixer.SetFloat(key, decibels))
            Debug.LogWarning("Expose the Audio Mixer parameter named " + key);
        PlayerPrefs.SetFloat(key, value);
    }

    public void SaveSettings() { PlayerPrefs.Save(); }
    void OnApplicationPause(bool paused) { if (paused) SaveSettings(); }
    void OnApplicationQuit() { SaveSettings(); }
}
