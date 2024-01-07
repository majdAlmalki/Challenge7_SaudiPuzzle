using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Voice.Dictation;
using Meta.WitAi;
using UnityEngine.InputSystem;

public class VoiceDictation123 : MonoBehaviour
{

    string temp = "";
    public string magicWord = "";
 
    //Attach the AppDict script
    public AppDictationExperience voiceToText;



    public void GetTheString(string newText)
    {
        temp = newText;
        //compares the two words
        if (temp == magicWord )
        {
            print(temp);
            
            print("Correct!!");
        }
        else
        {
            print(temp);


            print("###### Try again");
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        voiceToText.DictationEvents.OnFullTranscription.AddListener(GetTheString);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        print("Listening ******");
    //        voiceToText.Activate();
    //    }

    //    else if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        voiceToText.Deactivate();
    //    }
        
    //}
    public void TriggerPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Lesson");
        if (context.performed) WitActivate();
    }
    public void WitActivate()
    {
        Debug.Log("Lesson11");
        voiceToText.Activate();
    }
}
