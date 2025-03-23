using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public List<PlayerController> players;

    //so it appears in inspector
    public Transform playerSpawnTransform;

    public static GameManager instance;

    //prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public GameObject playerPawnPrefab;

    public GameObject cameraPrefab;
    public GameObject camera1Prefab;
    public GameObject camera2Prefab;


    public MapGenerator mapGenerator;

    private PawnSpawnPoint[] pawnSpawnPoints;

    public int mapSeed;
    public bool setSeed;

    //game states
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;

    public AudioSource MenuTheme;
    public AudioSource BattleTheme;

    public int playerCount;

    public void IsSetSeed()
    {
        if (setSeed == false)
        {
            setSeed = true;
        }
        else
        {
            setSeed = false;
        }
    }

    public void GameEnd()
    {
        Debug.Log("game over");

        foreach (PlayerController pLayer in players)
        {
            pLayer.lives = 0;
            //    Destroy(pLayer.gameObject);
        }


        ActivateGameOverScreen();

       


        //mapGenerator.DestroyMap();
    }

    public void Restarted()
    {
        SceneManager.LoadScene("Main");
        BattleTheme.Stop();
        MenuTheme.Play();

        //TitleScreenStateObject = GameObject.Find("Title Screen");
        if (TitleScreenStateObject != null)
        {
            Debug.Log("we found it!");

            Debug.Log(TitleScreenStateObject.name);
        }
        //MainMenuStateObject = GameObject.Find("MainMenu");
        //OptionsScreenStateObject = GameObject.Find("Options Screen");
        //CreditsScreenStateObject = GameObject.Find("Credits");
        //GameplayStateObject = GameObject.Find("GameState");
        //GameOverScreenStateObject = GameObject.Find("Game Over Screen");
        ActivateTitleScreen();

    }


    public void ChangedMapSeed(int seeds)
    {
        Debug.Log(seeds);
        //mapSeed = int.Parse(seeds);
        mapSeed = seeds;
    }


    //runs before start
    public void Awake()
    {
        setSeed = false;
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
        //only comment out while testing outside of main scene
        ActivateTitleScreen();
        //lives = 3;

        //audioSource.PlayOneShot(menuMusic, 0.5f);
        MenuTheme.Play();

        /*if (mapGenerator != null)
        {
            //temp commented
            mapGenerator.GenerateMap();
            //audioSource.PlayOneShot(gameplayMusic, 0.5f);
        }
        //temp
        SpawnPlayer();*/        
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
                if (playerCount > 1)
                {
                    GameObject playerSpawnTransform2 = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].gameObject;
                    GameObject newPlayerObj2 = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
                    GameObject newPawnObj2 = Instantiate(tankPawnPrefab, playerSpawnTransform2.transform.position, playerSpawnTransform2.transform.rotation);
                    GameObject newCameraObj2 = null;
                    CameraPositioning cameraPos2 = newPawnObj2.GetComponent<CameraPositioning>();
                    if (cameraPos2 != null)
                    {
                        newCameraObj2 = Instantiate(camera2Prefab, cameraPos2.cameraPosition.position, cameraPos2.cameraPosition.rotation);
                    }
                    if (newCameraObj2 != null)
                    {
                        newCameraObj2.transform.parent = newPawnObj2.transform;
                    }
                    Controller newController2 = newPlayerObj2.GetComponent<Controller>();
                    Pawn newPawn2 = newPawnObj2.GetComponent<Pawn>();

                    newPawnObj2.AddComponent<NoiseMaker>();
                    newPawn2.noiseMaker = newPawnObj2.GetComponent<NoiseMaker>();
                    newPawn2.noiseMakerVolume = 3;

                    //link them
                    newController2.pawn = newPawn2;
                    newPawn2.controller = newController2;
                    newController2.playerID = 2;
                }

                GameObject playerSpawnTransform = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].gameObject;

                //spawn player at 0
                GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);

                //spawn pawn and connect to controller
                GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.transform.position, playerSpawnTransform.transform.rotation);

                GameObject newCameraObj = null;

                CameraPositioning cameraPos = newPawnObj.GetComponent<CameraPositioning>();

                if (cameraPos != null)
                {
                    if (playerCount > 1)
                    {
                        newCameraObj = Instantiate(camera1Prefab, cameraPos.cameraPosition.position, cameraPos.cameraPosition.rotation);
                    }
                    else
                    {
                        newCameraObj = Instantiate(cameraPrefab, cameraPos.cameraPosition.position, cameraPos.cameraPosition.rotation);
                    }
                }

                if (newCameraObj != null)
                {
                    newCameraObj.transform.parent = newPawnObj.transform;
                }

                //get player controller and pawn components
                Controller newController = newPlayerObj.GetComponent<Controller>();
                Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        
                newPawnObj.AddComponent<NoiseMaker>();
                newPawn.noiseMaker = newPawnObj.GetComponent<NoiseMaker>();
                newPawn.noiseMakerVolume = 3;

                //link them
                newController.pawn = newPawn;
                newPawn.controller = newController;
                newController.playerID = 1;
            }
        }
    }


    public void Respawn(Pawn pawn)
    {
            //SpawnPlayer();
        
            Controller currentController = pawn.controller;

            //similar to spawnplayer
            pawnSpawnPoints = FindObjectsByType<PawnSpawnPoint>(FindObjectsSortMode.None);
            GameObject playerSpawnTransform = pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Length)].gameObject;



            GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.transform.position, playerSpawnTransform.transform.rotation);
            Pawn newPawn = newPawnObj.GetComponent<Pawn>();
            newPawnObj.AddComponent<NoiseMaker>();


            currentController.pawn = newPawn;
            newPawn.controller = currentController;

        GameObject newCameraObj = null;

        CameraPositioning cameraPos = newPawnObj.GetComponent<CameraPositioning>();

        if (cameraPos != null)
        {
            //newCameraObj = Instantiate(cameraPrefab, cameraPos.cameraPosition.position, cameraPos.cameraPosition.rotation);
            if (cameraPos != null)
            {
                if (playerCount > 1)
                {
                    if (currentController.playerID == 1)
                    {
                        newCameraObj = Instantiate(camera1Prefab, cameraPos.cameraPosition.position, cameraPos.cameraPosition.rotation);
                    }
                    else
                    {
                        newCameraObj = Instantiate(camera2Prefab, cameraPos.cameraPosition.position, cameraPos.cameraPosition.rotation);
                    }
                }
                else
                {
                    newCameraObj = Instantiate(cameraPrefab, cameraPos.cameraPosition.position, cameraPos.cameraPosition.rotation);
                }
            }
        }

        if (newCameraObj != null)
        {
            newCameraObj.transform.parent = newPawnObj.transform;

        }


        GameObject[] allTanks = GameObject.FindGameObjectsWithTag("AiController");
            //lives --;
            
            foreach (GameObject tank in allTanks)
            {
                //GameObject.AIController.TargetNearestPlayer();
                tank.GetComponent<AIController>().TargetNearestPlayer();
            }

    }

    private void DeactivateAllStates()
    {
        //deactivate everything
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false); 
        GameplayStateObject.SetActive(false);
    }

    //the activates for everything
    public void ActivateTitleScreen()
    {
        DeactivateAllStates();
        TitleScreenStateObject.SetActive(true);
    }
    public void ActivateMainMenu()
    {
        DeactivateAllStates();
        MainMenuStateObject.SetActive(true);
    }
    public void ActivateOptionsScreen()
    {
        DeactivateAllStates();
        OptionsScreenStateObject.SetActive(true);
    }
    public void ActivateCreditsScreen()
    {
        DeactivateAllStates();
        CreditsScreenStateObject.SetActive(true);
    }
    public void ActivateGameplayState()
    {
        MenuTheme.Stop();
        BattleTheme.Play();
        DeactivateAllStates();
        GameplayStateObject.SetActive(true);

        if (mapGenerator != null)
        {
            if (setSeed)
            {
                Random.InitState(mapSeed);
            }
            //temp commented
            mapGenerator.GenerateMap();
            BattleTheme.Play();
        }
        //temp
        SpawnPlayer();
    }
    public void ActivateGameOverScreen()
    {
        DeactivateAllStates();
        GameOverScreenStateObject.SetActive(true); 
    }

}
