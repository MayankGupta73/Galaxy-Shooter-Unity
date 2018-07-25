using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public GameObject titleImage;

    public int score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleImage.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleImage.SetActive(false);
        scoreText.text = "Score: 0";
        score = 0;
    }
}
