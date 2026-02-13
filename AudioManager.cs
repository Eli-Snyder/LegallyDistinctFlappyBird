using UnityEngine; // Is bro the best Inline-6?
using UnityEngine.Audio; // Bro might be the best Inline-6

public class AudioManager : MonoBehaviour
{
    [Header ("Shit")]
    [SerializeField] AudioMixer mixer; // If your code isn't just one massive shitpost
    [SerializeField] private AudioClip memeSound; // Please retire
    [SerializeField] private AudioSource memeryAudio, music;

    public static AudioManager instance;
    public const string MUSIC_KEY = "musicVolume"; // DAMN YOU JUST COMPILE


    private void Awake() // Wakie wakies the uhhhhhh
    {
        if (instance == null)
        {
            instance = this; // Yeah no I'm just gonna steal your kilobytes now.
            DontDestroyOnLoad(gameObject); // Sorry bud, but I need them to shitpost in my source code.
        }

        else { Destroy(gameObject); } // o7

        LoadVolume();
        music.Play();
    }

    void LoadVolume() // Volume Saved in VolumeController.cs for some reason
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        mixer.SetFloat(VolumeController.MIXER_MUSIC, Mathf.Log10(musicVolume)*20);
    }

    public void MemeMusic() // If I'm not shitposting, I'm either a hostage or dead.
    {
        music.Stop();
        memeryAudio.Play();
        Debug.Log("I CAST RUSSIAN HARDBASS"); // On God bro
    }
    
}