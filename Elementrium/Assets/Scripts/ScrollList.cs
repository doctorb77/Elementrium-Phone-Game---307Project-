using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public ButtonListControl buttonListControl;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void ExitScroll()
    {
        if (RightMenu1.isOn && RightMenu1.inGroup)
        {
            RightMenu1.inGroup = false;
            anim.Play("ScrollListLeave");
            anim.Play("RightSideRetract");
            //GameObject.Find("RightActivation").GetComponent<RightMenu1>().Play();
        }
        else if (RightMenu1.isOn && RightMenu1.inReaction)
        {
            RightMenu1.inReaction = false;
            anim.Play("ScrollListLeave");
            anim.Play("RightSideRetract");
            //GameObject.Find("RightActivation").GetComponent<RightMenu1>().Play();
        }
    }

}
