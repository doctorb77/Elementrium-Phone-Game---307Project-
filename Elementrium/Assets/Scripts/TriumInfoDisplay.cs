using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriumInfoDisplay : MonoBehaviour {

    public Animator deleteAnim;
    public static bool deleteOn;

	// Use this for initialization
	void Start () {
        deleteOn = false;
	}
	
	// Update is called once per frame
	public void TriumDelete () {
        if (!deleteOn) {
            deleteAnim.Play("DeleteOptionEnter");
            deleteOn = true;
        } else {
            deleteAnim.Play("DeleteOptionExit");
            deleteOn = false;
        }
	}
}
