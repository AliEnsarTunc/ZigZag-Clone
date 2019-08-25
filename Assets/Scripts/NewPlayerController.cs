using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class NewPlayerController : MonoBehaviour {

    public GameObject GameManagerObject;
    GameManager gameManager;
    GameObject PlayerMesh;
    Rigidbody MeshRigidBody;

    Vector3 currentDirection;
    
    //Custom directions for diogonal movement
    struct CustomDirection {
        public static Vector3 Zero = Vector3.zero;
        public static Vector3 Left = new Vector3(-1, 0, 1);
        public static Vector3 Right = new Vector3(1, 0, 1);
    }
    
    void Start () {
        gameManager = GameManagerObject.GetComponent<GameManager>();
        PlayerMesh = transform.Find("PlayerMesh").gameObject;
        MeshRigidBody = PlayerMesh.GetComponent<Rigidbody>();

        currentDirection = CustomDirection.Zero;
	}

    void Update() {

        ApplyMovement(currentDirection, gameManager.PlayerSpeed); 

        if (PlayerIsOnGround()) {
            isClicked();
        }
        else {
            PlayerFall();
        }

        RotateMesh();
    }
    

    void RotateMesh() {
        if (currentDirection == CustomDirection.Left) {
            PlayerMesh.transform.Rotate(CustomDirection.Right * gameManager.PlayerSpeed, Space.World);
        }
        else if (currentDirection == CustomDirection.Right) {
            PlayerMesh.transform.Rotate(-CustomDirection.Left * gameManager.PlayerSpeed, Space.World);
        }

    }

    bool PlayerIsOnGround() {
        //Debug.DrawRay(PlayerMesh.transform.position, transform.TransformDirection(Vector3.down), Color.red, 10); 
        return Physics.Raycast(PlayerMesh.transform.position, transform.TransformDirection(Vector3.down), 10);
    }

    //Called when player is not on ground to force player to fall
    bool playerIsDead = false;
    void PlayerFall() {
        if (!playerIsDead) {
            if (PlayerMesh.transform.position.y < -10) {
                Die();
            }
        }
        if (!MeshRigidBody.useGravity) {
            MeshRigidBody.useGravity = true;
        }
        //ApplyMovement(currentDirection, gameManager.PlayerSpeed * .5f);

        //ApplyMovement(Vector3.down, 25);
        MeshRigidBody.AddForce(Physics.gravity * MeshRigidBody.mass);

    }

    void ApplyMovement(Vector3 direction, float speed) {
        transform.position = transform.position + ((speed * Time.deltaTime) * direction);
    }

    Vector3 GetMovementDirection() {
        if (currentDirection == CustomDirection.Right) {
            return CustomDirection.Left;
        }
        else
            return CustomDirection.Right;

    }

    void isClicked() {
        if (Input.GetMouseButtonDown(0)) {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (gameManager.StartMenuIsVisible) {
                    gameManager.StartMenuIsVisible = false;
                    gameManager.HideStartMenu();
                }
                currentDirection = GetMovementDirection();
                gameManager.PlayerScore++;

                if (gameManager.currentGameState == GameManager.GameState.StartMenu) {
                    gameManager.currentGameState = GameManager.GameState.Playing;
                }

            }
        }
    }

    void Die() {
        playerIsDead = true;
        gameManager.GameOver();
        //gameManager.currentGameState = GameManager.GameState.Dead;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
