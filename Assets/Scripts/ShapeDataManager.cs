﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDataManager : MonoBehaviour
{

    [System.Serializable]
    public class ShapeData
    {
        [System.Serializable]
        public class ComponentData
        {
            public int componentCount;
            [TextArea]
            public string componentDetails;
        }

        public string shapeName;

        [TextArea]
        public string shapeDetails;
        //public string componentname;
        // public string componentdetails;
        public ComponentData edgedata;
        public ComponentData vertexdata;
        public ComponentData facedata;
        public ComponentData heightdata;
        public ComponentData radiusdata;

        public string areaHeader;
        [TextArea]
        public string areaDetails;
        public string volumeHeader;
        [TextArea]
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
 
    public void setComponentDetails(string shapename, string partName)
    {
        string partdata = "";
        switch (partName)
        {
            case "edge": partdata = shapedata[shapeList[shapename]].edgedata.componentDetails;
                break;
            case "face":
                partdata = shapedata[shapeList[shapename]].facedata.componentDetails;
                break;
            case "vertex":
                partdata = shapedata[shapeList[shapename]].vertexdata.componentDetails;
                break;
            case "radius":
                partdata = shapedata[shapeList[shapename]].radiusdata.componentDetails;
                break;
            case "height":
                partdata = shapedata[shapeList[shapename]].heightdata.componentDetails;
                break;
        }

        ComponentHeader.text = partName.ToUpper() ; 
        ComponentBody.text = partdata;
    }
}
