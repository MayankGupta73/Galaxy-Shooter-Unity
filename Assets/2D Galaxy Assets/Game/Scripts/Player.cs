using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int maxLives = 3;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject shieldGameObject;

    [SerializeField]
    private GameObject[] engines;

    [SerializeField]
    private float fireRate = 0.25f;

    private float nextFire = 0.0f;

    public bool canTripleShot = false;
    public bool canSpeed = false;
    public bool shieldsActive = false;

    [SerializeField]
    private GameObject playerExplosionPrefab;

    private UIManager uiManager;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(0, 0, 0);

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (uiManager != null)
        {
            uiManager.UpdateLives(lives);
        }

        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(spawnManager != null)
        {
            spawnManager.StartSpawnRoutine();
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
    }

    private void Shoot()
    {
        if (Time.time > nextFire)
        {
            audioSource.Play();
            nextFire = nextFire + fireRate;
            if (canTripleShot) Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            else Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            
        }
    }

    private void Movement()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (canSpeed)
        {
            transform.Translate(Vector3.right * speed * 2f * inputHorizontal * Time.deltaTime);
            transform.Translate(Vector3.up * speed * 2f * inputVertical * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * inputHorizontal * Time.deltaTime);
            transform.Translate(Vector3.up * speed * inputVertical * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 8.15f)
        {
            transform.position = new Vector3(8.15f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.15f)
        {
            transform.position = new Vector3(-8.15f, transform.position.y, 0);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedPowerupOn()
    {
        canSpeed = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeed = false;
    }

    public void EnableShields()
    {
        shieldsActive = true;
        shieldGameObject.SetActive(true);
    }

    public void DamagePlayer()
    {
        if (shieldsActive)
        {
            shieldsActive = false;
            shieldGameObject.SetActive(false);
        }
        else
        {
            lives--;
            uiManager.UpdateLives(lives);
            if (lives <= 0)
            {
                Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
                gameManager.gameOver = true;
                uiManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }

            int randomEngine = Random.Range(0, 2);
            if(engines[randomEngine].activeSelf == false)
            {
                engines[randomEngine].SetActive(true);
            }
            else
            {
                engines[1 - randomEngine].SetActive(true);
            }
        }
    }
}
 