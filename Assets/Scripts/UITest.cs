using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ShapeSelectionMenu; 
    public GameObject LearningMenu;
    public GameObject QuizMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToShapeSelectionMenu()
    {
        ShapeSelectionMenu.SetActive(true);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(false);
    }

    public void GoToLearningMenu()
    {
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(true);
        QuizMenu.SetActive(false);
    }

    public void GoToQuizMenu()
    {
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(true);
    }

    public void SwitchToAR()
    {

    }

    public void SwitchTo3D()
    {

    }


}
