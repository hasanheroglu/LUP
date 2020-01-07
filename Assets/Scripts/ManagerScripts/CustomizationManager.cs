using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour {

    public Ball[] balls;
    public Ball currentBall;
    public int currentBallIndex = 0;
    public GameManager gameManager;

    private TextMeshProUGUI buyLP;
    private TextMeshProUGUI buyLG;
    private TextMeshProUGUI lightPointText;
    private TextMeshProUGUI lightGemText;
    private TextMeshProUGUI ballNameText;

    private GameObject buyLPButton;
    private GameObject buyLGButton;
    public GameObject selectButton;
    public GameObject selectedText;

    private bool selectDefault = true;


	// Use this for initialization
	private void Awake () {
        buyLP = GameObject.Find("LP").GetComponent<TextMeshProUGUI>();
        buyLG = GameObject.Find("LG").GetComponent<TextMeshProUGUI>();
        ballNameText = GameObject.Find("BallName").GetComponent<TextMeshProUGUI>();
        lightPointText = GameObject.Find("LightPoint").GetComponent<TextMeshProUGUI>();
        lightGemText = GameObject.Find("LightGem").GetComponent<TextMeshProUGUI>();
        buyLPButton = GameObject.Find("BuyWithLP");
        buyLGButton = GameObject.Find("BuyWithLG");
        selectButton = GameObject.Find("Select");
        selectedText = GameObject.Find("Selected");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        buyLPButton.SetActive(false);
        buyLGButton.SetActive(false);
        selectButton.SetActive(false);
        selectedText.SetActive(false);
        Time.timeScale = 1.0f;
        for (int i = 0; i < balls.Length; i++){
            if(PlayerPrefs.GetInt("BallBought" + i) == 1)
                balls[i].bought = true;
            else
                balls[i].bought = false;
            if(PlayerPrefs.GetInt("BallSelected" + i) == 1){
                balls[i].selected = true;
                selectDefault = false;
            }
            else
                balls[i].selected = false;
        }

        balls[0].bought = true;
        if(selectDefault)
            balls[0].selected = true;

	}
	
	// Update is called once per frame
	void Update () {

        lightGemText.text = "LIGHTGEMS: " + gameManager.totalLightGems.ToString(); 
        lightPointText.text = "LIGHTPOINTS: " + gameManager.totalLightPoints.ToString(); 

        ballNameText.text = currentBall.name;
        currentBall = balls[currentBallIndex];
        currentBall.ball.SetActive(true);

        if (!currentBall.bought)
        {
            buyLP.text = "PAY " + currentBall.lightPointCost + " LP";
            buyLG.text = "PAY " + currentBall.lightGemCost + " LG";
            buyLPButton.SetActive(true);
            buyLGButton.SetActive(true);
            selectedText.SetActive(false);
            selectButton.SetActive(false);
        }

        else if (currentBall.bought && !currentBall.selected)
        {
            buyLPButton.SetActive(false);
            buyLGButton.SetActive(false);
            selectedText.SetActive(false);
            selectButton.SetActive(true);
        }

        else if (currentBall.bought && currentBall.selected)
        {
            buyLPButton.SetActive(false);
            buyLGButton.SetActive(false);
            selectButton.SetActive(false);
            selectedText.SetActive(true);
        }

        if(gameManager.totalLightGems < currentBall.lightGemCost){
            buyLGButton.GetComponent<Button>().interactable = false;
        }
        else{
            buyLGButton.GetComponent<Button>().interactable = true;
        }

        if(gameManager.totalLightPoints < currentBall.lightPointCost){
            buyLPButton.GetComponent<Button>().interactable = false;
        }
        else{
            buyLPButton.GetComponent<Button>().interactable = true;
        }
            
	}

    public void NextBall(){
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        currentBall.ball.SetActive(false);
        currentBallIndex++;
        if(currentBallIndex == balls.Length){
            currentBallIndex = 0;
        }
    }

    public void PrevBall(){
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        currentBall.ball.SetActive(false);
        currentBallIndex--;
        if (currentBallIndex < 0){
            currentBallIndex = balls.Length - 1;
        }

    }

    public void BuyLP(){
        if(currentBall.lightPointCost < gameManager.totalLightPoints){
            AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.Play("Button");
            gameManager.totalLightPoints -= currentBall.lightPointCost;
            currentBall.bought = true;
            PlayerPrefs.SetInt("LightPoints", gameManager.totalLightPoints);
            PlayerPrefs.SetInt("BallBought" + currentBallIndex, 1);
        }
        else{
            //you dont have enough money disable the button
        }
        //communicate with gm
    }

    public void BuyLG()
    {
        if (currentBall.lightGemCost < gameManager.totalLightGems){
            AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.Play("Button");
            gameManager.totalLightGems -= currentBall.lightGemCost;
            currentBall.bought = true;
            PlayerPrefs.SetInt("LightGems", gameManager.totalLightGems);
            PlayerPrefs.SetInt("BallBought" + currentBallIndex, 1);
        }
        else
        {
            //you dont have enough money disable the button
        }
        //communicate with gm
    }

    public void Select(){
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        gameManager.selectedBall = currentBall.ball.name;
        currentBall.selected = true;
        PlayerPrefs.SetString("SelectedBall", currentBall.ball.name);
        Debug.Log(currentBall.ball.name);
        for (int i = 0; i < balls.Length; i++){
            if(i == currentBallIndex){
                PlayerPrefs.SetInt("BallSelected" + i, 1);
            }
            else{
                balls[i].selected = false;
                PlayerPrefs.SetInt("BallSelected" + i, 0);
            }
        }
       
        //communicate with gm
    }

    public void MainMenu(){
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        SceneManager.LoadScene("MainMenu");
    }
}
