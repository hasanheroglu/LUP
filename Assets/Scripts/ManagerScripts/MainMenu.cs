using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public bool mainMenu = true;

    private TextMeshProUGUI level;
    private TextMeshProUGUI lightPoint;
    private TextMeshProUGUI lightGem;
    private GameObject optionsUI;
    private AudioManager audioManager;
    private RawImage rawImage;

    private void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        rawImage = GameObject.Find("Sound").GetComponent<RawImage>();

        if(mainMenu){
            level = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
            lightPoint = GameObject.Find("LightPoint").GetComponent<TextMeshProUGUI>();
            lightGem = GameObject.Find("LightGem").GetComponent<TextMeshProUGUI>();
        }
       
    }

    private void Update()
    {
        if(audioManager.mute){
            rawImage.color = Color.red;
        }
        else{
            rawImage.color = Color.white;
        }

        if(mainMenu){
            SetText();
        }
        
    }


    public void Play()
    {
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        if(PlayerPrefs.GetInt("LastLevel") == 0){

            SceneManager.LoadScene(1);
        }
        else{
            SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
        }
    }

    public void Menu(){
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        Application.Quit();
    }

    public void SetText(){
        level.text = "LEVEL: " + PlayerPrefs.GetInt("LastLevel").ToString();
        lightPoint.text = "LIGHTPOINTS: " + PlayerPrefs.GetInt("LightPoints").ToString();
        lightGem.text = "LIGHTGEMS: " + PlayerPrefs.GetInt("LightGems").ToString();
    }

    public void ResetStats(){
        PlayerPrefs.DeleteAll();
    }

    public void Customization(){
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        SceneManager.LoadScene("Customization");
    }

    public void MuteSound()
    {
        AudioManager audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.Play("Button");
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.mute = !audioManager.mute;
    }

    public void Shop(){
        SceneManager.LoadScene("Shop");
    }
}
