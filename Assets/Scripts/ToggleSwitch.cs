using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    public GameObject Switch;
    public Image BackgroundImage;
    public float toggleSpeed = 1.0f;
    float ImageSize;
    bool toggle = false;
    Vector3 destinationPosition;
    public bool State = false;

    Vector3 startingPosition;

    [System.Serializable]
    public class InteractionEvent : UnityEvent { }

    public InteractionEvent OnEvent = new InteractionEvent();
    public InteractionEvent OffEvent = new InteractionEvent();


    public void StateON()
    {
        Debug.Log("StateON called");
        OnEvent.Invoke();
    }

    public void StateOFF()
    {
        Debug.Log("StateOFF called");
        OffEvent.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
    
       // DontDestroyOnLoad(this.gameObject);


        ImageSize = BackgroundImage.rectTransform.rect.width;

        if( (ImageSize/2)%2 != 0) { 
            ImageSize = ImageSize + 2;
        }

        Vector3 switchPos = Switch.transform.localPosition;
        Switch.transform.localPosition = new Vector3(-(ImageSize/ 4), switchPos.y, switchPos.z);
        startingPosition = Switch.transform.localPosition;
        // Debug.Log(ImageSize);

        destinationPosition = new Vector3(switchPos.x + (ImageSize / 2), switchPos.y, switchPos.z);
    }

    // Update is called once per frame
    void Update()
    {

        /*   if (toggle)
           {
               float delTime = Time.deltaTime;
               Switch.transform.localPosition = Vector3.Lerp(Switch.transform.localPosition, destinationPosition, delTime * toggleSpeed);

               if(Vector3.Distance(Switch.transform.localPosition, destinationPosition) == 0)
               {
                   toggle = false;
               }
           }
           */
        float delTime = Time.deltaTime;
        if (!State)
        {
            Switch.transform.localPosition = Vector3.Lerp(Switch.transform.localPosition, startingPosition, delTime * toggleSpeed);

        }
        else
        {
            Switch.transform.localPosition = Vector3.Lerp(Switch.transform.localPosition, destinationPosition, delTime * toggleSpeed);

        }

    }

    public void ToggleState()
    {
        State = !State;
       
       /* if (State)
        {
            Vector3 switchPos = Switch.transform.localPosition;
            destinationPosition = new Vector3(switchPos.x + (ImageSize/2), switchPos.y, switchPos.z);
            toggle = true;
            StateON();
        }
        else
        {
            Vector3 switchPos = Switch.transform.localPosition;
            destinationPosition = new Vector3(switchPos.x - (ImageSize / 2), switchPos.y, switchPos.z);
            toggle = true;
            StateOFF();
        }
      */
        Debug.Log("Toggle State " + State);

    }
}
