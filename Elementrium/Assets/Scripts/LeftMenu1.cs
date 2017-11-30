using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StateHandling;
using Initialization;
using GlossaryObject;

public class LeftMenu1 : MonoBehaviour
{

    public static LeftMenu1 Instance { get { return instance; } }
    private static LeftMenu1 instance;

    public GameObject Menu;
    public Animator anim;
    public bool isOn;
    public GameObject glossary;
    public GameObject achievement;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

    // Use this for initialization
    void Start()
    {
        anim = Menu.GetComponent<Animator>();
        isOn = false;
        glossary.SetActive(false);
    }

    public void Play()
    {
        if (!isOn)
        {
			if (ZoomInfo.Instance.infoOn) {
				ZoomInfo.Instance.CloseZoomin();
			}
            anim.Play("LeftMenuDropDown");
            isOn = true;

        }
        else
        {
            anim.Play("LeftMenuRetract");
            isOn = false;
        }

    }
    public void ToGlossary()
    {
        //SceneManager.LoadScene("Glossary");
		if (ZoomInfo.Instance.infoOn) {
			ZoomInfo.Instance.CloseZoomin();
		}
        glossary.SetActive(true);
        glossary.GetComponent<Glossary>().PopulateGlossaryAtoms();
        glossary.GetComponent<Glossary>().onTab = 1;
        Initialize.sh.setCurrentState("Glossary", true, true);
    }
    public void ToAchievements()
    {
        //SceneManager.LoadScene("Achievements");
		if (ZoomInfo.Instance.infoOn) {
			ZoomInfo.Instance.CloseZoomin();
		}
        achievement.SetActive(true);
        Initialize.sh.setCurrentState("Glossary", true, true);
    }
}
