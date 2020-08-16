using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using GoogleARCore.Examples.HelloAR;


public class QuizManager : MonoBehaviour
{
    /*
    [System.Serializable]
    public class InteractionEvent : UnityEvent { }

    public InteractionEvent OnMCQSOLVEWHATISQUESTION = new InteractionEvent();
    public InteractionEvent OnTOUCHQUESTION = new InteractionEvent();
    */

    [System.Serializable]
    public class ShapeParts
    {
        [System.Serializable]
        public class Part
        {
            public GameObject part;
            public TextMesh[] partText;
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

    string ShapeName = null;
    int correctAnswer = 0;
    string currentCorrectAnswer =  null;
    public GameObject Background;
    int currentQuestionNumber = 0;
    int currentAnswerNumber = 0;
    List<int> tempQuestions = new List<int>();

    //UI element References
    public Text ScoreBoard;
    public Text  QuestionText;
    public TextMesh[] MCQOptions;
    public GameObject[] MCQButtons;
    public TextMesh AnswerText;
    public GameObject MCQPanel;
    public GameObject TouchPanel;
    public Text feedbackText;
    public Text TouchText;

    public Sprite skip;
    public Sprite next;
    public Image nextQuestionButton;
    public GameObject QuestionButton;
    public GameObject alldone;
    public bool TouchFinished = false;

    QuestionBank.QuestionTypes currentQuestionType;


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
       
     //   RandomizeQuestionOrder(questions[0]);
       // StartQuiz("Cube");
        //UpdateScore(); 
    }

    // Update is called once per frame

