using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CosmicRanch : MonoBehaviour {

    public SpriteRenderer background;
    public GameObject Wormhole;
	public int colorChoice;
	public Color32 color;

	// Use this for initialization
	
    void Start () {
        if (colorChoice == 0)
        {
            background.material.SetColor("_Color", Color.black);
        } else {
            background.material.SetColor("_Color", color);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //background.material.SetColor("_Color", TopMenu1.color);
	}
    public void ChangeColor () 
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Black":
                colorChoice = 1;
                color = new Color32(0, 0, 0, 255) ;
                break;
            case "Red":
                colorChoice = 2;
                color = new Color32(58, 0, 0, 255);
                break;
            case "Purple":
                colorChoice = 3;
                color = new Color32(38 , 0, 58, 255);
                break;
            case "Blue":
                colorChoice = 4;
                color = new Color32(0, 0, 58, 255);
                break;
            case "Green":
                colorChoice = 5;
                color = new Color32(0, 30, 3, 255);
                break;
        }
        background.material.SetColor("_Color", color);
    }

}
