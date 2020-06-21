using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDataManager : MonoBehaviour
{

    [System.Serializable]
    public class ShapeData
    {
        public string shapeName;
        public string shapeDetails;
        public string componentname;
        public string componentdetails;
        public string areaHeader;
        public string areaDetails;
        public string volumeHeader;
        public string volumeDetails;
    }


    public ShapeData[] shapedata;

    public TextMesh ShapeDetailsHeader;
    public TextMesh ShapeDetailsBody;

    public TextMesh ShapeAreaHeader;
    public TextMesh ShapeAreaBody;

    public TextMesh ShapeVolumeHeader;
    public TextMesh ShapeVolumeBody;

    public TextMesh ComponentHeader;
    public TextMesh ComponentBody;


    public static ShapeDataManager instance = null;

    Dictionary<string, int> shapeList = new Dictionary<string, int>();

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        for(int i = 0; i < shapedata.Length; i++)
        {
            shapeList.Add(shapedata[i].shapeName, i);
        }
    }


    public void setShapeDetails(string shapename)
    {
        ShapeDetailsHeader.text = shapedata[shapeList[shapename]].shapeName;
        ShapeDetailsBody.text = shapedata[shapeList[shapename]].shapeDetails;
    }

    public void setShapeAreaDetails(string shapename)
    {
        ShapeAreaHeader.text = shapedata[shapeList[shapename]].areaHeader;
        ShapeAreaBody.text = shapedata[shapeList[shapename]].areaDetails;
    }

    public void setShapeVolumeDetails(string shapename)
    {
        ShapeVolumeHeader.text = shapedata[shapeList[shapename]].volumeHeader; 
        ShapeVolumeBody.text = shapedata[shapeList[shapename]].volumeDetails; 
    }
 
    public void setComponentDetails(string shapename)
    {
        ComponentHeader.text = shapedata[shapeList[shapename]].componentname; 
        ComponentBody.text = shapedata[shapeList[shapename]].componentdetails;
    }
}
