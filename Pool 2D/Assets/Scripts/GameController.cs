using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //In this script we will manage the turns and different state of the game (win, pause(?), etc)
    public GameDataScript gameDataScript;
    public GameObject gameData;

    public SpawnScript spawnScript;

    //variable used to spawn balls with the right tag, pls don't change the name it cracks me every time 
    public bool player1BallsHasSpawned = false;

    public int playerActions = 0;
    public int maxActionPerTurn;
    public bool player1BallsActive = false;
    public bool player2BallsActive = false;
    public GameObject[] player1Balls;
    public GameObject[] player2Balls;

    public Text player1TurnText;
    public Text player2TurnText;

    public int player1DeadBalls;
    public int player2DeadBalls;
    public bool matchOver;
    public Text winText;

    public enum TurnState
    {
        PLAYER1,
        PLAYER2,
        ENDTURN,
        ENDMATCH
    }

    public TurnState currentState;

    void Start()
    {
        player1DeadBalls = 0;
        player2DeadBalls = 0;
        matchOver = false;
        spawnScript = gameObject.GetComponent<SpawnScript>();
        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();
        }

        spawnScript.SpawnTriplet(gameDataScript.player1selection);
        player1BallsHasSpawned = true;
        spawnScript.SpawnTriplet(gameDataScript.player2selection);

        player1Balls = GameObject.FindGameObjectsWithTag("Player1");
        player2Balls = GameObject.FindGameObjectsWithTag("Player2");

        currentState = TurnState.PLAYER1;
    }

    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PLAYER1):

                if(!player1BallsActive)
                {
                    ActivatePlayer1Balls();
                    player1TurnText.gameObject.SetActive(true);
                    player1BallsActive = true;
                }

                if (playerActions >= maxActionPerTurn)
                {
                    playerActions = 0;
                    player1TurnText.gameObject.SetActive(false);
                    currentState = TurnState.ENDTURN;
                }

            break;

            case (TurnState.PLAYER2):

                if (!player2BallsActive)
                {
                    ActivatePlayer2Balls();
                    player2TurnText.gameObject.SetActive(true);
                    player2BallsActive = true;
                }

                if (playerActions >= maxActionPerTurn)
                {
                    playerActions = 0;
                    player2TurnText.gameObject.SetActive(false);
                    currentState = TurnState.ENDTURN;
                }

            break;

            case (TurnState.ENDTURN):

                if (player1BallsActive)
                {
                    int player1 = 1;
                    DeactivatePlayer1Balls();
                    player1BallsActive = false;
                    StartCoroutine(EndTurn(player1));                
                }

                if (player2BallsActive)
                {
                    int player2 = 2;
                    DeactivatePlayer2Balls();
                    player2BallsActive = false;
                    StartCoroutine(EndTurn(player2));
                }

            break;
        }

        if (player1DeadBalls >= 3 || player2DeadBalls >= 3)
        {
            matchOver = true;
        }
    }

    void ActivatePlayer1Balls()
    {
        for(int i = 0; i < player1Balls.Length; i++)
        {
            CollisionCombatScript collisionScriptToCheck = player1Balls[i].GetComponent<CollisionCombatScript>();
            if (!collisionScriptToCheck.interactable)
            {
                collisionScriptToCheck.interactable = true;
                collisionScriptToCheck.IsActive = false;
            }
        }
    }

    void DeactivatePlayer1Balls()
    {
        for (int i = 0; i < player1Balls.Length; i++)
        {
            CollisionCombatScript collisionScriptToCheck = player1Balls[i].GetComponent<CollisionCombatScript>();
            if (collisionScriptToCheck.interactable)
            {
                collisionScriptToCheck.interactable = false;
                collisionScriptToCheck.IsActive = false;
            }
        }
    }

    void ActivatePlayer2Balls()
    {
        for (int i = 0; i < player2Balls.Length; i++)
        {
            CollisionCombatScript collisionScriptToCheck = player2Balls[i].GetComponent<CollisionCombatScript>();
            if (!collisionScriptToCheck.interactable)
            {
                collisionScriptToCheck.interactable = true;
                collisionScriptToCheck.IsActive = false;
            }
        }
    }

    void DeactivatePlayer2Balls()
    {
        for (int i = 0; i < player2Balls.Length; i++)
        {
            CollisionCombatScript collisionScriptToCheck = player2Balls[i].GetComponent<CollisionCombatScript>();
            if (collisionScriptToCheck.interactable)
            {
                collisionScriptToCheck.interactable = false;
                collisionScriptToCheck.IsActive = false;
            }
        }
    } 

    IEnumerator EndTurn(int lastplayer)
    {
        //we can put here the things that need to happen at the end of the turn (animations, particles..)
        yield return new WaitForSeconds(1f);
        if (!matchOver)
        {
            if (lastplayer == 1)
            {
                currentState = TurnState.PLAYER2;
            }

            if (lastplayer == 2)
            {
                currentState = TurnState.PLAYER1;
            }
        }
        else
        {
            if (player1DeadBalls >= 3)
            {
                winText.gameObject.SetActive(true);
                winText.text = "Player2" + winText.text;
            }

            if (player2DeadBalls >= 3)
            {
                winText.gameObject.SetActive(true);
                winText.text = "Player1" + winText.text;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}
