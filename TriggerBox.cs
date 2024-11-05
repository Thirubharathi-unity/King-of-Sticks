
using UnityEngine;


public class TriggerBox : MonoBehaviour
{
   
    public GameObject vfx;
   
    private void Start()
    {

    }
    private void Update()
    {

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SubStick")
        {
            AudioManegers.instance.playSFX(AudioManegers.instance.SubStick);
            GameObject sprite = Instantiate(vfx,other.gameObject.transform.position, other.transform.rotation);
            Destroy(other.gameObject);        
            Destroy(sprite, 0.50f);
            PlayingCanvas.Score += 10;
            Playing_canvas_Medium.Score += 10;
            Playing_canvas_Hard.Score += 10;
          
        }
        if (other.gameObject.tag == "MainStick")
        {
           
            AudioManegers.instance.playSFX(AudioManegers.instance.MainStick);
            GameObject sprite = Instantiate(vfx, other.gameObject.transform.position, other.transform.rotation);
            Destroy (other.gameObject);
            Destroy(sprite, 0.50f);
            PlayingCanvas.Score += 50;
            Playing_canvas_Medium.Score += 50;
            Playing_canvas_Hard.Score += 50;
        }
    }
  
}
