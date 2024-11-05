
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound_settings : MonoBehaviour
{
   
    public AudioMixer mixer;
    public Slider musicslider;
    public Slider SFXslider;
   
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            Loadvolume();
        }
        else
        {
            Musicvolume();
            SFXVolume();
        }
    }
    public void Musicvolume()
    {
        float volume = musicslider.value;
        mixer.SetFloat("Music",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume",volume);
    }
    public void SFXVolume()
    {
        float volume = SFXslider.value;
        mixer.SetFloat("SFX",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void Loadvolume()
    {
        musicslider.value= PlayerPrefs.GetFloat("MusicVolume");
        Musicvolume();
        SFXslider.value = PlayerPrefs.GetFloat("SFXVolume");
        SFXVolume();
    }
    public void MusicMute()
    {
        AudioManegers.instance.MuteMusic();
    }
    public void MuteSfx()
    {
        AudioManegers.instance.MuteSfx();
    }

  
}
