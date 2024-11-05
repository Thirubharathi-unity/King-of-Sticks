using System.Collections.Generic;

using UnityEngine;


public class Sticks : MonoBehaviour
{
    
    public static bool matchstart = false;
    public bool dragingStick = false;
    private Dictionary<GameObject, Quaternion> InitialQuaternion = new Dictionary<GameObject, Quaternion>();
    private Dictionary<GameObject, Vector3> Lastposition = new Dictionary<GameObject, Vector3>();
    private Vector3 offset;
    
    public static Sticks currentlyDragging;
    private Rigidbody rb;
    
    [SerializeField]
    private float Rotationthreshold;
    [SerializeField]
    private float movementThreshold;
    [SerializeField]
    public GameObject playingUI, TriggerMenu;
    private void Start()
    {
        if(playingUI == null || TriggerMenu == null)
        {
            Debug.Log("playing UI not assinged");
            playingUI = GameObject.FindGameObjectWithTag("PlayingUI");
            TriggerMenu = GameObject.FindGameObjectWithTag("Collition");
        }
        
        rb = GetComponent<Rigidbody>();
       
    }
    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                    {
                        OntouchDown(touch);
                    }    
                    break;
                case TouchPhase.Moved:
                    if(currentlyDragging == this)
                    {
                        
                    }
                  
                    break;
                case TouchPhase.Ended:
                    if (currentlyDragging == this)
                    {
                        OntouchUp(touch);
                    }
                   
                    break;
            }
        }
    }
    private void FixedUpdate()
    {
        if(dragingStick && rb != null)
        {
            rb.isKinematic = true;
        }
        else if(!dragingStick && rb != null)
        {
            rb.isKinematic = false;
        }
        if(dragingStick)
        {
            Vector3 Targetpos = GetMouseWorldPos(Input.GetTouch(0))+offset;
            transform.position= Vector3.Lerp(transform.position, Targetpos, Time.fixedDeltaTime*20);
        }
        
    } 

    void OntouchDown(Touch touch)
    {
        if (!matchstart) return;

        offset = transform.position - GetMouseWorldPos(touch);
        dragingStick = true;
        currentlyDragging = this;

    }

    private void OntouchUp(Touch touch)
    {
        dragingStick = false;
        currentlyDragging = null;
        
    }

   

    private Vector3 GetMouseWorldPos(Touch touch)
    {
        Vector3 touchPoint = touch.position;
        touchPoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(touchPoint);
    }



    void OnCollisionEnter(Collision collision)
    {
        if (StickTag(collision))
        {
            AudioManegers.instance.playSFX(AudioManegers.instance.CollisionEnter);
            Lastposition[collision.gameObject] = collision.transform.position;
        }
        if (StickTag(collision))
        {
            AudioManegers.instance.playSFX(AudioManegers.instance.CollisionEnter);
            InitialQuaternion[collision.gameObject] = collision.transform.rotation;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(IsStick(collision))
        {
            if (StickPositionMoved(collision))
            {
                Debug.Log(this.name + collision.gameObject.name);
                TriggerCollisionResponse();
            }
            if (StickRotationMoved(collision))
            {
                Debug.Log(this.name + collision.gameObject.name);
                TriggerCollisionResponse();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if( IsStick(collision))
        {
            if (StickPositionMoved(collision))
            {
                Debug.Log(this.name + collision.gameObject.name);

                TriggerCollisionResponse();
            }
            if(StickRotationMoved(collision))
            {
                Debug.Log(this.name+collision.gameObject.name);
                TriggerCollisionResponse();
            }
        }
    }

    private bool StickTag(Collision collision)
    {
        if (collision.gameObject.tag == "MainStick" || collision.gameObject.tag == "SubStick")
        {
            return true;
        }
        return false;
    }

    private bool IsStick(Collision collision)
    {
        bool CorrectingTag = matchstart && (collision.gameObject.tag == "SubStick" || collision.gameObject.tag == "MainStick");
        Sticks stickScript = collision.gameObject.GetComponent<Sticks>();        
        bool isBeingDragged = stickScript != null && stickScript.dragingStick;
        return CorrectingTag && !isBeingDragged;
    }

    private bool StickPositionMoved(Collision collision)
    {
        Vector3 Storedvalue;
        if (Lastposition.TryGetValue(collision.gameObject, out Storedvalue))
        {
                return Vector3.Distance(Storedvalue, collision.gameObject.transform.position) > movementThreshold;
        }
        return false;
        }

    private bool StickRotationMoved(Collision collision)
    {
        Quaternion quaternion;
        if (InitialQuaternion.TryGetValue(collision.gameObject, out quaternion))
        {
            return Quaternion.Angle(quaternion, collision.gameObject.transform.rotation) > Rotationthreshold;
        }
        return false;
        }

    private void TriggerCollisionResponse()
    {
        
        AudioManegers.instance.playSFX(AudioManegers.instance.CollisionTrigger);
        playingUI.gameObject.SetActive(false);
        TriggerMenu.gameObject.SetActive(true);
        matchstart = false;
    }
}