using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuizManager : MonoBehaviour
{


    [System.Serializable]
    public class ShapeParts
    {
        [System.Serializable]
        public class Part
        {
            public string partname;
            public GameObject[] parts;
        }
            public string ShapeName;
            public Part[] PartReferences;

    }
   

    [System.Serializable]
    public class QuestionBank
    {
        [System.Serializable]
        public enum QuestionTypes
        {
            MCQ,
            SOLVE,
            TOUCH,
            WHATIS

        }

        [System.Serializable]
        public class QuestionSet
        {
            public string question;
            
            public string answer;
            public string[] otheroption;
        }


        public string shapeName;
        public QuestionTypes QuestionType;
        public QuestionSet[] question;
    }


    public ShapeParts[] ShapeReferences;

    public QuestionBank[] questions;
    public GameObject Cube;
    public GameObject Cone;
    public GameObject Cylinder;
    public GameObject Sphere;
    public static QuizManager instance = null;
    public float rotationRate = 0.01f;
    GameObject SelectedShape;
    TextMesh Question;
    TextMesh AnswerCounter;
    int correctAnswer = 0;
    public GameObject Background;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Debug.Log("Touching at: " + touch.position);


            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hit))
                {


                    if (StateTracker.instance.getCurrentState() == StateTracker.State.QuizMode)
                    {
                      //  CloseAllInformation();
                    }

                }
                else
                {
                    //  text.text = "No HIT";
                    //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }

                //  text.text = "Touch began at " + touch.position;
                Debug.Log("Touch phase began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch phase Moved");
                //  text.text = "Touch phase moved";
                if (SelectedShape != null && (Background.activeSelf == true) && StateTracker.instance.getCurrentState() == StateTracker.State.QuizMode)
                {
                    SelectedShape.transform.Rotate(touch.deltaPosition.y * rotationRate, -touch.deltaPosition.x * rotationRate, 0, Space.World);
                }
                //  cube.transform.RotateAround(Vector3.down, touch.deltaPosition.x * rotationRate);
                //   cube.transform.RotateAround(Vector3.right, touch.deltaPosition.y * rotationRate);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //  text.text = "Touch phase ended";
                Debug.Log("Touch phase Ended");
            }
        }
    }

    public void StartQuiz(string name)
    {
        UpdateSelectedShape();


    }

    public void UpdateScore()
    {

    }

    public void UpdateQuestion()
    {

    }

    public void ShowCurrentAnswer()
    {

    }

    public void SetupShapeforQuestion()
    {

    }
    


    public void UpdateSelectedShape()
    {

        if (Cone.activeSelf)
        {
            SelectedShape = Cone;
        }
        else
       if (Cube.activeSelf)
        {
            SelectedShape = Cube;
        }
        else
       if (Cylinder.activeSelf)
        {
            SelectedShape = Cylinder;
        }
        else
           if (Sphere.activeSelf)
        {
            SelectedShape = Sphere;
        }
    }
}
