using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update




  


    public GameObject ShapeSelectionMenu; 
    public GameObject LearningMenu;
    public GameObject MainMenu;
    public GameObject QuizMenu;
    public Text feedback;
    public GameObject Background3d;
    public GameObject DisplayBox;
    public GameObject ToggleButton;

    [System.Serializable]
    public class InteractionEvent : UnityEvent { }

    public InteractionEvent OnShapeSelectionMenuSwitch = new InteractionEvent();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

  
    public void GoToLearningMenu()
    {
        StateTracker.instance.setCurrentState(StateTracker.State.LearningMode);
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(true);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(false);
       // DisplayBox.SetActive(true);
    }

    public void GoToQuizMenu()
    {
        StateTracker.instance.setCurrentState(StateTracker.State.QuizMode);
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void GoToShapeSelectionMenu()
    {
       // SwitchTo3D();
        StateTracker.instance.setCurrentState(StateTracker.State.ShapeSelection);
        if (!Background3d.activeSelf)
        {
            ToggleButton.GetComponent<Button>().onClick.Invoke();

        }
        ShapeSelectionMenu.SetActive(true);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(false);
       // Background3d.SetActive(true);
        DisplayBox.SetActive(false);
    }


    public void GoToMainMenu()
    {
        StateTracker.instance.setCurrentState(StateTracker.State.MainMenu);
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
        // feedback.text = "Switched to 3D " + name;
        OnShapeSelectionMenuSwitch.Invoke();
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
