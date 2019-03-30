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
    //Player Turn order
    public bool player1Turn;
    public bool player2Turn = false;
    //Turn Text
    public GameObject player1text;
    public GameObject player2text;
    //Map selection
    public GameObject mapObj;
    public string sceneToLoad;
    public Text mapSelectionText;
    public Button[] maps;
    //Button selection
    public Button[] teams;
    //Map Music (Could Be placed in an array)
    public AudioClip Menu;
    //fade animator reference
    public Animator fadeAnimator;

    void Start()
    {
        sceneToLoad = "";
        mapSelectionText.text = "";
        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();
        }
     
        GetComponent<AudioSource>().PlayOneShot(Menu);
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
            teams[0].interactable = false;
        }

        if (gameDataScript.player2selection == 0 && player2Turn)
        {
            gameDataScript.player2selection = 1;
            teams[0].interactable = false;
        }
    }

    public void Knight()
    {
        if (gameDataScript.player1selection == 0 && player1Turn)
        {
            gameDataScript.player1selection = 2;
            player1text.SetActive(false);
            player2text.SetActive(true);
            teams[1].interactable = false;
        }

        if (gameDataScript.player2selection == 0 && player2Turn)
        {
            gameDataScript.player2selection = 2;
            teams[1].interactable = false;
        }
    }

    public void Spartans()
    {
        if (gameDataScript.player1selection == 0 && player1Turn)
        {
            gameDataScript.player1selection = 3;
            player1text.SetActive(false);
            player2text.SetActive(true);
            teams[2].interactable = false;
        }

        if (gameDataScript.player2selection == 0 && player2Turn)
        {
            gameDataScript.player2selection = 3;
            teams[2].interactable = false;
        }
    }

    //methods attached to buttons to write in sceneToLoad the name of the scene to load 

    public void LevelJungle()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            sceneToLoad = "LevelJungle";
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

    public void LevelGlacier()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            sceneToLoad = "LevelGlacier";
            gameDataScript.SceneLoaded = "LevelGlacier";
            mapSelectionText.text = " selected";
            mapSelectionText.text = sceneToLoad + mapSelectionText.text;
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i].interactable == false)
                {
                    maps[i].interactable = true;
                }
            }
            maps[3].interactable = false;
        
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

    public void LevelJapanese()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            sceneToLoad = "LevelJapanese";
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
    public void LoadSelectedMap()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0 && sceneToLoad != "")
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        fadeAnimator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);       
    }

    /*public void MapSelectionButton()
    {
        mapObj.SetActive(true);
        selectMapButton.SetActive(false);
    }*/
}
