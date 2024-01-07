using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateVoice : MonoBehaviour
{
    [SerializeField]
    private Wit wit;

    
    // Update is called once per frame
    void Update()
    {
      if(wit == null) wit = GetComponent<Wit>();  
    }
    public void TriggerPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Lesson");
        if (context.performed) WitActivate();
    }
    public void WitActivate()
    {
        Debug.Log("Lesson11");
        wit.Activate();
    }
}
