using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    private Text myText;

    public void SetText(string textString)
    {
        myText.text = textString;
    }
    public void OnClick()
    {
        Debug.Log("BUTTON CLICKED");
        GameObject rightMenu = GameObject.Find("RightActivation");
        rightMenu.GetComponent<RightMenu1>().Play();
    }

    public void OnMouseDown()
    {
        Debug.Log("BUTTON DOWN");
        OnClick();
    }
}
