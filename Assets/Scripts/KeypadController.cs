using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class KeypadController : MonoBehaviour
{
    public List<int> correctKey = new List<int>();
    private List<int> inputKeyList = new List<int>();

    public Animator controller;
    public string animationName;
    public float timelineSpeed;

    [SerializeField] private TMP_InputField codeDisplay;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private string successText;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectKey;
    public UnityEvent onIncorrectKey;

    public bool allowMultipleActivations = false;
    private bool hasUsedCorrectCode = false;

    public bool HasUsedCorrectCode { get { return hasUsedCorrectCode; } }

    private void OnEnable()
    {
        controller.speed = timelineSpeed;
    }

    
    public void UserNumberEntry(int selectedNum)
    {
        if (inputKeyList.Count >= 4) return;

        inputKeyList.Add(selectedNum);

        UpdateDispay();

        if (inputKeyList.Count >= 4) CheckKey();
    }
    private void CheckKey()
    {
        for (int i = 0; i < correctKey.Count; i++)
        {
            if (inputKeyList[i] != correctKey[i])
            {
                IncorrectKey();
                return;
            }
        }
        CorrectKeyGiven();
    }

    private void CorrectKeyGiven()
    {
        if (allowMultipleActivations)
        {
            
            onCorrectKey.Invoke();
            codeDisplay.text = successText;
            //controller.Play(animationName, -1, timelineSpeed);
            StartCoroutine(RestKeyCode());
        }
        else if (!allowMultipleActivations && !hasUsedCorrectCode)
        {
            Debug.Log("M");
            onCorrectKey.Invoke();
            hasUsedCorrectCode = true;
            codeDisplay.text = successText;
            controller.Play(animationName, -1, timelineSpeed);
        }
    }
    private void IncorrectKey()
    {
        onIncorrectKey.Invoke();
        StartCoroutine(RestKeyCode());
    }
    private void UpdateDispay()
    {
        codeDisplay.text = null;
        for (int i = 0; i < inputKeyList.Count; i++)
        {
            codeDisplay.text += inputKeyList[i];
        }
    }
    public void DeleteEntry()
    {
        if (inputKeyList.Count <= 0) return;

        var listposition = inputKeyList.Count - 1;
        inputKeyList.RemoveAt(listposition);

        UpdateDispay();
    }

    IEnumerator RestKeyCode()
    {
        yield return new WaitForSeconds(resetTime);

        inputKeyList.Clear();
        codeDisplay.text = "Enter Code...";
    }
}
