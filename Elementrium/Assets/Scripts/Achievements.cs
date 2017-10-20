using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateHandling;

public class Achievements : MonoBehaviour
{

    public ButtonListControl buttonListControl;

    public void ExitAchievements()
    {
        SceneManager.LoadScene("MainGameScene");
        StateHandler.setCurrentState("MainGameScene",true,true);
    }
    void Start()
    {
        buttonListControl.PopulateAchievementList("Achievement");
    }
}
