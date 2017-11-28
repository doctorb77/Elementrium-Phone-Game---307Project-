using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour {

    public Animator ZoomAnim;
    public Camera cam;
    public bool zoomed;
    Transform start;

	// Use this for initialization
	void Start () {
        start = cam.transform;
        zoomed = false;
	}
	

    float[] getNewPos(float x, float y, float dx, float dy, float vel)
    {
        float[] newCoord = new float[2];

        float dTT = Mathf.Sqrt((x - dx) * (x - dx) + (y - dy) * (y - dy));

        if (dTT <= vel)
        {
            newCoord[0] = dx;
            newCoord[1] = dy;
            return newCoord;
        }

        float ratio = vel / dTT;
        float xDis = (dx - x) * ratio;
        float yDis = (dy - y) * ratio;

        newCoord[0] = x + xDis;
        newCoord[1] = y + yDis;
        return newCoord;    
    }

	// Update is called once per frame
	void Update () {
        GameObject selected = GameObject.FindWithTag("ZoomedBuddy");
        /*
        if (zoomed)
        {
            Debug.Log(selected);
            if (selected != null)
            {
                cam.transform.position = selected.transform.position;
                cam.transform.position.Set(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z - 10);
                Debug.Log(cam.transform.position);
            }
        } else
        {
           // cam.transform.position = start.position;
        }
        */
        float vel = 15;

        if (zoomed && selected != null)
        {
            float[] update = getNewPos(cam.transform.position.x, cam.transform.position.y, selected.transform.position.x, selected.transform.position.y, vel);
            Vector3 newPos = new Vector3(update[0], update[1], cam.transform.position.z);
            cam.transform.position = newPos;
        } else
        {
            float[] update = getNewPos(cam.transform.position.x, cam.transform.position.y, 540, 960, vel);
            Vector3 newPos = new Vector3(update[0], update[1], cam.transform.position.z);
            cam.transform.position = newPos;
        }

        if (ZoomInfo.Instance.infoOn) {
            if (!zoomed)
            {
                zoomed = true;
                if (selected != null) { 
                    Debug.Log("ZOOMING IN");
                    ZoomAnim.SetTrigger("zoomedIn");
                    
                    //ZoomAnim.Play("MainCamera_ZoomIn");
                }
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
