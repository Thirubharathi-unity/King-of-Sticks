using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausemenu_Hard : MonoBehaviour
{
    public GameObject[] Star;

    public Text ScoreTextFailed;
    public Text ScoreTextFinished;
    public GameObject levelFinished;
    public GameObject leveFailed;
    [SerializeField]
    public float TotalScore;
    private float Scorepercentage;
    private bool StarArchivedActive = false;
    //----------------------------------------------Mainmenu variable----------------  
    private string currentLevelname;
    private int PreviousStarcount;

    private void Start()
    {
        currentLevelname = SceneManager.GetActiveScene().name;
        PreviousStarcount = PlayerPrefs.GetInt(currentLevelname, 0);

    }

    private void FixedUpdate()
    {
        ScoreTextFinished.text = MathF.Round(Playing_canvas_Hard.Score).ToString();
        ScoreTextFailed.text = ScoreTextFinished.text;
        float Gainedscore = Mathf.Round(Playing_canvas_Hard.Score);
        Scorepercentage = (Gainedscore / TotalScore) * 100f;

        if (Scorepercentage >= 99f && !StarArchivedActive)
        {
            StarArcheved();
        }
    }

    public void Retry()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        Playing_canvas_Hard.health -= 1;
        Sticks.matchstart = true;
    }

    public void Unlocknextlevel()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;
        if (currentlevel >= PlayerPrefs.GetInt("HardUnlockedlevels"))
        {
            PlayerPrefs.SetInt("HardUnlockedlevels", currentlevel + 1);
        }
    }

    public void StarArcheved()
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
            Star[0].gameObject.SetActive(true);
            Star[1].gameObject.SetActive(true);
            Star[2].gameObject.SetActive(true);
            if (PreviousStarcount <= 3)
            {
                PlayerPrefs.SetInt(currentLevelname, 3);
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
}
