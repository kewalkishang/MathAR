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
    public GameObject MCQMenu;
    public GameObject ComponentSlider;
    public Text feedback;
    public GameObject Background3d;
    public GameObject DisplayBox;
    public GameObject ToggleButton;
    public GameObject QuizManagerGameObject;
    public GameObject LearningManagerGameObject;
    public GameObject QuizSelectAllOption;
    private string mode = null;
    public static bool AllSelected;
    public GameObject CautionMessage;
    public GameObject ARParent;
    public GameObject InteractionDisplay;
    public GameObject InteractionGuideButton1;
    public GameObject InteractionGuideButton2;
    public GameObject QuizGameMenu;

    // [System.Serializable]
    //  public class InteractionEvent : UnityEvent { }


    //public InteractionEvent OnShapeSelectionMenuSwitch = new InteractionEvent();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuizGameMenu.SetActive(true);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisableQuitMenu(){
        QuizGameMenu.SetActive(false);
    }

    public void GoToLearningMenu()
    {
        StateTracker.instance.setCurrentState(StateTracker.State.LearningMode);
        ShapeSelectionMenu.SetActive(false);
        LearningMenu.SetActive(true);
        LearningManagerGameObject.SetActive(true);
       // QuizMenu.SetActive(false);
     //   QuizManagerGameObject.SetActive(false);
    //    MainMenu.SetActive(false);
        LerpTest.instance.UpdateSelectedShapePosition();
       // DisplayBox.SetActive(true);
    }

    public void GoToQuizMenu()
    {
        Debug.Log("QUIZ MENU");
        StateTracker.instance.setCurrentState(StateTracker.State.QuizMode);
        ShapeSelectionMenu.SetActive(false);
        
        QuizMenu.SetActive(true);
        QuizManagerGameObject.SetActive(true);
       // MainMenu.SetActive(false);
      //  LearningMenu.SetActive(false);
       // LearningManagerGameObject.SetActive(false);
        // LerpTest.instance.UpdateSelectedShapePosition();
    }

    public void GoToShapeSelectionMenu(string modename)
    {

        mode = modename;
        StateTracker.instance.setCurrentState(StateTracker.State.ShapeSelection);

        if (!Background3d.activeSelf)
        {
            ToggleButton.GetComponent<Button>().onClick.Invoke();
            Debug.Log("BUTTON TOGGLE CALLED");
        }

        ShapeSelectionMenu.SetActive(true);

        if(mode == "quiz")
        {
            QuizSelectAllOption.SetActive(true);
        }
        else
        {
            QuizSelectAllOption.SetActive(false);
        }

        LearningMenu.SetActive(false);
        QuizMenu.SetActive(false);
        MainMenu.SetActive(false);
        MCQMenu.SetActive(false);
        QuizManagerGameObject.SetActive(false);
        LearningManagerGameObject.SetActive(false);
       // Background3d.SetActive(true);
        DisplayBox.SetActive(false);
        ComponentSlider.SetActive(false);
        ShapePartReferences.instance.MakeAllShapesOpaque();
        LerpTest.instance.UpdatePositionToShapeSelection();
        ShapePartReferences.instance.DisableAllParts();
        ARParent.SetActive(false);
        InteractionGuideButton1.SetActive(false);
        InteractionGuideButton2.SetActive(false);
        DisableInteraction();
    }

    public void EnableSelectedMode()
    {
        Debug.Log("Enable selected");
        if(mode == "learn")
        {
            GoToLearningMenu();
        }
        else if(mode == "quiz")
        {
            AllSelected = false;
            GoToQuizMenu();
        }
    }

    public void EnableQuizforAll()
    {
        AllSelected = true;
        GoToQuizMenu();
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
        LerpTest.instance.UpdatePositionToMainMenu();
    }

    public void learnmode()
    {
    
        //MainMenu.SetActive(false);
        //   SelectShapeMenu.SetActive(true);
    
        Debug.Log("Learn clicked");
    }
    public void SwitchToAR()
    {
        DisplayCautionMessage(true);
    }

    public void DisplayCautionMessage(bool state)
    {
        CautionMessage.SetActive(state);
    }

    public void SwitchTo3D()
    {
        // feedback.text = "Switched to 3D " + name;
       
       // OnShapeSelectionMenuSwitch.Invoke();
    }

    public void ToggleButtonOn()
    {
        Debug.Log("toggleButton ON clicked");
    }
    public void ToggleButtonOff()
    {
        Debug.Log("toggleButton OFF clicked");
    }

    public void EnableInteraction()
    {
        InteractionDisplay.SetActive(true);
    }

        public void DisableInteraction()
    {
        InteractionDisplay.SetActive(false);
    }
        

}
