using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    bool open = true;
    public GameObject slideObject;

    public float openHeight;
   // public bool toggle;
   // public Vector3 destinationPosition;

    public float slideSpeed = 1f;
    public Vector3 StartingPos;

    void Start()
    {
        StartingPos = slideObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float delTime = Time.deltaTime;
        if (open)
        {
           
            slideObject.transform.localPosition = Vector3.Lerp(slideObject.transform.localPosition, StartingPos, delTime * slideSpeed);

        }
        else
        {
            slideObject.transform.localPosition = Vector3.Lerp(slideObject.transform.localPosition, StartingPos - new Vector3(0,openHeight,0), delTime * slideSpeed);
        }
    }

    public void Slide()
    {
        open = !open;

    }

}
