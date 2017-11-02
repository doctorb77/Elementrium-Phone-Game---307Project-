using System.Collections;
using System.Collections.Generic;
using BackpackObject;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour {

    public GameObject bar;
    public bool increasing;
    public double currentFill;
	// Use this for initialization
	void Start () {
        bar.GetComponent<Image>().fillAmount = (float) (Backpack.getLevelPercentage() * 0.66); 
        currentFill = (float)(Backpack.getLevelPercentage() * 0.66);
    }
	
	// Update is called once per frame
	void Update () {
        double percentage = Backpack.getLevelPercentage();
        float goalFill = (float) (percentage * 0.66);

        if (currentFill != goalFill)
        {
            currentFill += 0.003;
        }

        if (currentFill > 0.660001)
        {
            currentFill = 0;
        }

        if (Mathf.Abs((float) currentFill - goalFill) <= 0.004)
            currentFill = goalFill;

        bar.GetComponent<Image>().fillAmount = (float) currentFill;

        //GetComponent<>
	}
}
