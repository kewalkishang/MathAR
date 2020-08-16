//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class HelloARController : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a vertical plane.
        /// </summary>
        public GameObject GameObjectVerticalPlanePrefab;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a horizontal plane.
        /// </summary>
        public GameObject GameObjectHorizontalPlanePrefab;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a feature point.
        /// </summary>
        public GameObject GameObjectPointPrefab;

        /// <summary>
        /// The rotation in degrees need to apply to prefab when it is placed.
        /// </summary>
        private const float k_PrefabRotation = 180.0f;
       
        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;

        bool anchorDropped = false;
        public bool dragging = false;
        /// Variables for references 
        /// 
        public GameObject Background3DMenu;
        public GameObject QuizMenu;
        public GameObject SelectedShapePosition;
        public GameObject SelectedParent;
        public GameObject Cube;
        public GameObject Cone;
        public GameObject Cylinder;
        public GameObject Sphere;
        public GameObject ARVisualizer;
        public GameObject ARParent;
        public GameObject ARBase;
        public GameObject UserCautionMsg;
        public GameObject userFeedback;
        public GameObject InteractionGuideButton1;
        public GameObject InteractionGuideButton2;
        public Sprite aron;
        public Sprite aroff;
        public Image ToggleButtonLearn;
        public Image ToggleButtonQuiz;
        /*public GameObject ShapeDetailBox;
        public GameObject ComponentBox;
        public GameObject LearningBox;
        public GameObject QuizBox;*/
        public GameObject DisplayBoxes;
        public Text FeedbackText;
        public float ForwardDistance = 1f;
        int count = 0;
        ///Variables for States
        private bool IsAR = false;
        private GameObject SelectedShape;
        public static HelloARController instance = null;
        Touch touch0, touch1;
        float distance;
        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        public void Start()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;//Avoid doing anything else
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("Starting " + this.name);
        }
        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {
            _UpdateApplicationLifecycle();


            /*   if (!Background3DMenu.activeSelf)
               {
                   DisplayBoxes.transform.LookAt(FirstPersonCamera.transform);
                  // DisplayBoxes.transform.localEulerAngles = new Vector3(DisplayBoxes.transform.localEulerAngles.x , DisplayBoxes.transform.localEulerAngles.y +180 , DisplayBoxes.transform.localEulerAngles.z);
               } */
            // If the player has not touched the screen, we are done with this update.
            if (!anchorDropped)
            {
                Touch touch;
                if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
                {

                    return;
                }

                // Should not handle input if the player is pointing on UI.
                /* if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                 {
                     count = count + 1;
                     FeedbackText.text = "HITTING ui " + count; 
                     return;
                 }*/

                if ((StateTracker.instance.getCurrentState() == StateTracker.State.LearningMode || StateTracker.instance.getCurrentState() == StateTracker.State.QuizMode) &&  IsAR && !UserCautionMsg.activeSelf)
                {
                    // count = count + 1;
                    // FeedbackText.text = "INSIDE LOOP" + count;


                    // Raycast against the location the player touched to search for planes.
                    TrackableHit hit;
                    TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                        TrackableHitFlags.FeaturePointWithSurfaceNormal;


                    if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                    {
                        // count = count + 1;
                        // FeedbackText.text = "RAYCAST " + count;
                        // Use hit pose and camera pose to check if hittest is from the
                        // back of the plane, if it is, no need to create the anchor.
                        if ((hit.Trackable is DetectedPlane) &&
                            Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                                hit.Pose.rotation * Vector3.up) < 0)
                        {
                            Debug.Log("Hit at back of the current DetectedPlane");
                        }
                        else
                        {
                            // Choose the prefab based on the Trackable that got hit.
                            GameObject prefab;
                            if (hit.Trackable is FeaturePoint)
                            {
                                //  prefab = GameObjectPointPrefab;
                                Debug.Log("HIT featurepoint");
                            }
                            else if (hit.Trackable is DetectedPlane)
                            {
                                DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                                if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                                {
                                    //  prefab = GameObjectVerticalPlanePrefab;
                                    //   GameObjectVerticalPlanePrefab.transform.position = hit.Pose.position;


                                }
                                else
                                {
                                    /// GameObjectHorizontalPlanePrefab.transform.position = hit.Pose.position;
                                    //  GameObjectHorizontalPlanePrefab.transform.Rotate(0, k_PrefabRotation, 0, Space.Self);
                                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                                    // Make game object a child of the anchor.
                                    // GameObjectVerticalPlanePrefab.transform.parent = anchor.transform;
                                  //  FeedbackText.text = "CLICKED ON HORIXONTAL PLANE";
                                    ARParent.transform.parent = anchor.transform;
                                    ARParent.transform.localPosition = Vector3.zero;
                                    ARParent.transform.rotation = Quaternion.identity;
                                    anchorDropped = true;
                                    ARParent.SetActive(true);
                                    userFeedback.SetActive(false);
                                    InteractionGuideButton1.SetActive(true);
                                    InteractionGuideButton2.SetActive(true);
                                    ARVisualizer.SetActive(false);
                                    //SelectedShape.transform.position = hit.Pose.position;


                                }
                            }


                        }
                    }
                }
            }
            else
            {
                StateTracker.State state = StateTracker.instance.getCurrentState();
                if (Input.touchCount == 1 && (state == StateTracker.State.LearningMode || state == StateTracker.State.QuizMode) && IsAR)
                {
                    Touch touch = Input.touches[0];

                    if (touch.phase == TouchPhase.Began)
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        // Does the ray intersect any objects excluding the player layer
                        if (Physics.Raycast(ray, out hit))
                        {



                            Debug.Log("You hit part " + hit.transform.name); // ensure you picked right object
                                                                             //GameObject hitpart = hit.transform.gameObject;
                            if (hit.transform.gameObject.tag == "base")
                                dragging = true;


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
                        if (dragging)
                        {

                            TrackableHit hit;
                            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                                TrackableHitFlags.FeaturePointWithSurfaceNormal;


                            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                            {
                                // count = count + 1;
                                // FeedbackText.text = "RAYCAST " + count;
                                // Use hit pose and camera pose to check if hittest is from the
                                // back of the plane, if it is, no need to create the anchor.
                                if ((hit.Trackable is DetectedPlane) &&
                                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                                        hit.Pose.rotation * Vector3.up) < 0)
                                {
                                    Debug.Log("Hit at back of the current DetectedPlane");
                                }
                                else
                                {
                                    // Choose the prefab based on the Trackable that got hit.
                                    GameObject prefab;
                                    if (hit.Trackable is FeaturePoint)
                                    {
                                        //  prefab = GameObjectPointPrefab;
                                        Debug.Log("HIT featurepoint");
                                    }
                                    else if (hit.Trackable is DetectedPlane)
                                    {
                                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                                        {
                                            //  prefab = GameObjectVerticalPlanePrefab;
                                            //   GameObjectVerticalPlanePrefab.transform.position = hit.Pose.position;


                                        }
                                        else
                                        {

                                            ARParent.transform.position = hit.Pose.position;

                                        }
                                    }


                                }
                            }



                            //  SelectedShape.transform.Rotate(touch.deltaPosition.y * rotationRate, -touch.deltaPosition.x * rotationRate, 0, Space.World);
                        }
                        //  cube.transform.RotateAround(Vector3.down, touch.deltaPosition.x * rotationRate);
                        //   cube.transform.RotateAround(Vector3.right, touch.deltaPosition.y * rotationRate);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        //  text.text = "Touch phase ended";
                        Debug.Log("Touch phase Ended");
                        dragging = false;
                    }
                }
                else
                    if (Input.touchCount == 2 && (state == StateTracker.State.LearningMode || state == StateTracker.State.QuizMode) && IsAR)
                {

                    
                    if (Input.GetTouch(1).phase == TouchPhase.Began)
                    {
                        touch0 = Input.GetTouch(0);
                        touch1 = Input.GetTouch(1);

                        Vector3 midpoint = (touch0.position + touch1.position) / 2;
                        float newDistance0 = Vector2.Distance(midpoint, touch0.position);
                        float newDistance1 = Vector2.Distance(midpoint, touch1.position);

                        distance = (newDistance0 + newDistance1) / 2;
                        FeedbackText.text = "BEGAN " + distance;
                    }
                    else
                    if ((Input.GetTouch(1).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Moved))
                    {
                        FeedbackText.text = "MOVING ";
                        touch0 = Input.GetTouch(0);
                        touch1 = Input.GetTouch(1);
                    
                        Vector3 midpoint = (touch0.position + touch1.position) / 2;
                        float newDistance0 = Vector2.Distance(midpoint, touch0.position);
                        float newDistance1 = Vector2.Distance(midpoint, touch1.position);
                        float ndistance = (newDistance0 + newDistance1) / 2;
                        float pinch = ndistance/distance;
                        float scale = Mathf.Clamp( pinch , 0.5f, 1.5f);
                        ARParent.transform.localScale = new Vector3(scale, scale, scale);
                        FeedbackText.text = "MOVING : " +   ndistance + " || " + scale;
                    }
                    
                }
            }
        }

        public void ToggleAR()
        {
            IsAR = !IsAR;

            StateTracker.instance.setARState(IsAR);

            if(IsAR)
            {
                SwitchOnAR();
                ToggleButtonLearn.sprite = aron;
                ToggleButtonQuiz.sprite = aron;
            }
            else
            {
                SwitchOffAR();
                ToggleButtonLearn.sprite = aroff;
                ToggleButtonQuiz.sprite = aron;
            }
        }

        public void SwitchOnAR()
        {
            UpdateSelectedShape();
            ARVisualizer.SetActive(true);
            Debug.Log("toggleButton ON clicked");
            IsAR = true;
            dragging = false;
            InteractionGuideButton1.SetActive(false);
            InteractionGuideButton2.SetActive(false);
            //  ARParent.SetActive(true);
            Background3DMenu.SetActive(false);
            //  QuizMenu.SetActive(false);
            // SelectedShape.transform.SetParent(FirstPersonCamera.transform);
            // SelectedShape.transform.localPosition = Vector3.forward * ForwardDistance;
            //  Vector3 currentGlobalPos = SelectedShape.transform.position;
            ARParent.SetActive(false);

            SelectedShape.transform.parent = ARParent.transform;
            SelectedShape.transform.localPosition = new Vector3(0,0.7f,0);
            SelectedShape.transform.localRotation = Quaternion.identity;
            DisplayBoxes.transform.parent = ARBase.transform;
            DisplayBoxes.transform.localPosition = new Vector3(0, 0 , 6);
            DisplayBoxes.transform.localEulerAngles = new Vector3( -90 , 0, 0);
            DisplayBoxes.transform.localScale = new Vector3(1, 1, 1);

            UserCautionMsg.SetActive(true);

          
            userFeedback.SetActive(true);
            userFeedback.GetComponent<Text>().text = "Touch on any plane to place your Shape. Toggle AR mode to replace Shape.";
            // SelectedShape.transform.position = new Vector3(currentGlobalPos.x, FirstPersonCamera.transform.position.y, currentGlobalPos.z);

        }

        public void SwitchOffAR()
        {
            dragging = false;
            UpdateSelectedShape();
            ARVisualizer.SetActive(false);
            InteractionGuideButton1.SetActive(false);
            InteractionGuideButton2.SetActive(false);
            Debug.Log("toggleButton OFF clicked");
            IsAR = false;
            userFeedback.SetActive(false);
            Background3DMenu.SetActive(true);
            //QuizMenu.SetActive(false);
            ARParent.transform.localScale = new Vector3(1, 1, 1);

            SelectedShape.transform.parent = SelectedParent.transform;
            SelectedShape.transform.localPosition = SelectedShapePosition.transform.localPosition;
            FeedbackText.text = "SWITCH OFF CALLED";

            DisplayBoxes.transform.parent = SelectedParent.transform;
            DisplayBoxes.transform.localPosition = new Vector3(-1.5f, 0, 0);
            DisplayBoxes.transform.localEulerAngles = new Vector3(0, 180, 0);
            DisplayBoxes.transform.localScale = new Vector3(1, 1, 1);
       
            UserCautionMsg.SetActive(false);
            anchorDropped = false;
            ARParent.SetActive(false);
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

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
               // Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }



   

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
