using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePartReferences : MonoBehaviour
{

    [System.Serializable]
    public class ShapeParts
    {
        [System.Serializable]
        public class Part
        {
            public string partname;
            public GameObject part;
           
        }
        public string ShapeName;
        public GameObject shape;
        public Part[] PartReferences;

    }

    public ShapeParts[] ShapeReferences;
 /*   public GameObject ConeMask;
    public GameObject Cylindermask1;
    public GameObject Cylindermask2; */
    public Material highlight;
    public Material facenormal;
    public Material linerendererNormal;
    public static ShapePartReferences instance = null;

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


    public void EnableShapePart(string ShapeName,string PartName)
    {
        for (int i = 0; i < ShapeReferences.Length; i++) {
            string shape = ShapeReferences[i].ShapeName;

            if(shape == ShapeName)
            {
                for(int j=0; j < ShapeReferences[i].PartReferences.Length; j++)
                {
                    string part = ShapeReferences[i].PartReferences[j].partname;
                    if (part == PartName)
                    {
                        ShapeReferences[i].PartReferences[j].part.SetActive(true);
                        for (int k = 0; k < ShapeReferences[i].PartReferences[j].part.transform.childCount; k++)
                        {
                            ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(true);

                            if(part!="vertex")
                            for (int l = 0; l < ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).GetChildCount(); l++)
                            {
                                ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).GetChild(l).gameObject.SetActive(false);

                            }

                           
                        }

                    }
                }
            }
         }
    }

    public void DisableShapePart(string ShapeName, string PartName)
    {
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
            string shape = ShapeReferences[i].ShapeName;

            if (shape == ShapeName)
            {
                for (int j = 0; j < ShapeReferences[i].PartReferences.Length; j++)
                {
                    string part = ShapeReferences[i].PartReferences[j].partname;
                    if (part == PartName)
                    {
                        ShapeReferences[i].PartReferences[j].part.SetActive(false);
                        Debug.Log("Disable " + part + " size : " + ShapeReferences[i].PartReferences[j].part.transform.childCount);
                        for (int k = 0; k < ShapeReferences[i].PartReferences[j].part.transform.childCount; k++)
                            ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void DisableAllParts()
    {
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
          
                for (int j = 0; j < ShapeReferences[i].PartReferences.Length; j++)
                {
                    
                        ShapeReferences[i].PartReferences[j].part.SetActive(false);
                      
                        for (int k = 0; k < ShapeReferences[i].PartReferences[j].part.transform.childCount; k++)
                            ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(false);
                    
                }
            
        }
    }

    public void HightlightPart(string ShapeName, string PartName)
    {
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
            string shape = ShapeReferences[i].ShapeName;

            if (shape == ShapeName)
            {
                for (int j = 0; j < ShapeReferences[i].PartReferences.Length; j++)
                {
                    string part = ShapeReferences[i].PartReferences[j].partname;
                    if (part == PartName)
                    {
                      //  ShapeReferences[i].PartReferences[j].part.SetActive(false);
                       // Debug.Log("Disable " + part + " size : " + ShapeReferences[i].PartReferences[j].part.transform.childCount);
                        for (int k = 0; k < ShapeReferences[i].PartReferences[j].part.transform.childCount; k++)
                        {
                            switch (part)
                            {
                                case "face":
                                    ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<Renderer>().material = highlight;
                                    break;
                                case "edge":
                                    if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>() != null)
                                        ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>().material = highlight ;
                                    break;
                                case "vertex":
                                    if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>() != null)
                                    {
                                        ParticleSystemRenderer ps = ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>();
                                        ps.material.color = Color.red;
                                      //  ParticleSystem.MainModule ma = ps.main;
                                        //ma.startColor = Color.red;
                                       
                                    }
                                    break;
                                case "height":
                                    if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>() != null)
                                        ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>().material = highlight;
                                    break;
                                case "radius":
                                    if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>() != null)
                                        ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>().material = highlight;
                                    break;
                            }
                        }
                          //  ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }

    }


    public void NonHightlightAllParts()
    {
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
            string shape = ShapeReferences[i].ShapeName;

           
                for (int j = 0; j < ShapeReferences[i].PartReferences.Length; j++)
                {
                    string part = ShapeReferences[i].PartReferences[j].partname;
                 
                        //  ShapeReferences[i].PartReferences[j].part.SetActive(false);
                        // Debug.Log("Disable " + part + " size : " + ShapeReferences[i].PartReferences[j].part.transform.childCount);
                        for (int k = 0; k < ShapeReferences[i].PartReferences[j].part.transform.childCount; k++)
                        {
                            switch (part)
                            {
                                case "face":
                                    ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<Renderer>().material = facenormal;
                                    break;
                                case "edge":
                                    if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>() != null)
                                    ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>().material = linerendererNormal;
                                     break;
                        case "height":
                            if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>() != null)
                                ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>().material = linerendererNormal;
                            break;
                        case "radius":
                            if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>() != null)
                                ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.GetComponent<LineRenderer>().material = linerendererNormal;
                            break;
                        case "vertex":
                            if (ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>() != null)
                            {
                                ParticleSystemRenderer ps = ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>();
                                ps.material.color = Color.white;
                               // ParticleSystem.MainModule ma = ps.main;
                               // ma.startColor = Color.white;
                               // ps.Play();
                            }
                            break;
                    }
                        }
                        //  ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(false);
                    
                }
            
        }

    }

    public int getComponentCount(string ShapeName, string PartName)
    {
        int count = 0;
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
            string shape = ShapeReferences[i].ShapeName;

            if (shape == ShapeName)
            {
                for (int j = 0; j < ShapeReferences[i].PartReferences.Length; j++)
                {
                    string part = ShapeReferences[i].PartReferences[j].partname;
                    if (part == PartName)
                    {
                        count = ShapeReferences[i].PartReferences[j].part.transform.childCount;
                        //  ShapeReferences[i].PartReferences[j].part.SetActive(false);
                        // Debug.Log("Disable " + part + " size : " + ShapeReferences[i].PartReferences[j].part.transform.childCount);

                        //  ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }


        return count;
    }

    public void MakeAllShapeTransparent()
    {
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
           Color shapeColor =  ShapeReferences[i].shape.GetComponent<MeshRenderer>().material.color ;
            ShapeReferences[i].shape.GetComponent<MeshRenderer>().material.color = new Color(shapeColor.r, shapeColor.g, shapeColor.b, 0.65f);

            //  = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
     }

    public void MakeAllShapesOpaque()
    {
        for (int i = 0; i < ShapeReferences.Length; i++)
        {
            Color shapeColor = ShapeReferences[i].shape.GetComponent<MeshRenderer>().material.color;
            ShapeReferences[i].shape.GetComponent<MeshRenderer>().material.color = new Color(shapeColor.r, shapeColor.g, shapeColor.b, 1);
        }
    }

}
