using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    
    GameManager gameManager;
    MeshRenderer meshRenderer;
    CapsuleCollider capsuleCollider;
    Animator animator;

    [SerializeField]
    ParticleSystem _particleSystem;
    

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        ChangeDiamondState(GenerateState());
        //_particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    
    void Update() {

    }

    //Creates particles on collision
    void OnTriggerEnter(Collider other) {
        gameManager.DiamondCount += gameManager.DiamondScoreValue;
        GameObject.Instantiate(_particleSystem, transform.position, Quaternion.Euler(new Vector3(-90,0,0))).Play();
        

        ChangeDiamondState(false);
        //_particleSystem.Play();
    }

    public void ChangeDiamondState(bool state) {
        /*
        GetComponent<MeshRenderer>().enabled = state;
        GetComponent<CapsuleCollider>().enabled = state;
        GetComponent<Animator>().enabled = state;
        */
        meshRenderer.enabled = state;
        capsuleCollider.enabled = state;
        animator.enabled = state;
    }


    public void SpawnCubeWithDiamond() {
        ChangeDiamondState(GenerateState());
    }

    bool GenerateState() {
        if (Random.value < gameManager.DiamondSpawnChance) {
            return true;
        }
        else return false;
    }



}
