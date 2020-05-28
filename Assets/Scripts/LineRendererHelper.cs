using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    public GameObject StartingPos;
    public GameObject EndingPos;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        highlightEdge();
    }

    // Update is called once per frame
    void Update()
    {
      //  lineRenderer.SetPosition(0, StartingPos.transform.localPosition);
       // lineRenderer.SetPosition(1, EndingPos.transform.localPosition);
    }

    public void highlightEdge()
    {

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.SetPosition(0, StartingPos.transform.localPosition);
        lineRenderer.SetPosition(1, EndingPos.transform.localPosition);
        
    }
}
