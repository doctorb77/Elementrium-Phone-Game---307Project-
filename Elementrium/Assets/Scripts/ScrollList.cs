using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public bool inGroup;
    public bool inReaction;
    public ButtonListControl buttonListControl;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        inGroup = false;
        inReaction = false;
    }

    public void InteractGroup()
    {
        if (RightMenu1.isOn && !inGroup)
        {
            inGroup = true;
            buttonListControl.PopulateList("Molecule");
            anim.Play("ScrollListEnter");

        }
    }
    public void InteractReaction()
    {
        if (RightMenu1.isOn && !inReaction)
        {
            inReaction = true;
            buttonListControl.PopulateList("Compound");
            anim.Play("ScrollListEnter");
        }
    }
    public void ExitScroll()
    {
        if (RightMenu1.isOn && inGroup)
        {
            anim.Play("ScrollListLeave");
            inGroup = false;
        }
        else if (RightMenu1.isOn && inReaction)
        {
            anim.Play("ScrollListLeave");
            inReaction = false;
        }
    }

}
