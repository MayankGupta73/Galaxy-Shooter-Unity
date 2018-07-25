using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private GameObject enemyExplosionPrefab;

    private UIManager uiManager;

    [SerializeField]
    private AudioClip clip;

	// Use this for initialization
	void Start () {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -6.6f)
        {
            transform.position = new Vector3(Random.Range(-7f,7f), 6.6f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if(player != null){
                player.DamagePlayer();
            }

            setExplosion();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
        else if(collision.tag == "Laser")
        {
            if(collision.transform.parent != null){
                Destroy(collision.transform.parent.gameObject);
            }
            Destroy(collision.gameObject);
            setExplosion();
            uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }

    private void setExplosion()
    {
        Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
    }
}
