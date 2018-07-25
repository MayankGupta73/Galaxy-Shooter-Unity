using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("Turn_Left", true);
            anim.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("Turn_Right", true);
            anim.SetBool("Turn_Left", false);
        }
        else
        {
            anim.SetBool("Turn_Left", false);
            anim.SetBool("Turn_Right", false);
        }
    }
}