    public float speed = 10f;
    void Update()
    {

     /*   if (Input.GetMouseButtonDown(0))
        {
            //Input.GetAxis
            // SelectedShape.transform.Rotate(touch.deltaPosition.y * rotationRate, -touch.deltaPosition.x * rotationRate, 0, Space.World);
            //   SelectedShape.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed * Time.deltaTime, -Input.GetAxis("Mouse X") * speed * Time.deltaTime, 0), Space.World);

            if (!StateTracker.instance.getARState())
            {
                float XaxisRotation = Input.GetAxis("Mouse X") * speed;
                float YaxisRotation = Input.GetAxis("Mouse Y") * speed;
                // select the axis by which you want to rotate the GameObject
                SelectedShape.transform.RotateAround(Vector3.down, XaxisRotation);
                SelectedShape.transform.RotateAround(Vector3.right, YaxisRotation);
            }


            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("You hit part " + hit.transform.name);
                StateTracker.State state = StateTracker.instance.getCurrentState();
                if (state == StateTracker.State.QuizMode)
                {
                 //   Debug.Log("You hit part " + hit.transform.name); // ensure you picked right object
                    GameObject hitpart = hit.transform.gameObject;

                    feedbackText.text = "HIT " + hit.transform.gameObject.name;
                    if (currentQuestionType == QuestionBank.QuestionTypes.TOUCH  )
                    {
                        if (hit.transform.gameObject.tag == "part")
                        {
                            hitpart.transform.GetChild(0).gameObject.SetActive(true);
                           // Debug.Log("toouch");
                            if (TouchTargets.Contains(hitpart))
                            {
                                TouchTargets.Remove(hitpart);
                            }

                            if (TouchTargets.Count == 0)
                            {
                                TouchText.text = "Good JOB!";
                                correctAnswer = correctAnswer + 1;
                                UpdateScore();
                            }
                            else
                            {
                                UpdateTouchScore();
                            }
                        }

                        if(hit.transform.gameObject.tag == "shape"){
                            string shapen = hit.transform.name;
                            Debug.Log("Selected shape" + shapen);
                            DisableShapeTouch();
                            if (shapen == currentCorrectAnswer)
                            {
                                Debug.Log("Correct shape selected");
                                TouchText.text = "CORRECT ANSWER!";
                                correctAnswer = correctAnswer + 1;
                                UpdateScore();
                            }
                            else
                            {
                               
                                TouchText.text = "WRONG ANSWER!";
                            
                                UpdateScore();
                                Debug.Log("Wrong shape selected");
                            }
                        }
                    }

                    if ( (currentQuestionType == QuestionBank.QuestionTypes.MCQ || currentQuestionType == QuestionBank.QuestionTypes.SOLVE || currentQuestionType == QuestionBank.QuestionTypes.WHATIS ) && hit.transform.gameObject.tag == "options")
                    {
                        Debug.Log("Clicked option");
                      
                        GameObject textobject = hitpart.transform.GetChild(0).gameObject;
                       
                        MCQAnswerSelected(textobject.GetComponent<TextMesh>());
                    }


                    //DisableOtherThanSelected(hit.transform.name);
                    //   UpdateShape(SelectedShape.name);
                    //  DisableAR();
                }
            }
        } */
        
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
                    GameObject hitpart = hit.transform.gameObject;

                    feedbackText.text = "HIT " + hit.transform.gameObject.name;
                    if (currentQuestionType == QuestionBank.QuestionTypes.TOUCH)
                    {
                        if (hit.transform.gameObject.tag == "part" && !TouchFinished)
                        {
                            hitpart.transform.GetChild(0).gameObject.SetActive(true);
                            // Debug.Log("toouch");
                            if (TouchTargets.Contains(hitpart))
                            {
                                TouchTargets.Remove(hitpart);
                            }

                            if (TouchTargets.Count == 0)
                            {
                                TouchText.text = "Good JOB!";
                                correctAnswer = correctAnswer + 1;
                                TouchFinished = true;
                                UpdateScore();
                            }
                            else
                            {
                                UpdateTouchScore();
                            }
                        }

                        if (hit.transform.gameObject.tag == "shape")
                        {
                            string shapen = hit.transform.name;
                            Debug.Log("Selected shape" + shapen);
                            DisableShapeTouch();
                            if (shapen == currentCorrectAnswer)
                            {
                                Debug.Log("Correct shape selected");
                                TouchText.text = "CORRECT ANSWER!";
                                correctAnswer = correctAnswer + 1;
                                UpdateScore();
                            }
                            else
                            {

                                TouchText.text = "WRONG ANSWER!";

                                UpdateScore();
                                Debug.Log("Wrong shape selected");
                            }
                        }
                    }

                    if ((currentQuestionType == QuestionBank.QuestionTypes.MCQ || currentQuestionType == QuestionBank.QuestionTypes.SOLVE || currentQuestionType == QuestionBank.QuestionTypes.WHATIS) && hit.transform.gameObject.tag == "options")
                    {
                        Debug.Log("Clicked option");

                        GameObject textobject = hitpart.transform.GetChild(0).gameObject;

                        MCQAnswerSelected(textobject.GetComponent<TextMesh>());
                    }


                    //DisableOtherThanSelected(hit.transform.name);
                    //   UpdateShape(SelectedShape.name);
                    //  DisableAR();
                

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
                 //&& StateTracker.instance.getCurrentState() == StateTracker.State.QuizMode
                 if (SelectedShape != null && StateTracker.instance.getCurrentState() == StateTracker.State.QuizMode && !HelloARController.instance.dragging)
                 {
                    Camera camera = Camera.main;

                    Vector3 right = Vector3.Cross(camera.transform.up, SelectedShape.transform.position - camera.transform.position);

                    Vector3 up = Vector3.Cross(SelectedShape.transform.position - camera.transform.position, right);

                    SelectedShape.transform.rotation = Quaternion.AngleAxis(-touch.deltaPosition.x * rotationRate, up) * SelectedShape.transform.rotation;

                    SelectedShape.transform.rotation = Quaternion.AngleAxis(touch.deltaPosition.y * rotationRate, right) * SelectedShape.transform.rotation;
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
        Debug.Log("Starting quiz" + name);
            
       // UpdateSelectedShape();
        currentQuestionNumber = 0;

        correctAnswer = 0;
        alldone.SetActive(false);

        switch (name)
        {
            case "Cube":
                ShapeName = "Cube";
                SelectedQuestion = questions[0];
                break;
            case "Cone":
                ShapeName = "Cone";
                SelectedQuestion = questions[1];
                break;
             
            case "Cylinder":
                ShapeName = "Cylinder";
                SelectedQuestion = questions[2];
                break;
              
            case "Sphere":
                ShapeName = "Sphere";
                SelectedQuestion = questions[3];
                break;

            case "All":
                ShapeName = "All";
                SelectedQuestion = questions[4];
                break;
              
            default: Debug.Log("UNKNOWN SHAPE");
                break;
        }

        RandomizeQuestionOrder(SelectedQuestion);
        UpdateScore();
       
        getNextQuestion(SelectedQuestion, name);
    }

    public void SwitchtoTouchPosition()
    {
        LerpTest.instance.UpdateSelectedShapePosition();
       // OnTOUCHQUESTION.Invoke();
    }

    public void SwitchToMCQ()
    {
        LerpTest.instance.UpdateSelectedToDisplayPosition();
        //OnMCQSOLVEWHATISQUESTION.Invoke();
    }

    public void UpdateScore()
    {
        ScoreBoard.text = correctAnswer.ToString() + " / " + SelectedQuestion.question.Length.ToString();

      
            nextQuestionButton.sprite = next;
        
    }

    int TouchCount = 0;
     
    public void UpdateTouchScore()
    {
        int touched = TouchCount - TouchTargets.Count;
        TouchText.text =  touched.ToString() + "/" + TouchCount.ToString();
    }

    public void RandomizeQuestionOrder(QuestionBank SelectedShapeQuestions)
    {
       

        tempQuestions.Clear();
        for (int i = 0; i < SelectedShapeQuestions.question.Length; i++)
        {
            tempQuestions.Add(i);
        }

       // printQuestions(tempQuestions);

        var count = tempQuestions.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = tempQuestions[i];
            tempQuestions[i] = tempQuestions[r];
            tempQuestions[r] = tmp;
        }
       // printQuestions(tempQuestions);
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
         getNextQuestion(SelectedQuestion, ShapeName);
    }

