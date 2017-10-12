using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().angularVelocity = 2;
        GetComponent<Rigidbody2D>().inertia = 0;	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().angularVelocity = 2;
    }
}
