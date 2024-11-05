
using UnityEngine;


public class LevelButton : MonoBehaviour
{
    public GameObject[] Star;
    [SerializeField]
    private string levelname;

    private void Start()
    {

        if (PlayerPrefs.HasKey(levelname))
        {
            int Starvalue = PlayerPrefs.GetInt(levelname);
            if (Starvalue == 1)
            {
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(false);
                Star[2].gameObject.SetActive(false);
            }
            else if (Starvalue == 2)
            {  
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(true);
                Star[2].gameObject.SetActive(false);
            }
            else if (Starvalue == 3)
            {   
                Star[0].gameObject.SetActive(true);
                Star[1].gameObject.SetActive(true);
                Star[2].gameObject.SetActive(true);
            }
            else
            {  
                Star[0].gameObject.SetActive(false);
                Star[1].gameObject.SetActive(false);
                Star[2].gameObject.SetActive(false);
            }
        }
    }
  
}
