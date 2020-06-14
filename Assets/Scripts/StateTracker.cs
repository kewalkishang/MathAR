using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateTracker : MonoBehaviour
{
    public enum State { MainMenu, ShapeSelection, LearningMode, QuizMode };
    public static StateTracker instance = null;

    State currentState = State.MainMenu;
    public Text feedback;

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


    public void setCurrentState(State currstate)
    {
        currentState = currstate;
      //  feedback.text = "Setting State to " + currentState.ToString();

    }

    public State getCurrentState()
    {
      //  feedback.text = "Current state to " + currentState.ToString();
        return currentState;
    }
}
