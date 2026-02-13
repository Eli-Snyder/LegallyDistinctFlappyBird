using UnityEngine; // Wow
using UnityEngine.UI; // Such Unity
using UnityEngine.Audio; // Such shit

public class VolumeController : MonoBehaviour
{
    [Header("Moar shit")] // Me when adding a code comment introduces a new and never-before-seen compilation error (I got a little too silly)
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    public const string MIXER_MUSIC = "MasterParam";

    // You know what? Fuck you.
    // *Puts all your methods on a single line*
    void Start() { slider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f); }
    private void Awake() { slider.onValueChanged.AddListener(SetMusicVolume); }
    private void OnDisable() { PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, slider.value); }
    void SetMusicVolume(float volume) { mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume)*20); }
}
