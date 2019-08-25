using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
// ReSharper disable All

	
public class PlayerController : MonoBehaviour {

    GameManager gameManager;

    Rigidbody rb;
    Vector3 _direction;

    //Defining custom directions with struct
    struct Directions {
	    public static Vector3 Zero = Vector3.zero;
		public static Vector3 Left = new Vector3 (1, 0, 1);
		public static Vector3 Right = new Vector3 (-1, 0, 1);
	}

	void Start () {
        rb = GetComponentInChildren<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	    _direction = Directions.Zero;

	}

    void Update() {
		if (transform.position.y > -25) {
			Move ();
		}
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);       
        }
    }


    void Move() {

        float trueSpeed = gameManager.PlayerSpeed * Time.deltaTime;


        //Check if sphere touches the ground, if true control direction and movement
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 10)) {
            if (!EventSystem.current.IsPointerOverGameObject()) { // UI elementlerinin üzerine tıklanmamışsa
                if (Input.GetMouseButtonDown(0)) {
                    if (_direction == Directions.Left) {
                        _direction = Directions.Right;
                    }
                    else {
                        _direction = Directions.Left;
                    }
                }
            }
            transform.position = transform.position + trueSpeed * _direction;
            
            //transform.Rotate(_direction);

        }
        //Sphere falls to the void
        else {
            transform.position = transform.position + trueSpeed * _direction;
            if (!rb.useGravity) {
                rb.useGravity = true;
            }
            rb.AddForce(Physics.gravity * rb.mass);

            /*transform.position = new Vector3(transform.position.x,
                transform.position.y - 20f * Time.deltaTime,
                transform.position.z);*/
        }

        //Rigidbody control system || obselete
        /*if (Input.GetMouseButtonDown (0)) {
            if (_direction == Directions.Left) {
                _direction = Directions.Right;
            } 
            else {
                _direction = Directions.Left;
            }
        }

        _rb.velocity = new Vector3 (_direction.x * Speed, _rb.velocity.y, _direction.z * Speed);
        */
    }
}