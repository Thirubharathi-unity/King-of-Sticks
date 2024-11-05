
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    [SerializeField]
    public Transform FocusObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(FocusObject);
    }
}
