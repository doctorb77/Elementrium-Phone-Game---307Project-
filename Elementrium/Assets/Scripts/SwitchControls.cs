using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchControls : MonoBehaviour {



    public Sprite offSprite;
    public Sprite onSprite;
    public Button onOffSwitch;

	public void SwitchToggleMusic () {
        if (onOffSwitch.image.sprite == onSprite) {
            onOffSwitch.image.sprite = offSprite;
            TopMenu1.Instance.musicIsOn = false;
        } else if (onOffSwitch.image.sprite == offSprite) {
            onOffSwitch.image.sprite = onSprite;
            TopMenu1.Instance.musicIsOn = true;
        }
	}
	public void SwitchToggleSound()
	{
		if (onOffSwitch.image.sprite == onSprite)
		{
			onOffSwitch.image.sprite = offSprite;
            TopMenu1.Instance.soundFxIsOn = false;
		}
		else if (onOffSwitch.image.sprite == offSprite)
		{
			onOffSwitch.image.sprite = onSprite;
            TopMenu1.Instance.soundFxIsOn = true;
		}
	}
	public void SwitchToggleFace()
	{
		if (onOffSwitch.image.sprite == onSprite)
		{
			onOffSwitch.image.sprite = offSprite;
            TopMenu1.Instance.faceIsOn = false;
		}
		else if (onOffSwitch.image.sprite == offSprite)
		{
			onOffSwitch.image.sprite = onSprite;
            TopMenu1.Instance.faceIsOn = true;
		}
	}
}
