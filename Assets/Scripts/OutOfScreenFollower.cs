using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreenFollower : MonoBehaviour
{

    public RectTransform arrow;
    public float speed = 1.0f;
    public GameObject selectedObject = null;
    private bool enable = false;

    public static OutOfScreenFollower instance = null;

    public void Start()
    {
    
        if (instance != null && instance != this)
        {
   
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("Starting " + this.name);
    }

    private void Update()
    {
        if (enable && (selectedObject != null))
        {
            Vector3 objScreenPos = Camera.main.WorldToScreenPoint(selectedObject.transform.position);

            Debug.Log("SCREEN POS " + objScreenPos);

            Debug.Log("Width " + Screen.width + " : Height " + Screen.height);
            // Get the directional vector between your arrow and the object
            Vector3 dir = (objScreenPos - arrow.position).normalized;



            // Calculate the angle 
            // We assume the default arrow position at 0° is "up"
            float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

            // Use the cross product to determine if the angle is clockwise
            // or anticlockwise
            Vector3 cross = Vector3.Cross(dir, Vector3.up);
            angle = -Mathf.Sign(cross.z) * angle;

            // Update the rotation of your arrow
            arrow.localEulerAngles = new Vector3(arrow.localEulerAngles.x, arrow.localEulerAngles.y, angle);
            Debug.Log("Arrow rot " + arrow.eulerAngles);

            float x = Mathf.Clamp(objScreenPos.x, 0 + arrow.rect.width, Screen.width - arrow.rect.width);
            float y = Mathf.Clamp(objScreenPos.y, 0 + arrow.rect.width, Screen.height - arrow.rect.width);
            arrow.transform.position = new Vector3(x, y, objScreenPos.z);
        }

    }

    
    public void EnableFollow()
    {
        if (StateTracker.instance.getARState())
        {
            arrow.gameObject.SetActive(true);
            enable = true;
        }
    }

    public void DisableFollow()
    {
      
            arrow.gameObject.SetActive(false);
            enable = false;
        
    }
   
}
