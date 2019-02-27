﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject initialState;
    public GameObject teamSelectionState;
    public GameObject mapSelectionState;

    public TeamSelectionManager teamSelectionManager;
    public GameDataScript gameDataScript;
    public GameObject gameData;

    public void PlayButton()
    {
        //activate correct buttons and UI elements set and call playerChoose()

        initialState.SetActive(false);
        teamSelectionState.SetActive(true);

        teamSelectionManager.playersChoose();
    }

    public void BackButton()
    {
        //go back and reset all gameobject and variables to their initial value

        initialState.SetActive(true);
        teamSelectionState.SetActive(false);

        teamSelectionManager.player1Turn = true;
        teamSelectionManager.player2Turn = false;

        gameDataScript.player1selection = 0;
        gameDataScript.player2selection = 0;
    }

    public void SelectMapButton()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            teamSelectionState.SetActive(false);
            mapSelectionState.SetActive(true);
        }
    }

    public void BackFromMapSelection()
    {
        teamSelectionManager.sceneToLoad = "";
        teamSelectionManager.mapSelectionText.text = "";
        for (int i = 0; i < teamSelectionManager.maps.Length; i++)
        {
            if (teamSelectionManager.maps[i].interactable == false)
            {
                teamSelectionManager.maps[i].interactable = true;
            }
        }

        mapSelectionState.SetActive(false);

        teamSelectionManager.player1Turn = true;
        teamSelectionManager.player2Turn = false;

        gameDataScript.player1selection = 0;
        gameDataScript.player2selection = 0;

        PlayButton();
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
