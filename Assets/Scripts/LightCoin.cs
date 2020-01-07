using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCoin : MonoBehaviour {

    public float rotatingSpeed = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotatingSpeed * Time.deltaTime, 0, 0);
    }
}
