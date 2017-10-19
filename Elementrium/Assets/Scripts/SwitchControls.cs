using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchControls : MonoBehaviour {
    public Sprite offSprite;
    public Sprite onSprite;
    public Button onOffSwitch;

	public void SwitchToggle () {
        if (onOffSwitch.image.sprite == onSprite) {
            onOffSwitch.image.sprite = offSprite;
        } else if (onOffSwitch.image.sprite == offSprite) {
            onOffSwitch.image.sprite = onSprite;
        }
	}
	
}
