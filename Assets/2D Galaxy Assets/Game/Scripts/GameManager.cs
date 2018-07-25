using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public GameObject player;

    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Update()
    {
        if(gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                gameOver = false;
                uiManager.HideTitleScreen();
            }
        }
    }
}
