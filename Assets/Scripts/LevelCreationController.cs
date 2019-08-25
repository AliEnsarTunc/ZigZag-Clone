using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelCreationController : MonoBehaviour {

    GameObject LevelMap;
    public GameObject GameManagerObject;
    GameManager gameManager;

    [SerializeField]
    Object CubePrefab;
	public Vector3 CubeSpawnPosition;
	public List<GameObject> CubeList = null;
	//public List<GameObject> CubeListTouched = new List<GameObject>();
	public Vector3 CubeSize;
    public int LevelSize = 30;

	struct Directions {
	    public static Vector3 Left = new Vector3(1, 0, 1);
		public static Vector3 Right = new Vector3 (-1, 0, 1);
	}

	void Start () {
        gameManager = GameManagerObject.GetComponent<GameManager>();
        //Random.InitState(gameManager.Seed); // Seed system, doesn't work. Find out why?? 
        LevelMap = GameObject.Find("LevelMap");
		CubeList.Add (GameObject.Find ("FirstCube"));
		CubeSize = CubeList [0].GetComponent<Renderer> ().bounds.size;
		CubeSpawnPosition = CubeList [0].transform.position;              

        for (int i = 0; i < LevelSize; i++) { //Init level
			SpawnCube ();
        }
        

    }

	//Spawns cubes at desired location
	public void SpawnCube() {
        CubeList.Add( (GameObject) GameObject.Instantiate(CubePrefab, CubeSpawnPositionGenerator(), Quaternion.Euler(new Vector3(0, 45, 0)), LevelMap.transform));
        CubeSpawnPosition = CubeList.Last ().transform.position;

	}

    //Recycles instantiated cubes for better performance
	public void RecycleCubes(GameObject Cube) {
		CubeList.Remove (Cube);
		Cube.transform.position = CubeSpawnPositionGenerator();
        Cube.GetComponentInChildren<DiamondController>().SpawnCubeWithDiamond();
		CubeList.Add (Cube);
		//CubeListTouched.RemoveAt (0);
	
	}

	//Randomly generates the position for the cube
	Vector3 CubeSpawnPositionGenerator() {
        int[] rand = new int[2] {-1, 1};
		int direction = rand [Random.Range (0, rand.Length)];
		if (CubeSpawnPosition.x > 4) {
			direction = -1;
		}
		else if (CubeSpawnPosition.x < -7) {
			direction = +1;
		}

		CubeSpawnPosition = new Vector3 (CubeSpawnPosition.x + CubeSize.x / 2 * direction, CubeSpawnPosition.y, CubeSpawnPosition.z + CubeSize.z / 2);
		return CubeSpawnPosition;

	}



}
