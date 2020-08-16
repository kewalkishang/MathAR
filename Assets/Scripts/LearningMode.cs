using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GoogleARCore.Examples.HelloAR;
public class LearningMode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ShapeDetails;
    public GameObject AreaDetails;
    public GameObject VolumeDetails;
    public GameObject ComponentsDetails;
    public GameObject ComponentMenu;
    //public GameObject ToggleButton;
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
    public GameObject edge;
    public GameObject height;
    public GameObject face;
    public GameObject vertex;
    public GameObject radius;
    public GameObject hinfo;
    public GameObject rinfo;
    public ToggleSwitch edgeToggle;
    public ToggleSwitch heightToggle;
    public ToggleSwitch faceToggle;
    public ToggleSwitch vertexToggle;
    public ToggleSwitch radiusToggle;
    public Slider componentSlider;
    public GameObject feedbackText;

    string CurrentComponentInfoDisplayed;
    List<string> EnabledParts = new List<string>();
    public float speed = 10f;
    bool rotating = false;
    bool  isAreaandVolume = false;
    /* [System.Serializable]
     public class InteractionEvent : UnityEvent { }

     public InteractionEvent OnInfoActivated = new InteractionEvent();
     public InteractionEvent OnInfoDisabled = new InteractionEvent();
     */
    void Start()
    {
        
    }

  /*  public static bool IsPointerOverUIObject()
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
    }*/
    // Update is called once per frame
    void Update()
    {

     


     /*   if (Input.GetMouseButtonDown(0))
        {
            if (!StateTracker.instance.getARState())
            {
                float XaxisRotation = Input.GetAxis("Mouse X") * speed;
                float YaxisRotation = Input.GetAxis("Mouse Y") * speed;
                // select the axis by which you want to rotate the GameObject
                SelectedShape.transform.RotateAround(Vector3.down, XaxisRotation);
                SelectedShape.transform.RotateAround(Vector3.right, YaxisRotation);
            }



            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log("You hit part " + hit.transform.name);
                StateTracker.State state = StateTracker.instance.getCurrentState();
                if (state == StateTracker.State.LearningMode )
                {
                    Debug.Log("You hit part " + hit.transform.name); // ensure you picked right object
                                                                     //GameObject hitpart = hit.transform.gameObject;
                    if (hit.transform.gameObject.tag == "part")
                        SetTouchedComponentInformation(SelectedShape.name, hit.transform.name);
                    //EnableComponentInformation(SelectedShape.name, hit.transform.name);

                    if (hit.transform.gameObject.tag == "close")
                        CloseAllInformation();
                }
            }
        } 
        */
         if(Input.touchCount == 1)
         {
            Touch touch = Input.GetTouch(0);
             Debug.Log("Touching at: " + touch.position);


             if (touch.phase == TouchPhase.Began)
             {
                 RaycastHit hit;
                 Ray ray = Camera.main.ScreenPointToRay(touch.position);
                 // Does the ray intersect any objects excluding the player layer
                 if (Physics.Raycast(ray, out hit))
                 {

                    StateTracker.State state = StateTracker.instance.getCurrentState();
                    if (state == StateTracker.State.LearningMode)
                    {
                        Debug.Log("You hit part " + hit.transform.name); // ensure you picked right object
                                                                         //GameObject hitpart = hit.transform.gameObject;
                        if (hit.transform.gameObject.tag == "part")
                            SetTouchedComponentInformation(SelectedShape.name, hit.transform.name);
                        //EnableComponentInformation(SelectedShape.name, hit.transform.name);

                        if (hit.transform.gameObject.tag == "close")
                            CloseAllInformation();

                        if(hit.transform.gameObject.tag == "mask")
                        {
                            if(faceToggle.State)
                            {
                                SetTouchedComponentInformation(SelectedShape.name, "face");
                            }
                        }

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

                if (SelectedShape != null && StateTracker.instance.getCurrentState() == StateTracker.State.LearningMode && !HelloARController.instance.dragging)
                 {
                    //SelectedShape.transform.Rotate(touch.deltaPosition.y * rotationRate, -touch.deltaPosition.x * rotationRate, 0, Space.World);
                    Camera camera = Camera.main;

                    Vector3 right = Vector3.Cross(camera.transform.up, SelectedShape.transform.position - camera.transform.position);

                    Vector3 up = Vector3.Cross(SelectedShape.transform.position - camera.transform.position, right);

                    SelectedShape.transform.rotation = Quaternion.AngleAxis(-touch.deltaPosition.x * rotationRate, up) * SelectedShape.transform.rotation;

                    SelectedShape.transform.rotation = Quaternion.AngleAxis(touch.deltaPosition.y * rotationRate, right) * SelectedShape.transform.rotation;

                    rotating = true;
                }
           
             }
             else if (touch.phase == TouchPhase.Ended)
             {
                 //  text.text = "Touch phase ended";
                 Debug.Log("Touch phase Ended");
                if (rotating)
                {
                    feedbackText.SetActive(false);
                    rotating = false;
                }
            }
         }
    }

    public void EnableAreaInformation()
    {
       // UpdateSelectedShape();
        ResetToggle();
   
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(true);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(false);
        MoveToDIsplayPosition();
        SetupShapeforAreaandVolume();
        feedbackText.SetActive(false);
        isAreaandVolume = true;
        // infoON = true;
    }


    public void SetupShapeforAreaandVolume()
    {
        ShapePartReferences.instance.MakeAllShapeTransparent();
        switch (SelectedShape.name)
        {
            case "Cube":
                edgeToggle.State = true;
                EnablePart("edge");
                break;
            case "Sphere":
                radiusToggle.State = true;
                EnablePart("radius");
             
                break;
            case "Cylinder":
                heightToggle.State = true;
                EnablePart("height");
                radiusToggle.State = true;
                EnablePart("radius");
                break;
            case "Cone":
                heightToggle.State = true;
                EnablePart("height");
                radiusToggle.State = true;
                EnablePart("radius");
                break;
        }

        rinfo.SetActive(false);
        hinfo.SetActive(false);
    }

    public void EnableVolumeInformation()
    {
     //   UpdateSelectedShape();
        ResetToggle();
   
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(true);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(false);
        MoveToDIsplayPosition();
        
        SetupShapeforAreaandVolume();
        feedbackText.SetActive(false);
        isAreaandVolume = true;
        // infoON = true;
    }

    public void EnableShapeInformation()
    {
        //  UpdateSelectedShape();
        ShapePartReferences.instance.MakeAllShapesOpaque();
        ResetToggle();
        feedbackText.SetActive(false);
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(true);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(false);
        MoveToDIsplayPosition();
        isAreaandVolume = false;
        // infoON = true;
    }

    public void EnableComponentInformation()
    {
        //   UpdateSelectedShape();
        ShapePartReferences.instance.MakeAllShapeTransparent();
        // feedbackText.SetActive(true);
        ResetToggle();
        if (!componentSlider.open)
        {
            componentSlider.Slide();
        }

      
        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        ComponentMenu.SetActive(true);
        MoveToSelectedPosition();
        isAreaandVolume = false;
       // MoveToDIsplayPosition();

        //infoON = true;
    }

    public void EnableHeightInfo()
    {
        SetTouchedComponentInformation(SelectedShape.name, "height");
      //  hinfo.SetActive(false);
    }

    public void EnableRadiusInfo()
    {
        SetTouchedComponentInformation(SelectedShape.name, "radius");
      //  rinfo.SetActive(false)
    }

    public void SetTouchedComponentInformation(string shapename, string partname)
    {
        CurrentComponentInfoDisplayed = partname;
        feedbackText.SetActive(false);
        ComponentsDetails.SetActive(true);
        ShapeDataManager.instance.setComponentDetails(shapename, partname);
        ShapePartReferences.instance.NonHightlightAllParts();
        ShapePartReferences.instance.HightlightPart(shapename, partname);
        MoveToDIsplayPosition();
    }

    private void OnDisable()
    {
        feedbackText.SetActive(false);
        CloseAllInformation();
        ResetToggle();
    }

    public void CloseAllInformation()
    {
        //   UpdateSelectedShape();
        if (isAreaandVolume)
            ResetToggle();


        DisplayBox.SetActive(true);
        ShapeDetails.SetActive(false);
        AreaDetails.SetActive(false);
        VolumeDetails.SetActive(false);
        ComponentsDetails.SetActive(false);
        ShapePartReferences.instance.NonHightlightAllParts();
        MoveToSelectedPosition();
      //  infoON = false;
        //  ComponentMenu.SetActive()
    }

    public void EnablePart(string part)
    {
        feedbackText.SetActive(true);


      
            Debug.Log("EnablePart "+ part);
        EnabledParts.Add(part);
       
        ShapePartReferences.instance.EnableShapePart(SelectedShape.name, part);

        if (ShapePartReferences.instance.getComponentCount(SelectedShape.name, part) > 0)
        {
            feedbackText.GetComponent<Text>().text = "Touch on the enabled part for more info.";

            if (part == "height")
            {
                hinfo.SetActive(true);
                feedbackText.GetComponent<Text>().text = "Click on the info button for more.";
            }
            if (part == "radius")
            {
                rinfo.SetActive(true);
                feedbackText.GetComponent<Text>().text = "Click on the info button for more.";
            }
        }
        else
        {
            feedbackText.GetComponent<Text>().text = SelectedShape.name + " doesnt have " + part;

        }

  

    }

    public void DisablePart(string part)
    {
        Debug.Log("DisablePart " + part);
        EnabledParts.Remove(part);
        ShapePartReferences.instance.DisableShapePart(SelectedShape.name, part);

        if(part == CurrentComponentInfoDisplayed)
        {
            CloseAllInformation();
        }

        if (part == "height")
        {
            hinfo.SetActive(false);
        }
        if (part == "radius")
        {
            rinfo.SetActive(false);
        }
    }


    public void ToggleClicked(string part)
    {
        if (!EnabledParts.Contains(part))
        {
            EnablePart(part);
        }
        else
        {
            DisablePart(part);
        }

    }

    public void ResetToggle()
    {




        if (vertexToggle.State)
        {
            vertexToggle.State = false;
            DisablePart("vertex");
        }

        if (edgeToggle.State)
        {
            edgeToggle.State = false;
            DisablePart("edge");
        }

        if (faceToggle.State)
        {
            faceToggle.State = false;
            DisablePart("face");
        }

        if (heightToggle.State)
        {
            heightToggle.State = false;
            DisablePart("height");
            hinfo.SetActive(false);
        }
        if (radiusToggle.State)
        {
            radiusToggle.State = false;
            DisablePart("radius");
            rinfo.SetActive(false);
        }




  
    }

    private void OnEnable()
    {
        feedbackText.SetActive(true);
        feedbackText.GetComponent<Text>().text = "Swipe on screen to rotate the Shape";
        UpdateSelectedShape();
        hinfo.SetActive(false);
        rinfo.SetActive(false);
        Debug.Log("Learning mode updateshape called");
        rotating = false;
    }

    public void UpdateSelectedShape()
    { 
       
        SelectedShape = null;

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
        // OnInfoActivated.Invoke();  
        LerpTest.instance.UpdateSelectedToDisplayPosition();
    }


    public void MoveToSelectedPosition()
    {
        LerpTest.instance.UpdateSelectedShapePosition();
        //OnInfoDisabled.Invoke();
    }
}
