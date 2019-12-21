//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: UIElement that responds to VR hands and generates UnityEvents
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.IO;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class UIElement : MonoBehaviour
    {
        public CustomEvents.UnityEventHand onHandClick;
        public Animator anim;

        private Text RecordText;

        float restartTimer;
        protected Hand currentHand;

        //-------------------------------------------------
        protected virtual void Awake()
        {
            Button button = GetComponent<Button>();
            RecordText = GameObject.Find("Content").GetComponent<Text>();
            if (button)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }


        //-------------------------------------------------
        protected virtual void OnHandHoverBegin(Hand hand)
        {
            currentHand = hand;
            InputModule.instance.HoverBegin(gameObject);
            ControllerButtonHints.ShowButtonHint(hand, hand.uiInteractAction);
        }


        //-------------------------------------------------
        protected virtual void OnHandHoverEnd(Hand hand)
        {
            InputModule.instance.HoverEnd(gameObject);
            ControllerButtonHints.HideButtonHint(hand, hand.uiInteractAction);
            currentHand = null;
        }


        //-------------------------------------------------
        protected virtual void HandHoverUpdate(Hand hand)
        {
            if (hand.uiInteractAction != null && hand.uiInteractAction.GetStateDown(hand.handType))
            {
                InputModule.instance.Submit(gameObject);
                ControllerButtonHints.HideButtonHint(hand, hand.uiInteractAction);
            }
        }


        //-------------------------------------------------
        protected virtual void OnButtonClick()
        {
            /*//onHandClick.Invoke( currentHand );
            if (gameObject.name == "StartButton"){
                anim.SetTrigger("StartGame");
                GameManagerS.ScreenIndex++;
                SceneManager.LoadScene(GameManagerS.ScreenIndex);
            }else if(gameObject.name == "RecordButton")
            {
                GameManagerS.ReadString();
                Debug.Log("Record");
            }else if(gameObject.name == "ExitButton")
            {
                Debug.Log("Exit");
            }
         */
            Debug.Log("Click " + gameObject.name);
            if (gameObject.name == "BackButton")
            {
                anim.ResetTrigger("Record");
                anim.SetTrigger("BackMenu");
            }
            else
            { 
                anim.SetTrigger("Record");
                ReadString();
            }


        }

        private void ReadString()
        {
            string path = Application.dataPath + "/test.txt";
            RecordText.text = "\tRecords";
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                string content = "";

                content = reader.ReadToEnd();
                
                RecordText.text = content;
                reader.Close();
            }

        }
    }




#if UNITY_EDITOR
    //-------------------------------------------------------------------------
    [UnityEditor.CustomEditor(typeof(UIElement))]
    public class UIElementEditor : UnityEditor.Editor
    {
        //-------------------------------------------------
        // Custom Inspector GUI allows us to click from within the UI
        //-------------------------------------------------
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            UIElement uiElement = (UIElement)target;
            if (GUILayout.Button("Click"))
            {
                InputModule.instance.Submit(uiElement.gameObject);
            }
        }
    }
#endif
}
