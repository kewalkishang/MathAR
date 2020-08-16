using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public GameObject sphere; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTransparency()
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);

    }

    public void getVertices()
    {


        List<Vector3> vertices = new List<Vector3>();
        BoxCollider box = GetComponent<BoxCollider>();
        Debug.Log("Vertices Centre "+ box.bounds.center);

        float xmid = box.bounds.center.x;
        float ymid = box.bounds.center.y;
        float zmid = box.bounds.center.z;

        float xext = box.bounds.extents.x;
        float yext = box.bounds.extents.y;
        float zext = box.bounds.extents.z;

        Vector3 ver1 = new Vector3(xmid + xext, ymid + yext, zmid - zext);
        Vector3 ver2 = new Vector3(xmid + xext, ymid - yext, zmid - zext);
        Vector3 ver3 = new Vector3(xmid - xext, ymid - yext, zmid - zext);
        Vector3 ver4 = new Vector3(xmid - xext, ymid + yext, zmid - zext);

        Vector3 ver5 = new Vector3(xmid + xext, ymid + yext, zmid + zext);
        Vector3 ver6 = new Vector3(xmid + xext, ymid - yext, zmid + zext);
        Vector3 ver7 = new Vector3(xmid - xext, ymid - yext, zmid + zext);
        Vector3 ver8 = new Vector3(xmid - xext, ymid + yext, zmid + zext);

        /*
         GameObject v1 = Instantiate(sphere, ver1, Quaternion.identity);
         GameObject v2 = Instantiate(sphere, ver2, Quaternion.identity);
         GameObject v3 = Instantiate(sphere, ver3, Quaternion.identity);
         GameObject v4 = Instantiate(sphere, ver4, Quaternion.identity);
         GameObject v5 = Instantiate(sphere, ver5, Quaternion.identity);
         GameObject v6 = Instantiate(sphere, ver6, Quaternion.identity);
        GameObject v7 = Instantiate(sphere, ver7, Quaternion.identity);
         GameObject v8 = Instantiate(sphere, ver8, Quaternion.identity);
         */



        vertices.Add(ver1);
        vertices.Add(ver2);
        vertices.Add(ver3);
        vertices.Add(ver4);
       // highlightEdge(vertices);

    }


    public void highlightFace()
    {

    }


    public void highlightEdge(List<Vector3> vert)
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.positionCount = vert.Count;
        for (int i = 0; i < vert.Count; i++)
        {
            Debug.Log("i :" + i);
            lineRenderer.SetPosition(i,vert[i]);
        }
    }

}
