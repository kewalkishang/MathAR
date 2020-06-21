using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LearningMode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShapeDetails;
    public GameObject AreaDetails;
    public GameObject VolumeDetails;
    public GameObject ComponentsDetails;
    public GameObject ComponentMenu;
    public float ModeSpeed = 10f;
    GameObject SelectedShape = null;
    public GameObject Cube;
    public GameObject Cone;
    public GameObject Cylinder;
    public GameObject Sphere;
    public GameObject SidePosition;
    bool infoON = false;
    public GameObject StartingPos;
    public float rotationRate = 0.01f;
    public GameObject Background;
    public GameObject DisplayBox;

    [System.Serializable]
    public class InteractionEvent : UnityEvent { }

    public InteractionEvent OnInfoActivated = new InteractionEvent();
    public InteractionEvent OnInfoDisabled = new InteractionEvent();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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


                    if(StateTracker.instance.getCurrentState() == StateTracker.State.LearningMode && hit.transform.gameObject.tag == "close")
                    {
                        CloseAllInformation();
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
                if (SelectedShape != null && (Background.activeSelf == true) && StateTracker.instance.getCurrentState() == StateTracker.State.LearningMode)
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

    public void EnableAreaInformation()
    {
        UpdateSelectedShape();
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(true);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(false);
        MoveToDIsplayPosition();
       // infoON = true;
    }

    public void EnableVolumeInformation()
    {
        UpdateSelectedShape();
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(true);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(false);
        MoveToDIsplayPosition();
        // infoON = true;
    }

    public void EnableShapeInformation()
    {
        UpdateSelectedShape();
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(true);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(false);
        MoveToDIsplayPosition();
        // infoON = true;
    }

    public void EnableComponentInformation()
    {
        UpdateSelectedShape();
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(true);
        ComponentMenu.SetActive(true);
        MoveToDIsplayPosition();
        //infoON = true;
    }

    public void CloseAllInformation()
    {
        UpdateSelectedShape();
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        MoveToSelectedPosition();
      //  infoON = false;
        //  ComponentMenu.SetActive()
    }

    public void UpdateSelectedShape()
    {



        if (Cone.activeSelf)
        {
            SelectedShape = Cone;
        }
        else
       if (Cube.activeSelf)
        {
            SelectedShape = Cube;
        }
        else
       if (Cylinder.activeSelf)
        {
            SelectedShape = Cylinder;
        }
        else
           if (Sphere.activeSelf)
        {
            SelectedShape = Sphere;
        }
    }


    public void MoveToDIsplayPosition()
    {
        OnInfoActivated.Invoke();  
    }


    public void MoveToSelectedPosition()
    {
        OnInfoDisabled.Invoke();
    }
}
