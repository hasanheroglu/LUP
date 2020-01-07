using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallTrigger : MonoBehaviour {


    private GameManager gm;
    private LevelManager lm;
    private GameObject plusTen;
    private AudioManager audioManager;
    private AudioSource audioSource;
    private PlayerController player;

    void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        lm = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeathBox")
        {
            lm.dead = true;
        }
        if (other.tag == "Cube")
        {
            audioSource.Play();
            if (!other.GetComponent<CubeBehaviour>().lit)
            {
                other.GetComponent<CubeBehaviour>().lit = true;
                lm.lightPoints += 10;
                lm.lightenedCubes++;
            }
        }
        if (other.tag == "LightCoin")
        {
            audioManager.Play("Coin");
            lm.lightPoints += 50;
            lm.lightCoins++;
            Destroy(other.gameObject);
        }
        /*
        if (other.tag == "Exit")
        {
            lm.passed = true;
        }
        */
    }
}
