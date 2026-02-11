using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;
    [SerializeField] private AudioClip memeSound;
    [SerializeField] private AudioSource memeryAudio, music;

    public const string MUSIC_KEY = "musicVolume"; // DAMN YOU JUST COMPILE
    private void Awake() // Wakie wakies the uhhhhhh
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {Destroy(gameObject);}

        LoadVolume();
        music.Play();
    }
    void LoadVolume() // Volume Saved in VolumeController.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);

        mixer.SetFloat(VolumeController.MIXER_MUSIC, Mathf.Log10(musicVolume)*20);
    }

    public void MemeMusic()
    {
        music.Stop();
        memeryAudio.Play();
        Debug.Log("I CAST RUSSIAN HARDBASS");
    }
    
}
