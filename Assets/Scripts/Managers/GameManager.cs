using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> players;

    //so it appears in inspector
    public Transform playerSpawnTransform;

    public static GameManager instance;

    //prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    public MapGenerator mapGenerator;

    private PawnSpawnPoint[] pawnSpawnPoints;

    public int mapSeed;
    public bool setSeed;

    //runs before start
    public void Awake()
    {
        if (setSeed)
        {
            Random.InitState(mapSeed);
        }

        players = new List<PlayerController>();
        //if no instance yet
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mapGenerator != null)
        {
            mapGenerator.GenerateMap();
        }
        //temp
        SpawnPlayer();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnPlayer()
    {
        pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);

        if (pawnSpawnPoints != null)
        {
            if (pawnSpawnPoints.Length > 0)
            {
                GameObject playerSpawnTransform = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].gameObject;

                //spawn player at 0
                GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);

                //spawn pawn and connect to controller
                GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.transform.position, playerSpawnTransform.transform.rotation);

                //get player controller and pawn components
                Controller newController = newPlayerObj.GetComponent<Controller>();
                Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        
                newPawnObj.AddComponent<NoiseMaker>();
                newPawn.noiseMaker = newPawnObj.GetComponent<NoiseMaker>();
                newPawn.noiseMakerVolume = 3;

                //link them
                newController.pawn = newPawn;
            }
        }
        
       
        
    }
}
