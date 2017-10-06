using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour
{

    public ButtonListControl buttonListControl;

    public void ExitAchievements()
    {
        SceneManager.LoadScene("MainGameScene");
    }
    void Start()
    {
        buttonListControl.PopulateList("Achievement");
    }
}
