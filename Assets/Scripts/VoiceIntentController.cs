using Meta.WitAi.Json;
using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.XR;

public class VoiceIntentController : MonoBehaviour
{
    // add AppVoiceExperiance reference
    [Header("Voice")]
    [SerializeField]
    private AppVoiceExperience appVoiceExperience;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI fullTranscriptText;

    [SerializeField]
    private TextMeshProUGUI partialTranscriptText;

    private ShapeController[] controllers;

    private bool isAppVoiceActive;

    private string saidPortalName;
    public GameObject portal;
    private void Awake()
    {
        controllers = FindObjectsOfType<ShapeController>();
        fullTranscriptText.text = partialTranscriptText.text = string.Empty;

        //bind transcriptions and activate state
        appVoiceExperience.VoiceEvents.onFullTranscription.AddListener((transcription) =>
        {
            fullTranscriptText.text = transcription;
        });

        appVoiceExperience.VoiceEvents.OnPartialTranscription.AddListener((transcription) =>
        {
            partialTranscriptText.text = transcription;

            saidPortalName = transcription;

            
        });

        appVoiceExperience.VoiceEvents.OnRequestCreated.AddListener((request) =>
        {
            isAppVoiceActive = true;
            Debug.Log("OnRequestCreated Active");
        });

        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(() =>
        {
            isAppVoiceActive = false;
            Debug.Log("OnRequestCompleted Active");
            if (saidPortalName != "" && saidPortalName != null)
            {
                bool correctWord = portal.name == saidPortalName;

                if (correctWord)
                {
                    Debug.Log("Success");
                    portal.SetActive(true);
                }

            }
        });
    }

    private void Update()
    {
        //if (Keyboard.current.spaceKey.wasPressedThisFrame && !isAppVoiceActive)
        if (OVRInput.GetDown(OVRInput.RawButton.X) && !isAppVoiceActive)
        {
            appVoiceExperience.Activate();
        }
    }

    public void SetColor(string[] info)
    {
        DisplayValues("SetColor:", info);
        Destroy(portal);
        //set color info based on intent response
        if (info.Length > 0 && UnityEngine.ColorUtility.TryParseHtmlString(info[0], out Color color))
        {
            
            Debug.Log("colorout");
            foreach (var controller in controllers)
            {
                Debug.Log("colorin");
                controller.SetColor(color);
            }
        }
    }

    private static void DisplayValues(string prefix, string[] info)
    {
        foreach(var i in info)
        {
            Debug.Log($"{prefix} {i}");
        }
    }

    //private void SetSaidSpell(WitResponseNode response)
    //{
    //    string intentName = response["intents"][0]["name"].Value.ToString();

    //    saidPortalName = intentName;
    //}
}
