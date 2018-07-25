using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private int powerupID;   //0 - Triple Shot 1 - Speed 2 - Shields

    [SerializeField]
    private AudioClip clip;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                if(powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if(powerupID == 1)
                {
                    player.SpeedPowerupOn();
                }
                else if(powerupID == 2)
                {
                    player.EnableShields();
                }
            }

            Destroy(this.gameObject);
        }
    }
}
