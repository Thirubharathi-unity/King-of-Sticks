
using UnityEngine;
using UnityEngine.UI;

public class Cameramaneger : MonoBehaviour
{
    [SerializeField]
    public GameObject Camera1;
    [SerializeField]
    public GameObject Camera2;
    [SerializeField]
    public GameObject Camera3;
    [SerializeField]
    public GameObject Camera4;
    public Toggle[] toggles;
    // Start is called before the first frame update
    void Start()
    {
        
        Camera1 = GameObject.FindGameObjectWithTag("CAM1");
        Camera2 = GameObject.FindGameObjectWithTag("CAM2");
        Camera3 = GameObject.FindGameObjectWithTag("CAM3");
        Camera4 = GameObject.FindGameObjectWithTag("CAM4");
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener(delegate { OnTogglechange(toggles[index], index); }); 
        }
        Debug.Log("Toggle 1 is on");
        toggles[0].isOn = true;
        Camera1.gameObject.SetActive(true);
        Camera2.gameObject.SetActive(false);
        Camera3.gameObject.SetActive(false);
        Camera4.gameObject.SetActive(false);

    }
    public void OnTogglechange(Toggle toggle, int Index)
    {
        if(toggle.isOn && Index < toggles.Length)
        {
            if(Index == 0)
            {
                Camera1.gameObject.SetActive(true);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(false);
                Camera4.gameObject.SetActive(false);
            }
            else if(Index == 1)
            {
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(true);
                Camera3.gameObject.SetActive(false);
                Camera4.gameObject.SetActive(false);
            }
            else if(Index == 2)
            {
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(true);
                Camera4.gameObject.SetActive(false);
            }
            else if (Index == 3)
            {
                Camera1.gameObject.SetActive(false);
                Camera2.gameObject.SetActive(false);
                Camera3.gameObject.SetActive(false);
                Camera4.gameObject.SetActive(true);
            }
            else { Debug.Log("INDEX is more then camera"); }
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            if(Index != i)
            {
                toggles[i].SetIsOnWithoutNotify(false);
            }
            else if (toggles[Index].isOn == false)
            {
                toggles[Index].SetIsOnWithoutNotify(true);
            }
        }

    }

    
}
