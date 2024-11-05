
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_script : MonoBehaviour
{
    public GameObject play;
    public GameObject Score;
    public GameObject Camera;
    public GameObject Life;
    public GameObject Pausemenu;
    public GameObject Resume;
    public GameObject Triggermenu;
    public GameObject Triggermenupanel;
    public GameObject Retrypannel;
    public GameObject Destroypannel;
    [Header("----------Buttons----------")]
    public Button playbutton;
    public Button Pausebutton;
    public Button Resumebutton;
    public Button Retrybutton;
    [Header("--------------Stick---------")]
    public GameObject[] SticksM;
    [SerializeField]
    public Vector3[] Sticklocation;
    private float Thereshold = 1f;
    private bool trggercalled = false;
    
    private void Awake()
    {
       
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            int temp = PlayerPrefs.GetInt("TutorialComplete");
            if(temp == 1)
            {
                Debug.Log(temp+this.gameObject.name+"has been destroyed");
                Destroy(this.gameObject);

            }
        }
        else
        {
            Debug.Log("no Key has found");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        SticksM = GameObject.FindGameObjectsWithTag("SubStick");
        if (SticksM != null && SticksM.Length > 0)
        {
            Sticklocation = new Vector3[SticksM.Length];
            for(int i = 0; i < SticksM.Length; i++)
            {
                if (SticksM[i] != null)
                {
                    Sticklocation[i] = SticksM[i].transform.position;
                }
                else { Debug.Log("Stick " + i + "is null"); }
            }
        }
        else { Debug.Log("Stick lenght is !>0"); }

    }
    private void Update()
    {
        if(trggercalled == false)
        {
            for (int i = 0; i < SticksM.Length; i++)
            {
                if (SticksM[i] != null && Vector3.Distance(Sticklocation[i], SticksM[i].transform.position) > Thereshold)
                {
                    Debug.Log(SticksM[i].name);
                    Calltriggermenu();
                }
            }
        }
        else { Debug.Log("trigger was called"); }
            
      
       
    }
    public void Playbutton()
    {
        playbutton.onClick.Invoke();
        Camera.gameObject.SetActive(true);
        play.SetActive(false);
    }
    public void Camerabutton()
    {
        Score.gameObject.SetActive(true);
        Camera.gameObject.SetActive(false);
         

    }
    public void Scorebuton()
    {
        Life.gameObject.SetActive(true);
        Score.gameObject.SetActive(false);
        Debug.Log("scorebutnclicked");
    }
    public void Lifebutton()
    {
        Life.gameObject.SetActive(false);
        Pausemenu.gameObject.SetActive(true);
    }

    public void pauseMenubuttn()
    {
        Pausemenu.gameObject.SetActive(false);
        Pausebutton.onClick.Invoke();
        Resume.gameObject.SetActive(true);
    }
    public void resumeMenubuttn()
    {
        Resume.gameObject.SetActive(false);
        Resumebutton.onClick.Invoke();
        Destroypannel.gameObject.SetActive(true);
    }
    public void Destroypannelbutn()
    {
        Destroypannel.gameObject.SetActive(false) ;
    }
    public void Calltriggermenu()
    {
        Triggermenu.gameObject.SetActive(true);
        Triggermenupanel.gameObject.SetActive(true);
        trggercalled = true;
    }
    public void Triggermenubutn()
    {
      Triggermenupanel.gameObject.SetActive(false );
        Retrypannel.gameObject.SetActive(true);
    }
    public void Retrypannebutn()
    {
        Retrybutton.onClick.Invoke();
        Retrypannel.gameObject.SetActive(false);
        PlayerPrefs.SetInt("TutorialComplete",1);
        Destroy(this.gameObject);
        Debug.Log("Tutorial object Destroyed");
        
    }
}
