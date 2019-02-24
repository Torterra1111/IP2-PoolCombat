using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject loadSceneButton;
    public GameObject backButton;

    public TeamSelectionManager teamSelectionManager;
    public GameDataScript gameDataScript;
    public GameObject gameData;

    public void ChooseFaction()
    {
        //activate buttons and call playerChoose()
        playButton.SetActive(false);
        quitButton.SetActive(false);
        loadSceneButton.SetActive(true);
        backButton.SetActive(true);
        teamSelectionManager.playersChoose();
    }

    public void Back()
    {
        //go back and reset all gameobject and variables to their initial value
        playButton.SetActive(true);
        quitButton.SetActive(true);
        loadSceneButton.SetActive(false);
        backButton.SetActive(false);

        teamSelectionManager.player1Turn = true;
        teamSelectionManager.player2Turn = false;

        gameDataScript.player1selection = 0;
        gameDataScript.player2selection = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        //find and get the gamedata instance at runtime
        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();
        }
    }

    void Update()
    {
        
    }
}
