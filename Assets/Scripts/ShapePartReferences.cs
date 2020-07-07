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
        public Part[] PartReferences;

    }

    public ShapeParts[] ShapeReferences;


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
                        for(int k= 0; k < ShapeReferences[i].PartReferences[j].part.transform.childCount; k++)
                        ShapeReferences[i].PartReferences[j].part.transform.GetChild(k).GetChild(0).gameObject.SetActive(true);

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


}
