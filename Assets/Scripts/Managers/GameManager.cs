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


    //runs before start
    public void Awake()
    {
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
        //temp
        SpawnPlayer();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnPlayer()
    {
        
        //spawn player at 0
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);

        //spawn pawn and connect to controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        //get player controller and pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        
        //link them
        newController.pawn = newPawn;
        
    }
}
