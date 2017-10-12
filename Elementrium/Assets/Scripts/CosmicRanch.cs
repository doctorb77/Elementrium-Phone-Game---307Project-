using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CosmicRanch : MonoBehaviour
{

    public SpriteRenderer background;
    public SpriteRenderer space;
    public SpriteRenderer rim;
    public GameObject Wormhole;
    public int colorChoice;
    public Color32 color;
    public Color32 rimColor;
    public List<GameObject> buddies = new List<GameObject>();

    // Use this for initialization

    void Start()
    {
        if (colorChoice == 0)
        {
            color = new Color32(120, 45, 212, 255);  // SetColor("_Color", Color.blue);
            rimColor = new Color32(103, 104, 255, 255);
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        //space.color = color;
        //rim.color = rimColor;
    }

    public void ChangeColor()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Blue":
                colorChoice = 0;
                color = new Color32(120, 45, 212, 170);
                rimColor = new Color32(43, 196, 232, 255);
                break;
            case "Green":
                colorChoice = 1;
                color = new Color32(6, 193, 81, 170);
                rimColor = new Color32(82, 236, 115, 255);
                break;
            case "Yellow":
                colorChoice = 2;
                color = new Color32(255, 185, 0, 170);
                rimColor = new Color32(255, 255, 114, 255);
                break;
            case "Purple":
                colorChoice = 3;
                rimColor = new Color32(172, 77, 193, 255);
                color = new Color32(215, 0, 111, 170);
                break;
            case "Orange":
                colorChoice = 4;
                rimColor = new Color32(218, 177, 90, 255);
                color = new Color32(255, 121, 0, 170);
                break;
        }
        space.color = color; // SetColor("_Color", color);
        rim.color = rimColor; // SetColor("_Color", color);
    }

}
