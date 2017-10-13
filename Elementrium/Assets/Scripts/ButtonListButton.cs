using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StateHandling;
using GlossaryObject;
using UnityEngine.SceneManagement;

public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    private Text myText;
    public Animator GlossaryAnim;

    public void SetText(string textString)
    {
        myText.text = textString;
    }
    public void OnClick()
    {
        if (SceneManager.GetActiveScene().name == "Glossary") {
            print("in glossary state");
            if (!Glossary.displayOpen) {
                print("opening tab");
                GlossaryAnim.Play("GlossaryInfoDisplayPopUp");
            }
        }
    }
}
