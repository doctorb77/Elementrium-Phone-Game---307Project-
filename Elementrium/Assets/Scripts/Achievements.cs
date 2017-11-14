using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;
using Initialization;

public class Achievements : MonoBehaviour
{
    public GameObject achievements;
    public ButtonListControl buttonListControl;

    public void ExitAchievements()
    {
        //SceneManager.LoadScene("MainGameScene");
        achievements.SetActive(false);
        Initialize.sh.setCurrentState("MainGameScene",true,true);
    }
    void Start()
    {
        buttonListControl.PopulateAchievementList("Achievement");
    }
}
