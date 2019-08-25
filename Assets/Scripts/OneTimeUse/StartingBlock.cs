using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlock : MonoBehaviour {

    GameManager gameManager;
    bool fall = false;
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if (fall) {
            Fall();
            if (transform.position.y < -10) {
                Destroy(gameObject);
            }
        }
    } 

    public void OnTriggerExit() {
        fall = true;
    }

    void Fall() {
        transform.position = Vector3.MoveTowards(transform.position,
                                                 new Vector3(transform.position.x, transform.position.y - 10, transform.position.z),
                                                 gameManager.CubeFallSpeed * Time.deltaTime);
    }

}
