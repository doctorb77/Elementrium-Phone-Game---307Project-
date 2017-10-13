using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public static bool inGroup;
    public static bool inReaction;
    public ButtonListControl buttonListControl;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        inGroup = false;
        inReaction = false;
    }

    private void Update()
    {
        
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
            anim.Play("RightSideRetract");
            //GameObject.Find("RightActivation").GetComponent<RightMenu1>().Play();
            inGroup = false;
        }
        else if (RightMenu1.isOn && inReaction)
        {
            anim.Play("ScrollListLeave");
            anim.Play("RightSideRetract");
            //GameObject.Find("RightActivation").GetComponent<RightMenu1>().Play();
            inReaction = false;
        }
    }

}
