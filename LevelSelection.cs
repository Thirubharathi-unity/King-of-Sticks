using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Slider LoadingSlide;
    public Animator trasition;
    private float transitiontime = 1.8f;
    public Button[] LevelButton;
    private int Levelunlocked;
    private int Mediumlevelunlocked;
    private int Hardlevelunlocked;
    public TextMeshProUGUI UnlockLevelsTxt;
    public GameObject unlocktxtGB;
    private void Start()
    {

        
        Hardlevelunlocked = PlayerPrefs.GetInt("HardUnlockedlevels", 0);
        Mediumlevelunlocked = PlayerPrefs.GetInt("MediumUnlockedlevels",0);
        Levelunlocked = PlayerPrefs.GetInt("Unlockedlevels",1);
        if(Levelunlocked >10 &&Mediumlevelunlocked <51)
        {
            StartCoroutine(UnlockedLevelText("New Challenge Unlocked: Medium Levels are now available! Dive in and test your skills"));
            Debug.Log("Mediumlevel unlocked");
            PlayerPrefs.SetInt("MediumUnlockedlevels", 51);
            Mediumlevelunlocked = PlayerPrefs.GetInt("MediumUnlockedlevels");
        }
        if(Mediumlevelunlocked > 65 && Hardlevelunlocked <101)
        {
            StartCoroutine(UnlockedLevelText("New Challenge Unlocked: Hard Levels are now available! Dive in and test your skills"));
            Debug.Log("Hard level unlocked");
            PlayerPrefs.SetInt("HardUnlockedlevels", 101);
            Hardlevelunlocked = PlayerPrefs.GetInt("HardUnlockedlevels");
        }
        for (int i = 0; i < LevelButton.Length; i++)
        {
            
            if(i < Levelunlocked)
            {
                LevelButton[i].interactable = true;
            }
            else if(i >=50 && i <Mediumlevelunlocked)
            {
                LevelButton[i].interactable=true;
            }
            else if(i>=100 && i < Hardlevelunlocked)
            {
                LevelButton[i].interactable=true;
            }
            else
            {
                LevelButton[i].interactable=false;
            }
        }
       
    }
    public void Levelload(int levelindex)
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        StartCoroutine(LoadLevel(levelindex));
    }
    IEnumerator LoadLevel(int levelindex)
    {
        trasition.SetTrigger("End");
        yield return new WaitForSeconds(transitiontime);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelindex);
        while (!operation.isDone) 
        {
            float progress = Mathf.Clamp01(operation.progress /.9f);
            LoadingSlide.value = progress;
            Debug.Log("Level load " + progress); 
            yield return null;
        }
    }
    IEnumerator UnlockedLevelText(string UnlockTxt)
    {
        UnlockLevelsTxt.text = UnlockTxt;
        AudioManegers.instance.playSFX(AudioManegers.instance.Unlockedlevels);
        unlocktxtGB.SetActive(true);
        yield return new WaitForSeconds(6);
        unlocktxtGB.SetActive(false);
    }
}
