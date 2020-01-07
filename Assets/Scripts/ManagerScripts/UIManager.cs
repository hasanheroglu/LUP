using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour {
    
    private bool paused;
    private TextMeshProUGUI level;
    private TextMeshProUGUI ballCount;
    private TextMeshProUGUI lightPoint;
    private TextMeshProUGUI pauseText;
    private TextMeshProUGUI completed;
    private TextMeshProUGUI failed;
    private TextMeshProUGUI fLightPoint;
    private TextMeshProUGUI nLightPoint;
    private TextMeshProUGUI ballCost;
    private TextMeshProUGUI summary;
    private int currentBuildIndex;
    private GameObject nextUI; //next level UI
    private GameObject gameUI; //Game UI
    //private GameObject pauseUI; //Pause Screen UI
    private LevelManager lm;
    private GameManager gm;
    private int lightPoints = 0;
    private GameObject failUI;
    private Button buyBall;

    private void Awake()
    {
        nextUI = GameObject.Find("NextUI");
        failUI = GameObject.Find("FailUI");
        pauseText = GameObject.Find("Pause").GetComponent<TextMeshProUGUI>();
        completed = GameObject.Find("Completed").GetComponent<TextMeshProUGUI>();
        failed = GameObject.Find("Failed").GetComponent<TextMeshProUGUI>();
        fLightPoint = GameObject.Find("FLightPoint").GetComponent<TextMeshProUGUI>();
        nLightPoint = GameObject.Find("NLightPoint").GetComponent<TextMeshProUGUI>();
        buyBall = GameObject.Find("BuyBallButton").GetComponent<Button>();
        ballCost = GameObject.Find("BallCost").GetComponent<TextMeshProUGUI>();
        summary = GameObject.Find("SummaryText").GetComponent<TextMeshProUGUI>();
        nextUI.SetActive(false);
        failUI.SetActive(false);
    }

    private void Start()
    {
        gameUI = GameObject.Find("GameUI");
        ballCount = GameObject.Find("BallCount").GetComponent<TextMeshProUGUI>();
        level = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        lightPoint = GameObject.Find("LightPoint").GetComponent<TextMeshProUGUI>();
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        level.text = "LEVEL " + currentBuildIndex.ToString();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        completed.text = "LEVEL " + currentBuildIndex.ToString() + "\nCOMPLETED!";
        failed.text = "LEVEL " + currentBuildIndex.ToString() + " FAILED!";
    }

    private void Update()
    {
        ballCount.text = "BALLS REMAINING: " + lm.lives.ToString();
        ballCost.text = "PAY " + gm.lifeCost.ToString() + " LP";
        fLightPoint.text = "LIGHTPOINTS: " + gm.totalLightPoints.ToString();
        nLightPoint.text = "LIGHTPOINTS: " + gm.totalLightPoints.ToString();

        //AddLightPoints(lightPoints,lm.lightPoints,lightPoint);
        lightPoint.text = "LIGHTPOINTS: " + lm.lightPoints.ToString();

        if(gm.lifeCost <= gm.totalLightPoints){
            buyBall.interactable = true;
        }
        else{
            buyBall.interactable = false;
        }
    }



    public void AddLightPoints(int source, int target, TextMeshProUGUI text){
        int x = source;
        if(x != target){
            x++;
        }

        lightPoint.text = "LIGHTPOINTS: " + x.ToString();
    }

    public void LoadNextLevel()
    {
        IncreaseLevelCount();
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousLevel()
    {
        IncreaseLevelCount();
        lm = FindObjectOfType<LevelManager>();
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        SceneManager.LoadScene(lm.currentLevelIndex - 1);
    }

    public void BuyLife()
    {
        gm = FindObjectOfType<GameManager>();
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if(gm.lifeCost <= gm.totalLightPoints){
            AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.Play("Button");
            Debug.Log("You Can Buy a Ball");
            gm.totalLightPoints -= gm.lifeCost;
            if(gm.lifeCost != 80000)
                gm.lifeCost *= 2;
            PlayerPrefs.SetInt("LightPoints", gm.totalLightPoints);
            lm.boughtLife = true;
        }
        else{
            Debug.Log("You CAN'T Buy a Ball");
            return;
        }

       
    }

    public int TotalPoints(){
        return (lm.lightenedCubes * lm.cubePoint) + (lm.lives * lm.ballPoint) + (lm.lightCoins * lm.coinPoint);
    }

    public void IncreaseLevelCount()
    {
        gm = FindObjectOfType<GameManager>();
        gm.levelCount++;
    }

    public void ShowPassedLevel(){
        summary.text = "";
        if(lm.lightenedCubes != 0)
            summary.text += lm.lightenedCubes.ToString() + " X LIGHTENED CUBES\t" + (lm.lightenedCubes * lm.cubePoint).ToString() + " LP\n";
        if(lm.lives != 0)
            summary.text += lm.lives.ToString() + " X REMAINING BALLS\t" + (lm.lives * lm.ballPoint).ToString() + " LP\n";
        if(lm.lightCoins != 0)
            summary.text += lm.lightCoins.ToString() + " X LIGHTCOINS\t\t" + (lm.lightCoins * lm.coinPoint).ToString() + " LP\n";
        summary.text += "TOTAL\t\t\t\t" + TotalPoints().ToString() + " LP";
        gameUI.SetActive(false);
        nextUI.SetActive(true);
        gm.totalLightPoints += TotalPoints();
        PlayerPrefs.SetInt("LightPoints", gm.totalLightPoints);
    }

    public void EnableFailedLevel(){
        failUI.SetActive(true);
    }

    public void DisableFailedLevel()
    {
        failUI.SetActive(false);
    }



}
