using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveAndLoad : MonoBehaviour {


    public GameObject GameManagerObject;
    GameManager gameManager;

    void Start() {
        gameManager = GameManagerObject.GetComponent<GameManager>();
    }
    
    void Update() {

    }

    public void SaveData() {
        gameManager = GameManagerObject.GetComponent<GameManager>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/zzSave.dat");
        SaveData data = new SaveData();

        data.highScore = gameManager.HighScore;
        data.diamondCount = gameManager.DiamondCount;
        data.totalGameCount = gameManager.GameCount;
        

        bf.Serialize(file, data);
        file.Close();
        file.Dispose();
    }

    public void LoadData() {
        if (File.Exists(Application.persistentDataPath + "/zzSave.dat")) {
            gameManager = GameManagerObject.GetComponent<GameManager>();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/zzSave.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            gameManager.HighScore = data.highScore;
            gameManager.DiamondCount = data.diamondCount;
            gameManager.GameCount = data.totalGameCount;

            file.Close();
        }
    }

}

[Serializable]
class SaveData {

    public int highScore;
    public int totalGameCount;
    public int diamondCount;

}