﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchTest : MonoBehaviour
{


   

    public Text text;
    public float rotationRate = 0.01f;
    public GameObject Cube;
    public GameObject Cone;
    public GameObject Cylinder;
    public GameObject Sphere;
   // public StateTracker stateTrack;

    GameObject SelectedShape;
    bool inARMode = false;

    [System.Serializable]
    public class InteractionEvent : UnityEvent{ }

    public InteractionEvent UpdateSelectedShape = new InteractionEvent();
    // Start is called before the first frame update

    public void UpdateShape(string name)
    {
        Debug.Log("updateShape called");
       // text.text = "UPDATE SHAPE CALLED FROM TOUCH";
        UpdateSelectedShape.Invoke();
       
      

       // ((MonoBehaviour)UpdateSelectedShape.GetPersistentTarget(0)).SendMessage(UpdateSelectedShape.GetPersistentMethodName(0),name);
    }


    void Start()
    {
        
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return true;
            }
        }

        return false;
    }
    // Update is called once per frame
    void Update()
    {
      /*  Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit))
        {
            text.text = "Hit " + hit.collider.name;
          //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            text.text = "No HIT";
            //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }*/

        foreach (Touch touch in Input.touches)
        {
            Debug.Log("Touching at: " + touch.position);
       

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hit))
                {
                 


                    {
                        StateTracker.State state = StateTracker.instance.getCurrentState();
                        if (state == StateTracker.State.ShapeSelection && hit.transform.gameObject.tag=="shape")
                        {
                            SelectedShape = hit.transform.gameObject;
                            DisableOtherThanSelected(hit.transform.name);
                            UpdateShape(SelectedShape.name);
                            DisableAR();
                        }
                        //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                    }
                }
                else
                {
                  //  text.text = "No HIT";
                    //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
                
                     //  text.text = "Touch began at " + touch.position;
                Debug.Log("Touch phase began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch phase Moved");
              //  text.text = "Touch phase moved";
                if (SelectedShape != null && (inARMode == false))
                {
                    SelectedShape.transform.Rotate(touch.deltaPosition.y * rotationRate, -touch.deltaPosition.x * rotationRate, 0, Space.World);
                }
              //  cube.transform.RotateAround(Vector3.down, touch.deltaPosition.x * rotationRate);
             //   cube.transform.RotateAround(Vector3.right, touch.deltaPosition.y * rotationRate);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
              //  text.text = "Touch phase ended";
               Debug.Log("Touch phase Ended");
            }
        }


    }

    public void DisableOtherThanSelected(string name)
    {
        switch (name)
        {
            case "Cube":
                Cube.SetActive(true);
                Cone.SetActive(false);
                Cylinder.SetActive(false);
                Sphere.SetActive(false);
                break;
            case "Cylinder":
                Cube.SetActive(false);
                Cone.SetActive(false);
                Cylinder.SetActive(true);
                Sphere.SetActive(false);
                break;
            case "Cone":
                Cube.SetActive(false);
                Cone.SetActive(true);
                Cylinder.SetActive(false);
                Sphere.SetActive(false);
                break;
            case "Sphere":
                Cube.SetActive(false);
                Cone.SetActive(false);
                Cylinder.SetActive(false);
                Sphere.SetActive(true);
                break;
            default:
                Debug.Log("Nothing selected");
                break;
        }
    }


    public void EnableAR()
    {
        inARMode = true;
    }

    public void DisableAR()
    {
        inARMode = false;
    }
}
