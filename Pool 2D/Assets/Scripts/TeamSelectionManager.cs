using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamSelectionManager : MonoBehaviour
{
    //this class manages the players' team selection by having methods attached to buttons that write the correct value in GameDataScript
    public GameObject gameData;
    public GameDataScript gameDataScript;

    public bool player1Turn;
    public bool player2Turn = false;

    public GameObject player1text;
    public GameObject player2text;

    public GameObject mapObj;
    public string sceneToLoad;
    public Text mapSelectionText;
    public Button[] maps;

    public Button[] teams;

    void Start()
    {
        sceneToLoad = "";
        mapSelectionText.text = "";
        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();
        }
    }

    void Update()
    {
        if (gameDataScript.player1selection != 0 && player1Turn)
        {
            player1Turn = false;
            player2Turn = true;
        }
    }

    public void playersChoose()
    {
        player1text.SetActive(true);
        player2text.SetActive(false);

        player1Turn = true;

        for (int i = 0; i < teams.Length; i++)
        {
            if (teams[i].interactable == false)
            {
                teams[i].interactable = true;
            }
        }
    }

    //example of method linked to button, it writes the correct int in gamedata and disables the button
    public void Samurai()
    {
        if (gameDataScript.player1selection == 0 && player1Turn)
        {
            gameDataScript.player1selection = 1;
            player1text.SetActive(false);
            player2text.SetActive(true);
        }

        if (gameDataScript.player2selection == 0 && player2Turn)
        {
            gameDataScript.player2selection = 1;
        }

        teams[0].interactable = false;
    }

    public void Knight()
    {
        if (gameDataScript.player1selection == 0 && player1Turn)
        {
            gameDataScript.player1selection = 2;
            player1text.SetActive(false);
            player2text.SetActive(true);
        }

        if (gameDataScript.player2selection == 0 && player2Turn)
        {
            gameDataScript.player2selection = 2;
        }

        teams[1].interactable = false;
    }

    public void TestScene()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            sceneToLoad = "SampleScene";
            mapSelectionText.text = " selected";
            mapSelectionText.text = sceneToLoad + mapSelectionText.text;
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i].interactable == false)
                {
                    maps[i].interactable = true;
                }
            }
            maps[0].interactable = false;
        }
    }

    public void Map1Scene()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            sceneToLoad = "Map1Scene";
            mapSelectionText.text = " selected";
            mapSelectionText.text = sceneToLoad + mapSelectionText.text;
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i].interactable == false)
                {
                    maps[i].interactable = true;
                }
            }
            maps[1].interactable = false;
        }
    }

    public void LevelDungeon()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            sceneToLoad = "LevelDungeon";
            mapSelectionText.text = " selected";
            mapSelectionText.text = sceneToLoad + mapSelectionText.text;
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i].interactable == false)
                {
                    maps[i].interactable = true;
                }
            }
            maps[2].interactable = false;
        }
    }

    public void LoadSelectedMap()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0 && sceneToLoad != "")
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }

    /*public void MapSelectionButton()
    {
        mapObj.SetActive(true);
        selectMapButton.SetActive(false);
    }*/
}
