using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Ink.Runtime;

public class DialogTest : MonoBehaviour
{
    public StarterAssetsInputs assetsInputs;
    public TextAsset inkAsset;
    Story _inkStory;
    // Start is called before the first frame update
    void Start()
    {

        List<string> list = new List<string>();

        list.Add("gorev1");
        list.Add("gorev2");
        list.Add("gorev3");
        list.Add("gameOver");

        _inkStory = new Story(inkAsset.text);
        DialogGet();
        
        
    }
    void DialogGet()
    {
        while(_inkStory.canContinue)
        {
            Debug.Log(_inkStory.Continue());
        }
        if(_inkStory.currentChoices.Count>0)
        {
            foreach(var choice in _inkStory.currentChoices)
            {
                Debug.Log(choice.text);

            }
            
        }
    }
    public void DegiskenState(bool value)
    {
        Debug.Log("Degisken State");
    }
    void GorevDegisken()
    {
        Debug.Log(_inkStory.variablesState["gorev1"]);
    }
    // Update is called once per frame
    void Update()
    {

        if(assetsInputs.jump)
        {
            _inkStory.ObserveVariable ("gorev1", (string varName, object newValue) => {
                DegiskenState((bool)newValue);
                });


            _inkStory.ChooseChoiceIndex(0);
            DialogGet();






            //

            {
                //
                assetsInputs.jump = false;
            }

        }
    }
}
