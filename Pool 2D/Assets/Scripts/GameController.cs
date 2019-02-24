using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //In this script we will manage the turns and different state of the game (win, pause(?), etc)
    public GameDataScript gameDataScript;
    public GameObject gameData;

    public SpawnScript spawnScript;

    //variable used to spawn balls with the right tag, pls don't change the name it cracks me every time 
    public bool player1BallsHasSpawned = false;

    void Start()
    {
        spawnScript = gameObject.GetComponent<SpawnScript>();

        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();

            spawnScript.SpawnTriplet(gameDataScript.player1selection);
            player1BallsHasSpawned = true;
            spawnScript.SpawnTriplet(gameDataScript.player2selection);
        }
    }

    void Update()
    {
        
    }
}
