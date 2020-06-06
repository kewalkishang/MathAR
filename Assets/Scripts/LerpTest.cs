using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SelectShapeMenu;
    public GameObject Cube;
    public GameObject CubeDest;
    public GameObject Sphere;
    public GameObject SphereDest;
    public GameObject Cylinder;
    public GameObject CylinderDest;
    public GameObject Cone;
    public GameObject ConeDest;
    public bool mode = false;
    public float smooth = 1;
    Vector3[] startPos = new Vector3[4];
    Vector3[] DestPos= new Vector3[4]; 
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
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        if (mode)
        {
            Debug.Log("Moving");
            float delTime = Time.deltaTime; 
            Cube.transform.localPosition = Vector3.Lerp(Cube.transform.localPosition, DestPos[0], delTime * smooth);
            Cone.transform.localPosition = Vector3.Lerp(Cone.transform.localPosition, DestPos[3], delTime * smooth);
            Cylinder.transform.localPosition = Vector3.Lerp(Cylinder.transform.localPosition, DestPos[2], delTime * smooth);
            Sphere.transform.localPosition = Vector3.Lerp(Sphere.transform.localPosition, DestPos[1], delTime * smooth);
    
        }

    }

    public void learnmode()
    {
        DestPos[0] = CubeDest.transform.localPosition;
        DestPos[1] = SphereDest.transform.localPosition;
        DestPos[2] = CylinderDest.transform.localPosition;
        DestPos[3] = ConeDest.transform.localPosition;
        MainMenu.SetActive(false);
        SelectShapeMenu.SetActive(true);
        mode = true;
        Debug.Log("Learn clicked");
    }

    public void quizmode()
    {

    }

    public void BackToMainMenu()
    {
        DestPos[0] = startPos[0];
        DestPos[1] = startPos[1];
        DestPos[2] = startPos[2];
        DestPos[3] = startPos[3];
        MainMenu.SetActive(true);
        SelectShapeMenu.SetActive(false);
    }
}
