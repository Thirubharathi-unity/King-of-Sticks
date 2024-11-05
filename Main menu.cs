
using System.Collections;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{

    public GameObject levelSelection;
    
    public GameObject MainmenuOB;
    public GameObject Text;
    
    private void Start()
    {
        

        AudioManegers.instance.PlayMusic(AudioManegers.instance.MainmenuTheme);
    }
    public void playbutton()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        MainmenuOB.gameObject.SetActive(false);
        levelSelection.SetActive(true);
    }
    public void GameQuitbutton()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        Application.Quit();
    }
    
    public void ButtonclickSound()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
    }
    public void multiplayerbutn()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        StartCoroutine(ComingsoonTxt());

    }
    IEnumerator ComingsoonTxt()
    {
        Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        Text.gameObject.SetActive(false);
    }


}
