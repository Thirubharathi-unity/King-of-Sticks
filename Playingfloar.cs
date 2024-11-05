
using System.Collections.Generic;
using UnityEngine;

public class Playingfloar : MonoBehaviour
{
    
    private Dictionary<GameObject, Quaternion> InitialQuaternion = new Dictionary<GameObject, Quaternion>();
    private Dictionary<GameObject, Vector3> Lastposition = new Dictionary<GameObject, Vector3>();       
    [SerializeField]
    private float Rotationthreshold = 2f;
    [SerializeField]
    private float movementThreshold = 1.5f;
    [SerializeField]
    public GameObject playingUI;
    [SerializeField]
     public GameObject TriggerMenu;

   

    private void Start()
    {
        Sticks.matchstart = false;
        playingUI = GameObject.FindGameObjectWithTag("PlayingUI");
        TriggerMenu = GameObject.FindGameObjectWithTag("Collition");
        TriggerMenu.SetActive(false);
        Debug.Log("Trigger Menu is OFF");
        
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
        if (IsStick(collision))
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
        if (IsStick(collision))
        {
            if (StickPositionMoved(collision))
            {
                Debug.Log(this.name + collision.gameObject.name);
                TriggerCollisionResponse();
            }
            if (StickRotationMoved(collision))
            {
                Debug.Log(this.name +collision.gameObject.name);
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
        bool CorrectingTag = Sticks.matchstart && (collision.gameObject.tag == "SubStick" || collision.gameObject.tag == "MainStick");
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
        Sticks.matchstart = false;
    }
}
