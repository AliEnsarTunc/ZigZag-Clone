using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameManager gameManager;
	GameObject player;
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = GameObject.FindGameObjectWithTag ("Player");
		Light spotlight = GameObject.Find ("Spotlight").GetComponent<Light> ();
		spotlight.enabled = true;
        
	}

	void Update () {
        if (gameManager.currentGameState == GameManager.GameState.Playing) {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        }

        //transform.position = new Vector3 (transform.position.x - camSpeed, transform.position.y , transform.position.z + camSpeed);

    }
}
