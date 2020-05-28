using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour
{
    public Text text;
    public float rotationRate = 0.01f;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
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
                text.text = "Touch began at " + touch.position;
                Debug.Log("Touch phase began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch phase Moved");
                text.text = "Touch phase moved";
              cube.transform.Rotate(touch.deltaPosition.y * rotationRate, -touch.deltaPosition.x * rotationRate, 0, Space.World);

              //  cube.transform.RotateAround(Vector3.down, touch.deltaPosition.x * rotationRate);
             //   cube.transform.RotateAround(Vector3.right, touch.deltaPosition.y * rotationRate);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                text.text = "Touch phase ended";
               Debug.Log("Touch phase Ended");
            }
        }


    }
}
