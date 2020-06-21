using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
  
    public GameObject Cube;
    public GameObject CubeDest;
    public GameObject Sphere;
    public GameObject SphereDest;
    public GameObject Cylinder;
    public GameObject CylinderDest;
    public GameObject Cone;
    public GameObject ConeDest;
    public GameObject SelectedShapePosition;
    public GameObject Background;
    public GameObject DisplayPosition;
    public bool mode = false;
    public float smooth = 1;
    Vector3[] startPos = new Vector3[4];
    Vector3[] DestPos= new Vector3[4];

    Quaternion ConeRotataion;
    // Start is called before the first frame update
    void Start()
    {

        startPos[0] = Cube.transform.localPosition;
        startPos[1] = Sphere.transform.localPosition;
        startPos[2] = Cylinder.transform.localPosition;
        startPos[3] = Cone.transform.localPosition;
        DestPos[0] = CubeDest.transform.localPosition;
        DestPos[1] = SphereDest.transform.localPosition;
        DestPos[2] = CylinderDest.transform.localPosition;
        DestPos[3] = ConeDest.transform.localPosition;

        ConeRotataion = Cone.transform.localRotation;
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        if (Background.activeSelf && mode)
        {
            Debug.Log("Moving");
            float delTime = Time.deltaTime;
            //Lerp to position
            Cube.transform.localPosition = Vector3.Lerp(Cube.transform.localPosition, DestPos[0], delTime * smooth);
            Cone.transform.localPosition = Vector3.Lerp(Cone.transform.localPosition, DestPos[3], delTime * smooth);
            Cylinder.transform.localPosition = Vector3.Lerp(Cylinder.transform.localPosition, DestPos[2], delTime * smooth);
            Sphere.transform.localPosition = Vector3.Lerp(Sphere.transform.localPosition, DestPos[1], delTime * smooth);

            //Lerp to Rotation
            if (StateTracker.instance.getCurrentState() == StateTracker.State.ShapeSelection || StateTracker.instance.getCurrentState() == StateTracker.State.MainMenu)
            {
                Cube.transform.localRotation = Quaternion.Lerp(Cube.transform.localRotation, Quaternion.identity, delTime * smooth);
                Cone.transform.localRotation = Quaternion.Lerp(Cone.transform.localRotation, ConeRotataion, delTime * smooth);
                Cylinder.transform.localRotation = Quaternion.Lerp(Cylinder.transform.localRotation, Quaternion.identity, delTime * smooth);
                Sphere.transform.localRotation = Quaternion.Lerp(Sphere.transform.localRotation, Quaternion.identity, delTime * smooth);
            }

        }

    }

    public void UpdatePositionToShapeSelection()
    {
        Cube.SetActive(true);
        Cone.SetActive(true);
        Cylinder.SetActive(true);
        Sphere.SetActive(true);
        DestPos[0] = CubeDest.transform.localPosition;
        DestPos[1] = SphereDest.transform.localPosition;
        DestPos[2] = CylinderDest.transform.localPosition;
        DestPos[3] = ConeDest.transform.localPosition;
        mode = true;
        Debug.Log("Learn clicked");
    }

    public void quizmode()
    {

    }

    public void UpdatePositionToMainMenu()
    {
        DestPos[0] = startPos[0];
        DestPos[1] = startPos[1];
        DestPos[2] = startPos[2];
        DestPos[3] = startPos[3];

    }


    public void UpdateSelectedShapePosition()
    {
        if (Cone.activeSelf)
        {
            DestPos[3] = SelectedShapePosition.transform.localPosition;
        }
        else
            if (Cube.activeSelf)
        {
            DestPos[0] = SelectedShapePosition.transform.localPosition;
        }
        else
            if (Cylinder.activeSelf)
        {
            DestPos[2] = SelectedShapePosition.transform.localPosition;
        }
        else
        if(Sphere.activeSelf)
        {
            DestPos[1] = SelectedShapePosition.transform.localPosition;
        }
    }

    public void UpdateSelectedToDisplayPosition()
    {
        if (Cone.activeSelf)
        {
            DestPos[3] = DisplayPosition.transform.localPosition;
        }
        else
            if (Cube.activeSelf)
        {
            DestPos[0] = DisplayPosition.transform.localPosition;
        }
        else
            if (Cylinder.activeSelf)
        {
            DestPos[2] = DisplayPosition.transform.localPosition;
        }
        else
        if (Sphere.activeSelf)
        {
            DestPos[1] = DisplayPosition.transform.localPosition;
        }
    }

}
