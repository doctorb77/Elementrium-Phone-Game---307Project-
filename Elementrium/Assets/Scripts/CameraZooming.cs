using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour {

    public Animator ZoomAnim;
    public bool zoomed;

	// Use this for initialization
	void Start () {
        zoomed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (ZoomInfo.Instance.infoOn) {
            if (!zoomed) {
                zoomed = true;
                Debug.Log("ZOOMING IN");
                ZoomAnim.SetTrigger("zoomedIn");
                //ZoomAnim.Play("MainCamera_ZoomIn");
            }
        } else if (!ZoomInfo.Instance.infoOn) {
            if (zoomed) {
                zoomed = false;
                Debug.Log("ZOOMING OUT");
                ZoomAnim.Play("MainCamera_ZoomOut");
            }
        }
	}
}
