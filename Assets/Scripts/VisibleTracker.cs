using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleTracker : MonoBehaviour
{
    // Start is called before the first frame update

    bool ready = false;

    public void Start()
    {
        ready = true;
    }


    void OnBecameInvisible()
    {

        if (ready)
        {
            Debug.Log("invisible " + gameObject.name);
       
            OutOfScreenFollower.instance.selectedObject = this.gameObject;
            OutOfScreenFollower.instance.EnableFollow();

        }
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        if (ready)
        {
            Debug.Log("Visible " + gameObject.name);
      

         
            OutOfScreenFollower.instance.DisableFollow();
          
        }
     
    }
}
