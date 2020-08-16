using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFollower : MonoBehaviour
{
    // public GameObject ARCamera;

    // Start is called before the first frame update
    GameObject tempobj;

    void Start()
    {
        tempobj = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        

        Transform tempTrans = tempobj.transform;
           //Camera.main.transform;
        tempTrans.position = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y , Camera.main.transform.position.z);
        gameObject.transform.LookAt(tempTrans);
        //gameObject.transform.g


    }
}
