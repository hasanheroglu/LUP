using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGenerator : MonoBehaviour {

    public GameObject[] managers;


	// Use this for initialization
	void Start () {
	    int width = 360; 
        int height = 640;
        bool isFullScreen = false; 
        int desiredFPS = 60;

        Screen.SetResolution(width, height, isFullScreen, desiredFPS);
	
        for (int i = 0; i < managers.Length; i++){
            Instantiate(managers[i], transform.position, Quaternion.identity);
        }

        SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
