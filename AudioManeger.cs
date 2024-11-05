
using UnityEngine;


public class AudioManegers :MonoBehaviour 
{
    public static AudioManegers instance;

    [Header("--------------Audio Clip------")]
    public AudioClip CollisionTrigger;
    public AudioClip MainStick;
    public AudioClip SubStick;
    public AudioClip CollisionEnter;
    public AudioClip GameOver;
    public AudioClip GameFinished;
    public AudioClip BackGroundMusic;
    public AudioClip Buttonclick;
    public AudioClip MainmenuTheme;
    public AudioClip Unlockedlevels;

    [Header("----------Source----------")]
    public AudioSource musicsource;
    public AudioSource Sfxsource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   

    public void PlayMusic(AudioClip clip)
    {
        musicsource.clip = clip;
        musicsource.Play();
    }
    public void StopMusic()
    {
        musicsource.Stop();
    }
    public void playSFX(AudioClip clip)
    {
        Sfxsource.PlayOneShot(clip);
        
    }
      
    public void MuteSfx()
    {
       playSFX(Buttonclick);
        Sfxsource.mute = !Sfxsource.mute; 
    }
    public void MuteMusic()
    {
        playSFX(Buttonclick);
        musicsource.mute =!musicsource.mute;
    }
}
