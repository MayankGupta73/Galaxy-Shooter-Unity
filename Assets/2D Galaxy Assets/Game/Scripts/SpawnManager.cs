using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerups;

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    public void StartSpawnRoutine()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //enemy coroutine
    IEnumerator EnemySpawnRoutine()
    {
        while (!gameManager.gameOver)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    //powerup coroutine
    IEnumerator PowerupSpawnRoutine()
    {
        while (!gameManager.gameOver)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }
}
