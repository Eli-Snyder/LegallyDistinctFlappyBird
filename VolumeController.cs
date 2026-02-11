using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    public const string MIXER_MUSIC = "MasterParam";

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
    }

    private void Awake()
    {
        slider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, slider.value);
    }

    void SetMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume)*20);
    }
}
