using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    //every team has its array filled in the editor with prefabs
    public GameObject[] samurai;
    public GameObject[] knights;

    //a 6 elements array that retains the positions where the balls need to spawn
    public Transform[] spawnPoints;

    public GameController gameController;

    void Start()
    {
        gameController = gameObject.GetComponent<GameController>();
    }

    void Update()
    {
        
    }

    public void SpawnTriplet(int team)
    {
        //spawn 3  balls of the same team and player

        //samurai team spawn
        if (team == 1)
        {   
            if (gameController.player1BallsHasSpawned == false)
            {
                GameObject samuraiHeavy = Instantiate(samurai[0], spawnPoints[0].position, Quaternion.identity);
                samuraiHeavy.tag = "Player1";
            }
            else
            {
                GameObject samuraiHeavy = Instantiate(samurai[0], spawnPoints[3].position, Quaternion.identity);
                samuraiHeavy.tag = "Player2";
            }

            if (gameController.player1BallsHasSpawned == false)
            {
                GameObject samuraiMedium = Instantiate(samurai[1], spawnPoints[1].position, Quaternion.identity);
                samuraiMedium.tag = "Player1";
            }
            else
            {
                GameObject samuraiMedium = Instantiate(samurai[1], spawnPoints[4].position, Quaternion.identity);
                samuraiMedium.tag = "Player2";
            }

            if (gameController.player1BallsHasSpawned == false)
            {
                GameObject samuraiLight = Instantiate(samurai[2], spawnPoints[2].position, Quaternion.identity);
                samuraiLight.tag = "Player1";
            }
            else
            {
                GameObject samuraiLight = Instantiate(samurai[2], spawnPoints[5].position, Quaternion.identity);
                samuraiLight.tag = "Player2";
            }
        }

        //knight team spawn
        if (team == 2)
        {
            if (gameController.player1BallsHasSpawned == false)
            {
                GameObject knightHeavy = Instantiate(knights[0], spawnPoints[0].position, Quaternion.identity);
                knightHeavy.tag = "Player1";
            }
            else
            {
                GameObject knightHeavy = Instantiate(knights[0], spawnPoints[3].position, Quaternion.identity);
                knightHeavy.tag = "Player2";
            }

            if (gameController.player1BallsHasSpawned == false)
            {
                GameObject knightMedium = Instantiate(knights[1], spawnPoints[1].position, Quaternion.identity);
                knightMedium.tag = "Player1";
            }
            else
            {
                GameObject knightMedium = Instantiate(knights[1], spawnPoints[4].position, Quaternion.identity);
                knightMedium.tag = "Player2";
            }

            if (gameController.player1BallsHasSpawned == false)
            {
                GameObject knightLight = Instantiate(knights[2], spawnPoints[2].position, Quaternion.identity);
                knightLight.tag = "Player1";
            }
            else
            {
                GameObject knightLight = Instantiate(knights[2], spawnPoints[5].position, Quaternion.identity);
                knightLight.tag = "Player2";
            }
        }
    }
}
