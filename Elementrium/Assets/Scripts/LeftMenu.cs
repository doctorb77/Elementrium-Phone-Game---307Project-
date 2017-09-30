using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftMenu : MonoBehaviour
{

    public GameObject Menu;
    public Animator anim;
    public bool isOn;

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "LevelStat")
                {
                    if (!isOn)
                    {
                        anim.Play("LeftMenuDropDown");
                        isOn = true;
                    }
                    else
                    {
                        anim.Play("LeftMenuRetract");
                        isOn = false;
                    }
                }
				if (hit.transform.gameObject.name == "ButtonGlossary")
				{
					if (isOn)
					{
                        SceneManager.LoadScene("Glossary");
					}
				}
				if (hit.transform.gameObject.name == "ButtonAchievement")
				{
					if (isOn)
					{
						SceneManager.LoadScene("Achievements");
					}
				}
            }
        }
    }
}
