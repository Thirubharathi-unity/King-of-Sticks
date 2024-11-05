
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Playing_canvas_Medium : MonoBehaviour
{
    public GameObject Heart0, Heart1, Heart2, AdRewardBanner;
    public static int health;
    public Text scoreTextCanvas;
    public static int Score;
    public Text Levelname;
    //------------------------------Star system----------------    

    public GameObject levelFinished;
    public GameObject leveFailed;
    private string currentLevelname;
    private int PreviousStarcount;
    public GameObject[] Star;
    private bool StarArchivedActive = false;
    [SerializeField]
    public float totalScore;
    private float Scorepercentage;


    [Header("------Audio-------")]

    public Slider MusicSlider;
    public Slider SFXslider;
    [SerializeField] AudioMixer audioMixer;
    public static bool Adshowed;



    void Start()
    {


        //---------------------------------------Ad------------------------------------------
        Adshowed = false;
        // ---------------------------------------------star system --------------------------------

        currentLevelname = SceneManager.GetActiveScene().name;
        PreviousStarcount = PlayerPrefs.GetInt(currentLevelname, 0);


        // ---------------------------------------------canvas UI--------------------------       
        Score = 0;
        Levelname.text = SceneManager.GetActiveScene().name;

        //   ----------------------------------------- Audio System ---------------------------
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();

        }
        else
        {
            MusicVolumeButton();
            SFXVolumeButton();
        }
        AudioManegers.instance.PlayMusic(AudioManegers.instance.BackGroundMusic);



        //   ----------------------------- Health system ---------------------------
        health = 2;
        Heart0.gameObject.SetActive(true);
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);
        
        AdRewardBanner.gameObject.SetActive(false);
    }

    void Update()
    {
        scoreTextCanvas.text = "Points : " + Mathf.Round(Score).ToString();
        float Gainedscore = Mathf.Round(Score);
        Scorepercentage = (Gainedscore / totalScore) * 100f;


        //   ----------------------------- Health system ---------------------------       
        switch (health)
        {
            case 2:
                Heart0.gameObject.SetActive(true);
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(true);
                AdRewardBanner.gameObject.SetActive(false);
                break;
            case 1:
                Heart0.gameObject.SetActive(true);
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(false);
                
                break;
            default:
                if (!Adshowed)
                {
                    Heart0.gameObject.SetActive(false);
                    Heart1.gameObject.SetActive(false);
                    Heart2.gameObject.SetActive(false);
                    
                    AdRewardBanner.gameObject.SetActive(true);
                    Sticks.matchstart = false;

                }
                else if (Adshowed)
                {
                    if (!StarArchivedActive)
                    {
                        StarArcheved();
                    }
                }
                break;

        }
    }
    //   ------------------------------------- Scene Manegement funtion ------------------------------------------
    public void PauseButton()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        Sticks.matchstart = false;
    }

    public void ResumeButton()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        Sticks.matchstart = true;
    }

    public void matchstart()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        Sticks.matchstart = true;
        Debug.Log("Match started");
    }

    public void Empybuttonsound()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
    }

    //----------------------------------------Star system------------------------------------------
    private void Unlocknextlevel()
    {

        int currentlevel = SceneManager.GetActiveScene().buildIndex;
        if (currentlevel >= PlayerPrefs.GetInt("MediumUnlockedlevels"))
        {
            PlayerPrefs.SetInt("MediumUnlockedlevels", currentlevel + 1);
        }
    }
    private void StarArcheved()
    {
        StarArchivedActive = true;
        if (Scorepercentage >= 40f && Scorepercentage < 75f)
        {
            Unlocknextlevel();
            AudioManegers.instance.playSFX(AudioManegers.instance.GameFinished);
            levelFinished.gameObject.SetActive(true);
            Star[0].SetActive(true);
            if (PreviousStarcount <= 1)
            {
                PlayerPrefs.SetInt(currentLevelname, 1);
            }
        }
        else if (Scorepercentage >= 75f && Scorepercentage < 98f)
        {
            Unlocknextlevel();
            AudioManegers.instance.playSFX(AudioManegers.instance.GameFinished);
            levelFinished.gameObject.SetActive(true);
            Star[0].SetActive(true);
            Star[1].SetActive(true);
            if (PreviousStarcount <= 2)
            {
                PlayerPrefs.SetInt(currentLevelname, 2);
            }
        }
        else if (Scorepercentage >= 99f)
        {
            Unlocknextlevel();
            AudioManegers.instance.playSFX(AudioManegers.instance.GameFinished);
            levelFinished.gameObject.SetActive(true);
            if (health >= 2)
            {
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(true);
                Star[2].gameObject.SetActive(true);
                if (PreviousStarcount <= 3)
                {
                    PlayerPrefs.SetInt(currentLevelname, 3);
                }
            }
            else if (health < 2)
            {
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(true);

                if (PreviousStarcount <= 2)
                {
                    PlayerPrefs.SetInt(currentLevelname, 2);
                }
            }

        }
        else if (Scorepercentage <= 49f)
        {
            if (PreviousStarcount <= 0)
            {
                PlayerPrefs.SetInt(currentLevelname, 0);
            }
            AudioManegers.instance.playSFX(AudioManegers.instance.GameOver);
            leveFailed.gameObject.SetActive(true);
        }

    }

    //-------------------------------------- Audio Maneger Funtion ----------------------------------------------

    public void MusicVolumeButton()
    {
        float volume = MusicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);

    }
    public void SFXVolumeButton()
    {
        float volume = SFXslider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        MusicVolumeButton();
        SFXslider.value = PlayerPrefs.GetFloat("SFXVolume");
        SFXVolumeButton();
    }
    public void MusicMuteButton()
    {
        AudioManegers.instance.MuteMusic();
    }
    public void SFXmuteButton()
    {
        AudioManegers.instance.MuteSfx();
    }

}
