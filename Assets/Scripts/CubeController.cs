using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CubeController : MonoBehaviour {

    GameManager gameManager;
	LevelCreationController LCC;
    GameObject followCam;
    
	float cubeSafeZoneDistance;

	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		LCC = GameObject.Find ("LevelController").GetComponent<LevelCreationController> ();
		followCam = GameObject.Find ("FollowCam");

	}
	
	// Update is called once per frame
	void Update ()
	{
		cubeSafeZoneDistance = followCam.transform.position.z - LCC.CubeSize.z * 4f;

		if (transform.position.z < cubeSafeZoneDistance) {		
			//if (_fall)
				CubeFall ();
			if (transform.position.y < -5) {//Recycles the cube if position.y reaches at desired position
				RecycleCube ();
			}
			
		}

		//Debug code
	    /*if (CubeHoldingPlayer!=null) {
	        //CubeHoldingPlayer.GetComponent<Renderer>().material.color = Color.red;
			CubeHoldingPlayer = null;
	    }*/
        
    }
    
    /*
	void OnTriggerEnter(Collider other) { 

		if (other.tag == "Player") {
		    //CubeHoldingPlayer = this.gameObject;
		    //Debug.Log("Player collided with trigger");
			//LCC.SpawnCube ();
			//LCC.CubeListTouched.Add (this.gameObject);
		}

	}
    */

    /*
	public void OnTriggerExit() {
	    //Debug.Log("Trigger Exit");
        //if (touched) {
        //    DestroyCube();
        //}
        //GetComponent<Renderer>().material.color = Color.blue;
	    _fall = true;

	    //if (LCC.CubeList[0].GetComponent<CubeController>().touched)
	    //{
	    //    DestroyCube(LCC.CubeList[0]);
	    //}
	}
    */

    //Calls the Level creation controller recycler(Seperated from main code for clean up)
	public void RecycleCube() {

        //GetComponent<Renderer>().material.color = Color.white;
		LCC.RecycleCubes (this.gameObject);

	}
  
    //Called when cube leaves the "safe zone" to move cube towards the void
	public void CubeFall() {

		transform.position = Vector3.MoveTowards (transform.position,
			new Vector3 (transform.position.x, transform.position.y - 10, transform.position.z),
			gameManager.CubeFallSpeed * Time.deltaTime);
		
		if (transform.position.y < -10) {
			//DestroyCube(this.gameObject);
			//LCC.RecycleCube ();
			//_fall = false;
		}
	}


	void OnDestroy() {
	    LCC.CubeList.Remove(this.gameObject);
	}



}