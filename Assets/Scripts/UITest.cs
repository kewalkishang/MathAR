using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ShapeSelectionMenu; 
    public GameObject LearningMenu;
    public GameObject MainMenu;
    public GameObject QuizMenu;
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

  
    public void GoToLearningMenu()
    {
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(true);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(false);
    }

    public void GoToQuizMenu()
    {
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void GoToShapeSelectionMenu()
    {
        ShapeSelectionMenu.SetActive(true);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(false);
    }


    public void GoToMainMenu()
    {
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void learnmode()
    {
    
        //MainMenu.SetActive(false);
        //   SelectShapeMenu.SetActive(true);
    
        Debug.Log("Learn clicked");
    }
    public void SwitchToAR()
    {

    }

    public void SwitchTo3D()
    {

    }

    public void ToggleButtonOn()
    {
        Debug.Log("toggleButton ON clicked");
    }
    public void ToggleButtonOff()
    {
        Debug.Log("toggleButton OFF clicked");
    }
}