    public void getNextQuestion(QuestionBank bank, string name)
    {
        ResetAllShapes();
        EnableShapeTouch();
        nextQuestionButton.sprite = skip;
        ShapePartReferences.instance.MakeAllShapesOpaque();

        for (int i = 0; i < MCQOptions.Length; i++)
        {
            MCQOptions[i].color = Color.black;
        }


        if (currentQuestionNumber < bank.question.Length)
        {
            string currentQuestion = bank.question[tempQuestions[currentQuestionNumber]].question;
            QuestionBank.QuestionTypes type = bank.question[tempQuestions[currentQuestionNumber]].QuestionType;
            string answer = bank.question[tempQuestions[currentQuestionNumber]].answer;
            currentQuestionType = type;
            Debug.Log("LENGTH : " + bank.question.Length);
            Debug.Log(" QUESTION " + currentQuestion + " : ANSWER" + answer);

            List<string> otheroptions = new List<string>();
            for (int i = 0; i < bank.question[tempQuestions[currentQuestionNumber]].otheroption.Length; i++) {

                otheroptions.Add(bank.question[tempQuestions[currentQuestionNumber]].otheroption[i]);
            }
            QuestionButton.SetActive(true);
            /*   if (currentQuestionNumber == bank.question.Length - 1)
                   QuestionButton.SetActive(false);
               else
                   QuestionButton.SetActive(true);
                   */
            switch (type)
            {
                case QuestionBank.QuestionTypes.MCQ:
                    SwitchToMCQ();
                    MCQPanel.SetActive(true);
                    TouchPanel.SetActive(false);
                    EnableMCQButtons();
                    SetupQuestionForMCQ(currentQuestion, answer, otheroptions);
                    break;
                case QuestionBank.QuestionTypes.SOLVE:
                    SwitchToMCQ();
                    MCQPanel.SetActive(true);
                    TouchPanel.SetActive(false);
                    EnableMCQButtons();
                    SetupQuestionForSolve(currentQuestion, name);
                    break;
                case QuestionBank.QuestionTypes.TOUCH:
                    if (name != "All")
                    {
                        SwitchtoTouchPosition();
                    }
                    TouchFinished = false;
                    TouchPanel.SetActive(true);
                    MCQPanel.SetActive(false);
                    TouchText.text = "";
                    SetupQuestionForTouch(currentQuestion, name, answer);
                    break;
                case QuestionBank.QuestionTypes.WHATIS:
                    SwitchToMCQ();
                    MCQPanel.SetActive(true);
                    TouchPanel.SetActive(false);
                    EnableMCQButtons();
                    SetupQuestionForWhatIS(currentQuestion, name);
                    break;
            }


        }
        else
        {
            SetStatetoDone();
        }

        currentQuestionNumber = currentQuestionNumber + 1;
    }



    public void SetStatetoDone()
    {
        QuestionText.text = "";
        TouchText.text = "";
       QuestionButton.SetActive(false);
        alldone.SetActive(true);
        SwitchtoTouchPosition();
        MCQPanel.SetActive(false);
    }


