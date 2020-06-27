using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
            public QuestionTypes QuestionType;
            public string answer;
            public string[] otheroption;
        }


        public string shapeName;
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
 
    int correctAnswer = 0;
    string currentCorrectAnswer =  null;
    public GameObject Background;
    int currentQuestionNumber = 0;
    int currentAnswerNumber = 0;
    List<int> tempQuestions = new List<int>();

    //UI element References
    public Text ScoreBoard;
    public Text  QuestionText;
    public Text[] MCQOptions;
    public Button[] MCQButtons;
    public Text AnswerText;

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
       
        RandomizeQuestionOrder(questions[0]);
        StartQuiz("Cube");
        UpdateScore(); 
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



    QuestionBank SelectedQuestion = null;
    public void StartQuiz(string name)
    {
        UpdateSelectedShape();
        currentQuestionNumber = 0;

        switch (name)
        {
            case "Cube": SelectedQuestion = questions[0];
                break;
            case "Cone":
                SelectedQuestion = questions[1];
                break;
                break;
            case "Cylinder":
                SelectedQuestion = questions[2];
                break;
                break;
            case "Sphere":
                SelectedQuestion = questions[3];
                break;
                break;
            default: Debug.Log("UNKNOWN SHAPE");
                break;
        }


        getNextQuestion(SelectedQuestion);
    }

    public void UpdateScore()
    {
        ScoreBoard.text = correctAnswer.ToString() + " / " + tempOptions.Count.ToString();
    }

    public void RandomizeQuestionOrder(QuestionBank SelectedShapeQuestions)
    {
       

        tempQuestions.Clear();
        for (int i = 0; i < SelectedShapeQuestions.question.Length; i++)
        {
            tempQuestions.Add(i);
        }

        printQuestions(tempQuestions);

        var count = tempQuestions.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = tempQuestions[i];
            tempQuestions[i] = tempQuestions[r];
            tempQuestions[r] = tmp;
        }
        printQuestions(tempQuestions);
        //SelectedShapeQuestions
    }

    public void printQuestions(List<int> SelectedShapeQuestions)
    {
        for (int i = 0; i < SelectedShapeQuestions.Count; i++)
        {
            Debug.Log("Ques " + i + " : " + SelectedShapeQuestions[i]);
        }
    }


    public QuestionBank getSelectedQuestionBank()
    {

        return questions[0];
    }


    public void ShowCurrentQuestion()
    {

    }

    public void skipCurrentQuestion()
    {
         getNextQuestion(SelectedQuestion);
    }

    public void getNextQuestion(QuestionBank bank)
    {
        if (currentQuestionNumber < bank.question.Length)
        {
            string currentQuestion = bank.question[ tempQuestions[currentQuestionNumber]].question;
            QuestionBank.QuestionTypes type = bank.question[ tempQuestions[ currentQuestionNumber]].QuestionType;
            string answer = bank.question[tempQuestions[currentQuestionNumber]].answer;
          
            List<string> otheroptions = new List<string>();
            for (int i = 0; i < bank.question[tempQuestions[currentQuestionNumber]].otheroption.Length; i++){

                otheroptions.Add(bank.question[tempQuestions[currentQuestionNumber]].otheroption[i]);
            }


            switch (type)
            {
                case QuestionBank.QuestionTypes.MCQ :
                    EnableMCQButtons();
                    SetupQuestionForMCQ(currentQuestion, answer, otheroptions);
                    break;
                case QuestionBank.QuestionTypes.SOLVE: SetupQuestionForSolve();
                    break;
                case QuestionBank.QuestionTypes.TOUCH: SetupQuestionForTouch();
                    break;
                case QuestionBank.QuestionTypes.WHATIS: SetupQuestionForWhatIS();
                    break;
            }


        }

        currentQuestionNumber = currentQuestionNumber + 1;
    }

    public void SetupQuestionForMCQ(string question, string answer, List<string> options) {
        QuestionText.text = question;

        AnswerText.text = "";

        List<int> randomedButtonPos = ShuffleAnswerButton(MCQOptions);

        MCQOptions[randomedButtonPos[0]].text = answer;
        currentAnswerNumber = randomedButtonPos[0];

      currentCorrectAnswer = answer;

        for (int i = 1; i < MCQOptions.Length; i++)
        {
            MCQOptions[randomedButtonPos[i]].text = options[i-1];
        }
        
    }

    public void MCQAnswerSelected(Text ChildText)
    {
        Debug.Log(" Selected : " + ChildText.text);

        DisableMCQButtons();

        if(ChildText.text == currentCorrectAnswer)
        {
            Debug.Log("correct answer");
            correctAnswer = correctAnswer + 1;
            ShowAnswer(true);


            GameObject buttonObject = ChildText.gameObject.transform.parent.gameObject;
            Debug.Log(" parent button " + buttonObject.name);
            Button button = buttonObject.GetComponent<Button>();
            ColorBlock cb = button.colors;
            cb.disabledColor = Color.green;
            button.colors = cb;
        }
        else
        {

            
            Debug.Log("wrong answer answer");
            ShowAnswer(false);
            GameObject buttonObject = ChildText.gameObject.transform.parent.gameObject;
            Debug.Log(" parent button " + buttonObject.name);
            Button button = buttonObject.GetComponent<Button>();
            ColorBlock cb = button.colors;
            cb.disabledColor = Color.red;
            button.colors = cb;
        }

        UpdateScore();
    }

    public void DisableMCQButtons()
    {
        for(int i = 0; i < MCQButtons.Length; i++)
        {
            MCQButtons[i].interactable = false;
        }
    }


    public void EnableMCQButtons()
    {
        for (int i = 0; i < MCQButtons.Length; i++)
        {
            MCQButtons[i].interactable = true;
           
            ColorBlock cb = MCQButtons[i].colors;
            cb.disabledColor = Color.grey;
            MCQButtons[i].colors = cb;

        }
    }



    public void ShowAnswer(bool correct)
    {


        switch (correct)
        {
            case true:
                AnswerText.text ="You are right!";  

                break;

            case false:
                AnswerText.text = "Correct Answer :"+ currentCorrectAnswer;

                break;
        }

    }

    public void SetupQuestionForSolve()
    {

    }

    public void SetupQuestionForTouch()
    {

    }

    public void SetupQuestionForWhatIS()
    {

    }

    public void ShowCurrentAnswer()
    {

    }

    List<int> tempOptions = new List<int>();
    public List<int> ShuffleAnswerButton(Text[] options)
    {
        
        tempOptions.Clear();
        for (int i = 0; i < options.Length; i++)
        {
            tempOptions.Add(i);
        }

        printQuestions(tempOptions);

        var count = tempOptions.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = tempOptions[i];
            tempOptions[i] = tempOptions[r];
            tempOptions[r] = tmp;
        }
        printQuestions(tempOptions);
        return tempOptions;

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
