using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

    // Use this for initialization
    ParticleSystem _particleSystem;
	void Start () {
        _particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, _particleSystem.main.duration);
		
	}
	
	// Update is called once per frame
	void Update () {
    }
}
