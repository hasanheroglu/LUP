using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour {

    public int lives = 2;
    public int lightPoints;
    public bool restart = false;
    public bool levelone = false;
    public bool dead = false;
    public bool isReset = false; // 
    public int checkpointLevelIndex = 0;
    public bool passed = false;
    public int skipAdFrequency = 2; // Every 3 level
    public bool failed = false;
    public bool watchedAd = false;
    public int currentLevelIndex;
    public bool boughtLife = false;
    public int lightCoins = 0;
    public int lightenedCubes = 0;
    public int cubePoint = 10;
    public int ballPoint = 200;
    public int coinPoint = 100;


    private Vector3 initialPosition;
    private PlayerController pc;
    private AudioManager audioManager;
    private GameManager gm;
    private UIManager ui;

	// Use this for initialization
    void Start () {
        dead = false;
        Time.timeScale = 1f;
        boughtLife = false;
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        initialPosition = pc.gameObject.transform.position;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        lightPoints = 0;
        gm.lifeCost = 5000;
        skipAdFrequency = 2;
        lives = 2;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Mag: " + pc.mag + " Started: " + pc.started + "dead: " + dead);


        if (passed)
        {
            audioManager.Play("Success");
            PlayerPrefs.SetInt("LastLevel", currentLevelIndex);
            //gm.totalLightPoints += lightPoints;
            PlayerPrefs.SetInt("LightPoints", gm.totalLightPoints);
            ui.ShowPassedLevel();
            passed = false;
            DestroyGameObjects();
        }


        if (dead)
        {
            Debug.Log("Mag: " + pc.mag + " Started: " + pc.started + "dead: " + dead);
            audioManager.Play("Reset");
            ResetBall();
        }

        if (currentLevelIndex % 5 == 1)
        {
            checkpointLevelIndex = currentLevelIndex - 1;
        }

        if(currentLevelIndex == 1 && lives <= 0)
        {
            SceneManager.LoadScene(1);
        }


        if(lives < 0){
            if(!failed)
                audioManager.Play("Death");
            failed = true;
            pc.started = false;
            if(!levelone)
                ui.EnableFailedLevel();
            PlayerPrefs.SetInt("LastLevel", currentLevelIndex - 1);
        }

        if (boughtLife )
        {
            lives += 2;
            ResetBall();
            boughtLife = false;
            failed = false;
            ui.DisableFailedLevel();
        } 
	}

    public void ResetBall(){
        pc.started = false;
        pc.canShoot = true;
        pc.gameObject.transform.position = initialPosition;

        lives--;
        //isReset = true;
        //isReset = false;
        pc.ResetVelocity();
        dead = false;
    }

    private void DestroyGameObjects(){
        GameObject[] objects = FindObjectsOfType<GameObject>() as GameObject[];
        foreach (GameObject o in objects)
        {
            if (o.tag == "DD" || o.tag == "MainCamera" || o.tag == "LevelManager" || o.tag == "GameManager" || o.tag == "AudioManager" || o.tag == "AdManager") //Don't Delete 
            {
            }
            else
            {
                Destroy(o.gameObject);
            }
        }
    }

    public void TapToReset(){
        LevelManager lm;
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        AudioManager audioManager;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Reset");
        lm.ResetBall();
    }


}