    public void SetupQuestionForMCQ(string question, string answer, List<string> options) {
        int qno = currentQuestionNumber + 1;
        QuestionText.text =  qno.ToString() + ". "  +  question;

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

    public void MCQAnswerSelected(TextMesh ChildText)
    {
        Debug.Log(" Selected : " + ChildText.text);

        DisableMCQButtons();

        for (int i = 0; i < MCQOptions.Length; i++)
        {
            MCQOptions[i].color = Color.grey;
        }


        if (ChildText.text == currentCorrectAnswer)
        {
            Debug.Log("correct answer");
            correctAnswer = correctAnswer + 1;
            ShowAnswer(true);

           
            ChildText.color = Color.green;
          /*  GameObject buttonObject = ChildText.gameObject.transform.parent.gameObject;
            Debug.Log(" parent button " + buttonObject.name);
            Button button = buttonObject.GetComponent<Button>();
            ColorBlock cb = button.colors;
            cb.disabledColor = Color.green;
            button.colors = cb; */
        }
        else
        {

            
            Debug.Log("wrong answer answer");
            ShowAnswer(false);

            ChildText.color = Color.red;
            /*  GameObject buttonObject = ChildText.gameObject.transform.parent.gameObject;
              Debug.Log(" parent button " + buttonObject.name);
              Button button = buttonObject.GetComponent<Button>();
              ColorBlock cb = button.colors;
              cb.disabledColor = Color.red;
              button.colors = cb; */
        }

        UpdateScore();
    }

    public void DisableMCQButtons()
    {
        for(int i = 0; i < MCQButtons.Length; i++)
        {
            MCQButtons[i].GetComponent<Collider>().enabled = false;
        }
    }



    public void EnableMCQButtons()
    {
        for (int i = 0; i < MCQButtons.Length; i++)
        {
            MCQButtons[i].GetComponent<Collider>().enabled = true;
           // MCQButtons[i].interactable = true;
           
            //ColorBlock cb = MCQButtons[i].colors;
           // cb.disabledColor = Color.grey;
            ///MCQButtons[i].colors = cb;

        }
    }

    public void EnableShapeTouch()
    {
        Cube.GetComponent<Collider>().enabled = true;
        Cone.GetComponent<Collider>().enabled = true;
        Cylinder.GetComponent<Collider>().enabled = true;
           Sphere.GetComponent<Collider>().enabled = true;
    }

    public void DisableShapeTouch()
    {
        Cube.GetComponent<Collider>().enabled = false;
        Cone.GetComponent<Collider>().enabled = false;
        Cylinder.GetComponent<Collider>().enabled = false;
        Sphere.GetComponent<Collider>().enabled = false;
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

    public void SetupQuestionForSolve(string question, string ShapeName)
    {

        AnswerText.text = "";

        List<int> randomedButtonPos = ShuffleAnswerButton(MCQOptions);


        //Area
        float CubeArea;
        float CylinderArea;
        float SphereArea;
        float ConeArea;

        //Volume
        float Cubevol;
        float CylinderVol;
        float SphereVol;
        float ConeVol;
        float answer = 0;
        List<float> options = new List<float>();

        switch (question)
        {
            case "area":
                int qno = currentQuestionNumber + 1;
                QuestionText.text = qno.ToString() + ". Calculate the area of the following "+ ShapeName;

                switch (ShapeName)
                {
                    case "Cube":
                        int side = GetRandomValueBetweeen(1, 5);

                        SetupCubeforSolveANDWhatIS(side);

                        Debug.Log("SIDE " + side.ToString());
                        CubeArea = 6 * side * side;
                        Debug.Log("area"  + CubeArea.ToString());
                        CylinderArea = 2 * Mathf.PI * side *  (side + side) * 2; //2πrh+2πr2
                        SphereArea = 4  * Mathf.PI * side * side * 3; //4πr2
                        ConeArea = Mathf.PI * side * (side + Mathf.Sqrt ( side * side + side * side))  ; //πr(r+h2+r2)

                        answer = CubeArea;

                        options.Add(CylinderArea);
                        options.Add(SphereArea);
                        options.Add(ConeArea);

                        

                        break;
                    case "Cone":
                         int Conerad = GetRandomValueBetweeen(1, 5);
                         int Conehei = GetRandomValueBetweeen(1, 5);
                        //SetupCubeforSolveANDWhatIS(side);
                        SetupConeforSolveANDWhatIS(Conerad, Conehei);
                        Debug.Log("RADIUS " + Conerad.ToString() + " : hEIGHT " + Conehei ) ;
                        CubeArea = 6 * Conerad * Conehei * 3; 
                       
                        CylinderArea = 2 * Mathf.PI * Conerad * (Conehei + Conehei) * 2; //2πrh+2πr2
                        SphereArea = 4 * Mathf.PI * Conerad * Conehei * 3; //4πr2
                        ConeArea = Mathf.PI * Conerad * (Conerad + Mathf.Sqrt(Conehei * Conehei + Conerad * Conerad)); //πr(r+h2+r2)

                        answer = ConeArea;
                        Debug.Log("area" + answer);
                        options.Add(CylinderArea);
                        options.Add(SphereArea);
                        options.Add(CubeArea);

                        break;
                       
                    case "Cylinder":
                        int Cylinderrad = GetRandomValueBetweeen(1, 5);
                        int Cylinderhei = GetRandomValueBetweeen(1, 5);
                        SetupCylinderforSolveANDWhatIS(Cylinderrad, Cylinderhei);

                        Debug.Log("RADIUS " + Cylinderrad.ToString() + " : hEIGHT " + Cylinderhei.ToString() );
                        CubeArea = 6 * Cylinderrad * Cylinderhei * 1.7f;
                       
                        CylinderArea = 2 * Mathf.PI * Cylinderrad * (Cylinderhei + Cylinderrad); //2πrh+2πr2
                        SphereArea = 4 * Mathf.PI * Cylinderrad * Cylinderhei * 2.5f; //4πr2
                        ConeArea = Mathf.PI * Cylinderrad * (Cylinderhei + Mathf.Sqrt(Cylinderrad * Cylinderrad + Cylinderhei * Cylinderhei)) * 3; //πr(r+h2+r2)

                        answer = CylinderArea;
                        Debug.Log("area" + answer.ToString());
                        options.Add(CubeArea);
                        options.Add(SphereArea);
                        options.Add(ConeArea);


                        break;
                        
                    case "Sphere":
                        int radius = GetRandomValueBetweeen(1, 5);

                        SetupSphereforSolveANDWhatIS(radius);
                        Debug.Log("Radius " + radius.ToString());
                        CubeArea = 6 * radius * radius * 1.2f;
                      
                        CylinderArea = 2 * Mathf.PI * radius * (radius + radius) * 1.5f; //2πrh+2πr2
                        SphereArea = 4 * Mathf.PI * radius * radius; //4πr2
                        Debug.Log("area" + SphereArea.ToString());
                        ConeArea = Mathf.PI * radius * (radius + Mathf.Sqrt(radius * radius + radius * radius)) * 2; //πr(r+h2+r2)

                        answer = SphereArea;

                        options.Add(CylinderArea);
                        options.Add(CubeArea);
                        options.Add(ConeArea);

                        break;
                      
                    default:
                        Debug.Log("UNKNOWN SHAPE");
                        break;
                }


                break;
            case "volume":
                 qno = currentQuestionNumber + 1;
                QuestionText.text = qno.ToString() + ". Calculate the volume of the following " + ShapeName;

                switch (ShapeName)
                {
                    case "Cube":
                        ShapeName = "Cube";
                        int side = GetRandomValueBetweeen(1, 5);

                        SetupCubeforSolveANDWhatIS(side);

                        Debug.Log("SIDE " + side.ToString());
                        Cubevol = side * side * side;
                        Debug.Log("Volume " + Cubevol.ToString());
                        CylinderVol = Mathf.PI * side * side * side * 2.5f;
                        SphereVol = (4 / 3) * Mathf.PI * side * side * side * 5.2f;
                        ConeVol = Mathf.PI * side * side * (side / 3) * 3.3f;

                        answer = Cubevol;

                        options.Add(CylinderVol);
                        options.Add(SphereVol);
                        options.Add(ConeVol);

                        break;
                    case "Cone":
                        ShapeName = "Cone";
                        int Conerad = GetRandomValueBetweeen(1, 5);
                        int Conehei = GetRandomValueBetweeen(1, 5);
                        //SetupCubeforSolveANDWhatIS(side);
                        SetupConeforSolveANDWhatIS(Conerad, Conehei);
                        Debug.Log("RADIUS " + Conerad.ToString() + " : hEIGHT " + Conehei);
                        Cubevol = Conerad * Conerad * Conerad * 4 ;
                        
                        CylinderVol = Mathf.PI * Conerad * Conehei * Conehei;
                        SphereVol = (4 / 3) * Mathf.PI * Conerad * Conerad * Conehei * 2.5f;
                        ConeVol = Mathf.PI * Conerad * Conerad * (Conehei / 3);

                        answer = ConeVol;
                        Debug.Log("Volume " + answer.ToString());
                        options.Add(CylinderVol);
                        options.Add(SphereVol);
                        options.Add(Cubevol);



                        break;
                       
                    case "Cylinder":
                        ShapeName = "Cylinder";

                        int Cylinderrad = GetRandomValueBetweeen(1, 5);
                        int Cylinderhei = GetRandomValueBetweeen(1, 5);
                        SetupCylinderforSolveANDWhatIS(Cylinderrad, Cylinderhei);


                        Debug.Log("RADIUS " + Cylinderrad.ToString() + " : hEIGHT " + Cylinderhei.ToString());
                        Cubevol = Cylinderrad * Cylinderrad * Cylinderrad * 6.5f;

                        CylinderVol = Mathf.PI * Cylinderrad * Cylinderrad * Cylinderhei;
                        SphereVol = (4 / 3) * Mathf.PI * Cylinderrad * Cylinderrad * Cylinderhei * 2.2f;
                        ConeVol = Mathf.PI * Cylinderrad * Cylinderhei * (Cylinderrad / 3);

                        answer = CylinderVol;
                        Debug.Log("Volume " + answer.ToString());
                        options.Add(Cubevol);
                        options.Add(SphereVol);
                        options.Add(ConeVol);


                        break;
                    
                    case "Sphere":
                        ShapeName = "Sphere";
                   

                 

                        int radius = GetRandomValueBetweeen(1, 5);

                        SetupSphereforSolveANDWhatIS(radius);

                        Debug.Log("radius " + radius.ToString());
                        Cubevol = radius * radius * radius * 2.4f;

                        CylinderVol = Mathf.PI * radius * radius * radius * 1.7f;
                        SphereVol = (4 / 3) * Mathf.PI * radius * radius * radius ;
                        ConeVol = Mathf.PI * radius * radius * (radius / 3) * 2.15f;

                        
                        answer = SphereVol;
                        Debug.Log("Volume " + answer.ToString());
                        options.Add(CylinderVol);
                        options.Add(Cubevol);
                        options.Add(ConeVol);
                        break;
                    default:
                        Debug.Log("UNKNOWN SHAPE");
                        break;
                }

                break;
        }

        //Populate answers
        MCQOptions[randomedButtonPos[0]].text = answer.ToString("F2");
        currentAnswerNumber = randomedButtonPos[0];
        currentCorrectAnswer = answer.ToString("F2");

        for (int i = 1; i < MCQOptions.Length; i++)
        {
            MCQOptions[randomedButtonPos[i]].text = options[i - 1].ToString("F2");
        }


    }


    public int GetRandomValueBetweeen(int min, int max)
    {
      

        return Random.Range(min, max);

    }

 

    public void SetupQuestionForWhatIS(string question, string ShapeName)
    {
        AnswerText.text = "";

        List<int> randomedButtonPos = ShuffleAnswerButton(MCQOptions);

        switch (ShapeName)
        {
            case "Cube":
                int side = GetRandomValueBetweeen(1, 5);

                SetupCubeforSolveANDWhatIS(side);

                Debug.Log("SIDE " + side.ToString());

                switch (question)
                {
                    case "side":
                        int qno = currentQuestionNumber + 1;
              
                        QuestionText.text = qno.ToString() + ". What is the value of the side from the follow cube";
                        currentCorrectAnswer = side.ToString();
                        //Populate answers
                        MCQOptions[randomedButtonPos[0]].text = side.ToString();
                        currentAnswerNumber = randomedButtonPos[0];

                        int tempVal = GetRandomValueBetweeen(1, 5);

                        List<int> options = new List<int>();
                        if (tempVal % 2 == 0)
                        {
                            options.Add(side + 1);
                            options.Add(side + 2);
                            options.Add(side - 1);

                        }
                        else
                        {
                            options.Add(side - 1);
                            options.Add(side - 2);
                            options.Add(side + 1);
                        }
                        for (int i = 1; i < MCQOptions.Length; i++)
                        {
                           
                            MCQOptions[randomedButtonPos[i]].text = options[i-1].ToString();
                        }



                        break;
                }
             



                break;
            case "Cone":

                int Conerad = GetRandomValueBetweeen(1, 5);
                int Conehei = GetRandomValueBetweeen(1, 5);
                //SetupCubeforSolveANDWhatIS(side);
                SetupConeforSolveANDWhatIS(Conerad, Conehei);
                Debug.Log("RADIUS " + Conerad.ToString() + " : hEIGHT " + Conehei);
               
                switch (question)
                {
                    case "radius":
                        int qno = currentQuestionNumber + 1;
                        QuestionText.text = qno.ToString() + ". What is the value of the radius from the follow Cone";
                        currentCorrectAnswer = Conerad.ToString();
                        //Populate answers
                        MCQOptions[randomedButtonPos[0]].text = Conerad.ToString();
                        currentAnswerNumber = randomedButtonPos[0];

                        int tempVal = GetRandomValueBetweeen(1, 5);

                        List<int> options = new List<int>();
                        if (tempVal % 2 == 0)
                        {
                            options.Add(Conerad + 1);
                            options.Add(Conerad + 2);
                            options.Add(Conerad - 1);

                        }
                        else
                        {
                            options.Add(Conerad - 1);
                            options.Add(Conerad - 2);
                            options.Add(Conerad + 1);
                        }
                        for (int i = 1; i < MCQOptions.Length; i++)
                        {

                            MCQOptions[randomedButtonPos[i]].text = options[i - 1].ToString();
                        }


                        break;
                    case "height":
                        qno = currentQuestionNumber + 1;
                        QuestionText.text = qno.ToString() + ". What is the value of the height from the follow Cone";
                        currentCorrectAnswer = Conehei.ToString();
                        //Populate answers
                        MCQOptions[randomedButtonPos[0]].text = Conehei.ToString();
                        currentAnswerNumber = randomedButtonPos[0];

                        tempVal = GetRandomValueBetweeen(1, 5);

                        options = new List<int>();
                        if (tempVal % 2 == 0)
                        {
                            options.Add(Conehei + 1);
                            options.Add(Conehei + 2);
                            options.Add(Conehei - 1);

                        }
                        else
                        {
                            options.Add(Conehei - 1);
                            options.Add(Conehei - 2);
                            options.Add(Conehei + 1);
                        }
                        for (int i = 1; i < MCQOptions.Length; i++)
                        {

                            MCQOptions[randomedButtonPos[i]].text = options[i - 1].ToString();
                        }
                        break;
                }



                        break;

            case "Cylinder":
                int Cylinderrad = GetRandomValueBetweeen(1, 5);
                int Cylinderhei = GetRandomValueBetweeen(1, 5);
                SetupCylinderforSolveANDWhatIS(Cylinderrad, Cylinderhei);


                Debug.Log("RADIUS " + Cylinderrad.ToString() + " : hEIGHT " + Cylinderhei.ToString());

                switch (question)
                {
                    case "radius":
                        int qno = currentQuestionNumber + 1;
                        QuestionText.text = qno.ToString() + ". What is the value of the radius from the follow Cylinder.";
                        currentCorrectAnswer = Cylinderrad.ToString();
                        //Populate answers
                        MCQOptions[randomedButtonPos[0]].text = Cylinderrad.ToString();
                        currentAnswerNumber = randomedButtonPos[0];

                        int tempVal = GetRandomValueBetweeen(1, 5);

                        List<int> options = new List<int>();
                        if (tempVal % 2 == 0)
                        {
                            options.Add(Cylinderrad + 1);
                            options.Add(Cylinderrad + 2);
                            options.Add(Cylinderrad - 1);

                        }
                        else
                        {
                            options.Add(Cylinderrad - 1);
                            options.Add(Cylinderrad - 2);
                            options.Add(Cylinderrad + 1);
                        }
                        for (int i = 1; i < MCQOptions.Length; i++)
                        {

                            MCQOptions[randomedButtonPos[i]].text = options[i - 1].ToString();
                        }


                        break;
                    case "height":
                        qno = currentQuestionNumber + 1;
                        QuestionText.text = qno.ToString() + ". What is the value of the height from the follow Cone";
                        currentCorrectAnswer = Cylinderhei.ToString();
                        //Populate answers
                        MCQOptions[randomedButtonPos[0]].text = Cylinderhei.ToString();
                        currentAnswerNumber = randomedButtonPos[0];

                        tempVal = GetRandomValueBetweeen(1, 5);

                        options = new List<int>();
                        if (tempVal % 2 == 0)
                        {
                            options.Add(Cylinderhei + 1);
                            options.Add(Cylinderhei + 2);
                            options.Add(Cylinderhei - 1);

                        }
                        else
                        {
                            options.Add(Cylinderhei - 1);
                            options.Add(Cylinderhei - 2);
                            options.Add(Cylinderhei + 1);
                        }
                        for (int i = 1; i < MCQOptions.Length; i++)
                        {

                            MCQOptions[randomedButtonPos[i]].text = options[i - 1].ToString();
                        }
                        break;
                }

                break;

            case "Sphere":

                int radius = GetRandomValueBetweeen(1, 5);

                SetupSphereforSolveANDWhatIS(radius);

                Debug.Log("SIDE " + radius.ToString());

                switch (question)
                {
                    case "radius":
                        int qno = currentQuestionNumber + 1;

                        QuestionText.text = qno.ToString() + ". What is the value of the radius from the follow sphere";
                        currentCorrectAnswer = radius.ToString();
                        //Populate answers
                        MCQOptions[randomedButtonPos[0]].text = radius.ToString();
                        currentAnswerNumber = randomedButtonPos[0];

                        int tempVal = GetRandomValueBetweeen(1, 5);

                        List<int> options = new List<int>();
                        if (tempVal % 2 == 0)
                        {
                            options.Add(radius + 1);
                            options.Add(radius + 2);
                            options.Add(radius - 1);

                        }
                        else
                        {
                            options.Add(radius - 1);
                            options.Add(radius - 2);
                            options.Add(radius + 1);
                        }
                        for (int i = 1; i < MCQOptions.Length; i++)
                        {

                            MCQOptions[randomedButtonPos[i]].text = options[i - 1].ToString();
                        }



                        break;
                }


                break;

            default:
                Debug.Log("UNKNOWN SHAPE");
                break;
        }
    }


    public void SetupCubeforSolveANDWhatIS(int side)
    {
        Debug.Log("Cube Setup" + ShapeReferences[0].ShapeName);
        //Enabling the edges
        ShapeReferences[0].PartReferences[0].part.SetActive(true);
        int EdgesCount = ShapeReferences[0].PartReferences[0].partText.Length;

        for(int i= 0; i < ShapeReferences[0].PartReferences[0].part.transform.childCount; i++)
        {
            ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
            for (int l = 0; l < ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).transform.GetChild(0).GetChildCount(); l++)
            {
                ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

            }
        }


        for (int i = 0; i < EdgesCount; i++)
        {
            ShapeReferences[0].PartReferences[0].partText[i].text = side.ToString();

        }

    }

    public void SetupCylinderforSolveANDWhatIS(int radius, int height)
    {
        ShapePartReferences.instance.MakeAllShapeTransparent();
        Debug.Log("Cylinder Setup" + ShapeReferences[1].ShapeName);
        ShapeReferences[2].PartReferences[0].part.SetActive(true); //radius
        ShapeReferences[2].PartReferences[1].part.SetActive(true); //height
        ShapeReferences[2].PartReferences[0].part.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        ShapeReferences[2].PartReferences[0].part.transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        ShapeReferences[2].PartReferences[0].partText[0].text = radius.ToString();

        ShapeReferences[2].PartReferences[1].part.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

        ShapeReferences[2].PartReferences[1].part.transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        ShapeReferences[2].PartReferences[1].partText[0].text = height.ToString();
    }

    public void SetupSphereforSolveANDWhatIS(int radius)
    {
        ShapePartReferences.instance.MakeAllShapeTransparent();
        Debug.Log("Sphere Setup" + ShapeReferences[3].ShapeName);
        ShapeReferences[3].PartReferences[0].part.SetActive(true);
        ShapeReferences[3].PartReferences[0].part.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        ShapeReferences[3].PartReferences[0].part.transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        ShapeReferences[3].PartReferences[0].partText[0].text = radius.ToString();

    }
    public void SetupConeforSolveANDWhatIS(int radius, int height)
    {
        ShapePartReferences.instance.MakeAllShapeTransparent();
        Debug.Log("Cone Setup" + ShapeReferences[1].ShapeName);
        ShapeReferences[1].PartReferences[0].part.SetActive(true); //height
        ShapeReferences[1].PartReferences[1].part.SetActive(true); //radius
        ShapeReferences[1].PartReferences[0].part.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        ShapeReferences[1].PartReferences[0].part.transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        ShapeReferences[1].PartReferences[0].partText[0].text = height.ToString();

        ShapeReferences[1].PartReferences[1].part.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        ShapeReferences[1].PartReferences[1].part.transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        ShapeReferences[1].PartReferences[1].partText[0].text = radius.ToString();
    }

    List<GameObject> TouchTargets = new List<GameObject>();

    public void SetupQuestionForTouch(string question, string ShapeName , string answer)
    {
        // AnswerText.text = "";
        Debug.Log("in touch question setup " + question + " name " + ShapeName);
        TouchTargets.Clear();
        DisableShapeTouch();

        switch (ShapeName)
        {
            case "Cube":


                int partCount = 0;
                
               
                Debug.Log("CUBE");

                switch (question)
                {
                    case "edge":

                        ShapeReferences[0].PartReferences[0].part.SetActive(true);
                        partCount = ShapeReferences[0].PartReferences[0].part.transform.childCount;

                        for(int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).gameObject);
                        }

                        for (int i = 0; i < ShapeReferences[0].PartReferences[0].part.transform.childCount; i++)
                        {
                           
                            for (int l = 0; l < ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).transform.GetChild(0).GetChildCount(); l++)
                            {
                                ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                            }
                        }


                        break;
                    case "vertex":
                        ShapeReferences[0].PartReferences[1].part.SetActive(true);
                        partCount = ShapeReferences[0].PartReferences[1].part.transform.childCount;
                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[0].PartReferences[1].part.transform.GetChild(i).gameObject);
                        }
                        break;
                    case "face":
                        ShapeReferences[0].PartReferences[2].part.SetActive(true);
                        partCount = ShapeReferences[0].PartReferences[2].part.transform.childCount;
                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[0].PartReferences[2].part.transform.GetChild(i).gameObject);
                        }
                        break;
                    
                }
                int qno = currentQuestionNumber + 1;
                QuestionText.text = qno.ToString() + ". Touch the " + partCount + "  "+question + " of the  cube.";
                TouchCount = TouchTargets.Count;
                UpdateTouchScore();

                break;
            case "Cone":
                partCount = 0;
                
               
                Debug.Log("CONE");

                switch (question)
                {
                    case "edge":
                        ShapeReferences[1].PartReferences[2].part.SetActive(true);
                        partCount = ShapeReferences[1].PartReferences[2].part.transform.childCount;

                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[1].PartReferences[2].part.transform.GetChild(i).gameObject);
                        }

                        break;
                    case "face":
                        ShapeReferences[1].PartReferences[3].part.SetActive(true);
                        partCount = ShapeReferences[1].PartReferences[3].part.transform.childCount;

                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[1].PartReferences[3].part.transform.GetChild(i).gameObject);
                        }

                        break;
                    case "vertex":
                        ShapeReferences[1].PartReferences[4].part.SetActive(true);
                        partCount = ShapeReferences[1].PartReferences[4].part.transform.childCount;

                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[1].PartReferences[4].part.transform.GetChild(i).gameObject);
                        }

                        break;
                }

                qno = currentQuestionNumber + 1;
                QuestionText.text = qno.ToString() + ". Touch the " + partCount + "  " + question + " of the Cone.";
                TouchCount = TouchTargets.Count;
                UpdateTouchScore();
                break;

            case "Cylinder":
                partCount = 0;
                
               
                Debug.Log("CYLINDER");

                switch (question)
                {
                    case "edge":
                        ShapeReferences[2].PartReferences[3].part.SetActive(true);
                        partCount = ShapeReferences[2].PartReferences[3].part.transform.childCount;

                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[2].PartReferences[3].part.transform.GetChild(i).gameObject);
                        }

                        break;
                    case "face":
                        ShapeReferences[2].PartReferences[2].part.SetActive(true);
                        partCount = ShapeReferences[2].PartReferences[2].part.transform.childCount;

                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[2].PartReferences[2].part.transform.GetChild(i).gameObject);
                        }

                        break;
                    
                }

                qno = currentQuestionNumber + 1;
                QuestionText.text = qno.ToString() + ". Touch the " + partCount + "  " + question + " of the Cylinder.";
                TouchCount = TouchTargets.Count;
                UpdateTouchScore();
                break;

            case "Sphere":
                partCount = 0;
              
                
                Debug.Log("SPHERE");

                switch (question)
                {
                    case "face":
                        ShapeReferences[3].PartReferences[1].part.SetActive(true);
                        partCount = ShapeReferences[3].PartReferences[1].part.transform.childCount;

                        for (int i = 0; i < partCount; i++)
                        {
                            TouchTargets.Add(ShapeReferences[3].PartReferences[1].part.transform.GetChild(i).gameObject);
                        }
                        break;

                }
                qno = currentQuestionNumber + 1;
                QuestionText.text = qno.ToString() + ". Touch the " + partCount + "  " + question + " of the Sphere.";
                TouchCount = TouchTargets.Count;
                UpdateTouchScore();

                break;

            case "All":
                EnableShapeTouch();
                Debug.Log("all quesiton");
                currentCorrectAnswer = answer;
                qno = currentQuestionNumber + 1;
               
                QuestionText.text = qno.ToString() + ". "+ question;

                break;

            default:
                Debug.Log("UNKNOWN SHAPE");
                break;
        }

        Debug.Log("WHASDOASOs");

    }

  


    public void ResetAllShapes()
    {
        for (int i = 0; i < ShapeReferences[0].PartReferences[0].part.transform.childCount; i++)
        {
            ShapeReferences[0].PartReferences[0].part.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < ShapeReferences.Length; i++)
        {
            for (int j = 0; j < ShapeReferences[i].PartReferences.Length; j++)
                ShapeReferences[i].PartReferences[j].part.SetActive(false);
        
        }   
    }

 

    List<int> tempOptions = new List<int>();
    public List<int> ShuffleAnswerButton(TextMesh[] options)
    {
        
        tempOptions.Clear();
        for (int i = 0; i < options.Length; i++)
        {
            tempOptions.Add(i);
        }

      //  printQuestions(tempOptions);

        var count = tempOptions.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = tempOptions[i];
            tempOptions[i] = tempOptions[r];
            tempOptions[r] = tmp;
        }
      //  printQuestions(tempOptions);
        return tempOptions;

    }

    private void OnEnable()
    {

        Debug.Log("quiz enabled");
        UpdateSelectedShape();
    }


    public void UpdateSelectedShape()
    {

        Debug.Log(" quiz mode : " + UITest.AllSelected);

        if (UITest.AllSelected)
        {
            return;
        }

        if (Cone.activeSelf)
        {
            SelectedShape = Cone;
            StartQuiz("Cone");
        }
        else
       if (Cube.activeSelf)
        {
            SelectedShape = Cube;
            StartQuiz("Cube");
        }
        else
       if (Cylinder.activeSelf)
        {
            SelectedShape = Cylinder;
            StartQuiz("Cylinder");
        }
        else
           if (Sphere.activeSelf)
        {
            SelectedShape = Sphere;
            StartQuiz("Sphere");
        }
    }
}
