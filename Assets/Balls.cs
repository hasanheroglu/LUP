using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balls : MonoBehaviour {

    public Texture2D[] balls;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<RawImage>().texture = balls[Random.Range(0, balls.Length)];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
