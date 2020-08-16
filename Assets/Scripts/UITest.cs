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
    public GameObject QuizManagerGameObject;
    public GameObject LearningManagerGameObject;
    private string mode = null;


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
        LearningManagerGameObject.SetActive(true);
        QuizMenu.SetActive(false);
        QuizManagerGameObject.SetActive(false);
        MainMenu.SetActive(false);

       // DisplayBox.SetActive(true);
    }

    public void GoToQuizMenu()
    {
        StateTracker.instance.setCurrentState(StateTracker.State.QuizMode);
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(false);
        LearningManagerGameObject.SetActive(false);
        QuizMenu.SetActive(true);
        QuizManagerGameObject.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void GoToShapeSelectionMenu(string modename)
    {

        mode = modename;
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

        ShapePartReferences.instance.DisableAllParts();
    }

    public void EnableSelectedMode()
    {
        if(mode == "learn")
        {
            GoToLearningMenu();
        }
        else if(mode == "quiz")
        {
            GoToQuizMenu();
        }
    }

    public void GoToMainMenu()
    {
        StateTracker.instance.setCurrentState(StateTracker.State.MainMenu);
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(false);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(true);
        LearningManagerGameObject.SetActive(false);
        QuizManagerGameObject.SetActive(false);
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
