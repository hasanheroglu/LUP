using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public int totalLightPoints = 0;
    public int totalLightGems = 0;
    public int extraLives = 0;
    public int levelCount = 1;
    public int lifeCost = 5000;
    public string selectedBall = "Ball";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        totalLightPoints = PlayerPrefs.GetInt("LightPoints");
        totalLightGems = PlayerPrefs.GetInt("LightGems");
        selectedBall = PlayerPrefs.GetString("SelectedBall");
        if(selectedBall == ""){
            selectedBall = "Ball";
        }
    }



    // Update is called once per frame
    void Update () {
        
	}


}
