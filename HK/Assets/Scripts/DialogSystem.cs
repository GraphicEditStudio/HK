using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;
public class DialogSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset inkAsset;
    public TMP_Text dialogText;
    public Button choiceButtons1;
    public Button choiceButtons2;
    public Button choiceButtons3;

    Story _inkStory;
    public StarterAssetsInputs _input;
    void Start()
    {
        _inkStory = new Story(inkAsset.text);
        
        //DialogSystemController();
        //choiceButtons1.onClick.AddListener(ButtonClicker);
        GorevSystem();
        DialogSystemController();
        ButtonClicker();
        GorevSystem();
        DialogSystemController();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.sprint)
        {
            ButtonClicker();
        }
    }
    bool t1 = true;
    
    void ButtonClicker()
    {
        if (t1)
        {
            _inkStory.ChooseChoiceIndex(0);
            Debug.Log(_inkStory.Continue());
            // DialogSystemController();
            t1 = false;
        }
        string savedJson = _inkStory.state.ToJson();
       _inkStory.state.LoadJson(savedJson) ;
    }
    void DegiskenTest()
    {

    }
    public void SetHealthInUI(bool value)
    {
        Debug.Log("Gorev Degisti");
        Debug.Log(_inkStory.variablesState["gorev1"]);
    }
    void GorevSystem()
    {
        _inkStory.ObserveVariable("gorev1", (string varName, object newValue) => {
            SetHealthInUI((bool)newValue);
        });
        
        Debug.Log("Görev System Start");
        Debug.Log(_inkStory.variablesState["gorev1"]);
        //
        _inkStory.SwitchFlow("Görevler");
       _inkStory.ChoosePathString("Görevler");
        while (_inkStory.canContinue)
        {
            Debug.Log(_inkStory.Continue());
        }
       // 
        

    }
    public void DialogSystemController()
    {
        Debug.Log("Dialog System Start");
        //
        _inkStory.RemoveFlow("Görevler");
        _inkStory.SwitchToDefaultFlow();
        while (_inkStory.canContinue)
        {
            Debug.Log(_inkStory.Continue());
        }
        if (_inkStory.currentChoices != null)
        {
            if (_inkStory.currentChoices.Count > 0)
            {
                for (int i = 0; i < _inkStory.currentChoices.Count; ++i)
                {
                    Choice choice = _inkStory.currentChoices[i];
                    choiceButtons1.name = choice.text;
                    Debug.Log("Choice " + (i + 1) + ". " + choice.text);
                }
            }
        }

    }
}
