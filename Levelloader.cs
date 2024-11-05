using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelloader : MonoBehaviour
{
    public static Levelloader Instance;
    public Slider loadingslide;

    public Animator Trasition;
    private float transitiontime = 1f;
    

    public void ExitToMainmenu()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        StartCoroutine(LevelLoader(151));
    }
    public void Nextlevel()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        StartCoroutine(LevelLoader(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void Restartlevel()
    {
        AudioManegers.instance.playSFX(AudioManegers.instance.Buttonclick);
        StartCoroutine(LevelLoader(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator LevelLoader(int Levelindex)
    {
        Trasition.SetTrigger("Start");
        yield return new WaitForSeconds(transitiontime);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(Levelindex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress /.9f);
            loadingslide.value = progress;
            Debug.Log(progress);
            yield return null;

        }


    }
}   
