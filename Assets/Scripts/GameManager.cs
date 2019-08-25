using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    int playerScore = 0;
    [SerializeField]
    int diamondCount = 0;
    int highScore = 0;
    int gameCount = 0;

    public GameState currentGameState = GameState.StartMenu;

    [Header("Level Settings")]
    [SerializeField]
    float diamondSpawnChance = 0.2f;
    [SerializeField]
    int diamondScoreValue = 1;
    [SerializeField]
    float cubeFallSpeed = 20f;
    [SerializeField]
    int seed;

    [Header("Player Settings")]
    [SerializeField]
    float playerSpeed = 10f;

    
    Text scoreOnScreen;
    GameObject GameOverMenu;
    GameObject StartMenu;
    Animator GameOverMenu_Animator;
    Animator StartMenu_Animator;

    void Start() {
        LoadData();
        Seed = (int)System.DateTime.Now.Ticks;
        scoreOnScreen = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        GameOverMenu = GameObject.Find("GameOverMenu");
        StartMenu = GameObject.Find("StartMenu");
        GameOverMenu_Animator = GameOverMenu.GetComponent<Animator>();
        StartMenu_Animator = StartMenu.GetComponent<Animator>();
        LoadStartValues();
        GameObject.FindGameObjectWithTag("DiamondCount").GetComponent<Text>().text = DiamondCount.ToString();
    }

    void Update() {
        updateScore();
        if (readyToRestart && GameOverMenu_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            ReloadScene();
        }
    }

    public bool StartMenuIsVisible = true;
    public void HideStartMenu() {
        StartMenu_Animator.SetBool("GameStarted", false);
    }


    public void LoadStartValues() {
        Text test = StartMenu.transform.Find("BottomGroup/ScoreAndGameCount").GetComponent<Text>();
        test.text = "BEST SCORE: " + highScore + "\n" + "GAMES PLAYED: " + gameCount;
    }
    public void GameOver() {

        gameCount++;
        diamondCount++;
        if (playerScore > highScore) {
            highScore = playerScore;
        }
        SaveData();
        LoadData();
        GameOverMenu.transform.Find("MenuItem1/Score").GetComponent<Text>().text = playerScore.ToString();
        GameOverMenu.transform.Find("MenuItem1/BestScore").GetComponent<Text>().text = highScore.ToString();

        currentGameState = GameState.Dead;
        GameOverMenu.GetComponent<Image>().enabled = true;
        GameOverMenu_Animator.SetBool("GameOver", true);

    }

    public void SaveData() {
        GetComponent<SaveAndLoad>().SaveData();
    }

    public void LoadData() {
        GetComponent<SaveAndLoad>().LoadData();
    }

    public bool readyToRestart = false;
    public void RestartGame() {
        GameOverMenu.GetComponent<Image>().enabled = false;
        GameOverMenu_Animator.SetBool("GameOver", false);
        readyToRestart = true;
    }

    void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void updateScore() {
        scoreOnScreen.text = playerScore.ToString();
    }

    public enum GameState {
        StartMenu,
        Playing,
        Paused,
        Dead
    }
    
    public float DiamondSpawnChance {
        get {
            return diamondSpawnChance;
        }

        set {
            diamondSpawnChance = value;
        }
    }

    public float PlayerSpeed {
        get {
            return playerSpeed;
        }

        set {
            playerSpeed = value;
        }
    }

    public float CubeFallSpeed {
        get {
            return cubeFallSpeed;
        }

        set {
            cubeFallSpeed = value;
        }
    }

    public int DiamondScoreValue {
        get {
            return diamondScoreValue;
        }

        set {
            diamondScoreValue = value;
        }
    }

    public int Seed {
        get {
            return seed;
        }

        set {
            seed = value;
        }
    }

    public int PlayerScore {
        get {
            return playerScore;
        }

        set {
            playerScore = value;
        }
    }

    public int HighScore {
        get {
            return highScore;
        }

        set {
            highScore = value;
        }
    }

    public int GameCount {
        get {
            return gameCount;
        }

        set {
            gameCount = value;
        }
    }

    public int DiamondCount {
        get {
            return diamondCount;
        }

        set {
            diamondCount = value;
        }
    }
}
