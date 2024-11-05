
using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausemenuManeger : MonoBehaviour
{
    
   
    public GameObject[] Star;
    
    public Text ScoreTextFailed;
    public Text ScoreTextFinished;
    public GameObject levelFinished;
    public GameObject leveFailed;
    [SerializeField]
    public float TotalScore;
    private float Scorepercentage;
    private bool StarArchivedActive= false;
//----------------------------------------------Mainmenu variable----------------  
    private string currentLevelname;
    private int PreviousStarcount;
   
    private void Start()
    {
        currentLevelname = SceneManager.GetActiveScene().name;
        PreviousStarcount = PlayerPrefs.GetInt(currentLevelname,0);
        
    }

    private void FixedUpdate()
    {
        ScoreTextFinished.text = MathF.Round(PlayingCanvas.Score).ToString();
        ScoreTextFailed.text = ScoreTextFinished.text;
        float Gainedscore = Mathf.Round(PlayingCanvas.Score);
        Scorepercentage = (Gainedscore / TotalScore) * 100f;

        if (Scorepercentage >= 99f && !StarArchivedActive)
        {
            StarArcheved();
        }     
    }
    
    public void Retry()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        PlayingCanvas.health -= 1;
        Sticks.matchstart = true;
    }

    public void Unlocknextlevel()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;
        if (currentlevel >= PlayerPrefs.GetInt("Unlockedlevels"))
        {
            PlayerPrefs.SetInt("Unlockedlevels", currentlevel+1);
        }           
    }

    public void StarArcheved()
    {      
        StarArchivedActive = true;
      
        if(Scorepercentage >= 50f && Scorepercentage < 75f)
        {
            Unlocknextlevel();
            AudioManegers.instance.playSFX(AudioManegers.instance.GameFinished);
            levelFinished.gameObject.SetActive(true);
            Star[0].SetActive(true);
            if(PreviousStarcount<=1)
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
            if(PreviousStarcount <=2)
            {
                 PlayerPrefs.SetInt(currentLevelname, 2);
            }
        }
        else if (Scorepercentage >= 99f)
        {
            Unlocknextlevel();
            AudioManegers.instance.playSFX(AudioManegers.instance.GameFinished);
            levelFinished.gameObject.SetActive(true);
            if (PlayingCanvas.health >= 3)
            {
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(true);
                Star[2].gameObject.SetActive(true);
                if (PreviousStarcount <= 3)
                {
                    PlayerPrefs.SetInt(currentLevelname, 3);
                }
            }
            else if (PlayingCanvas.health < 3)
            {
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(true);

                if (PreviousStarcount <= 2)
                {
                    PlayerPrefs.SetInt(currentLevelname, 2);
                }
            }
        }
        else if(Scorepercentage <= 49f)
        {
            if (PreviousStarcount <= 0)
            {              
                PlayerPrefs.SetInt(currentLevelname, 0);
            }
            AudioManegers.instance.playSFX(AudioManegers.instance.GameOver);
            leveFailed.gameObject.SetActive(true);
        }
          
    }
 
}
    