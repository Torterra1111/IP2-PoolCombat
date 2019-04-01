using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    //Game Object controllers
    public GameObject initialState;
    public GameObject teamSelectionState;
    public GameObject mapSelectionState;
    //Game Data Controllers
    public TeamSelectionManager teamSelectionManager;
    public GameDataScript gameDataScript;
    public GameObject gameData;
    //Sound Controllers for Menu
    public AudioClip ButtonClick;
    //Class Desc
    public GameObject KnightDesc;
    public GameObject SamuraiDesc;
    public GameObject SpartansDesc;
    public GameObject LewdDesc;

    public void PlayButton()
    {
        //activate correct buttons and UI elements set and call playerChoose()

        initialState.SetActive(false);
        teamSelectionState.SetActive(true);

        teamSelectionManager.playersChoose();
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
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
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
    }

    public void SelectMapButton()
    {
        if (gameDataScript.player1selection != 0 && gameDataScript.player2selection != 0)
        {
            teamSelectionState.SetActive(false);
            mapSelectionState.SetActive(true);
            gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
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
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
    }

    public void QuitGame()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
        Application.Quit();
    }

    void Start()
    {
        //find and get the gamedata instance at runtime
        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();
            gameDataScript.player1selection = 0;
            gameDataScript.player2selection = 0;
        }
        else
        {
            gameData = new GameObject("GameData");
            gameData.AddComponent<GameDataScript>();
        }
    }

    //these method are called from buttons
    public void ShowKnight()
    {
        KnightDesc.SetActive(true);
    }
    public void ShowSamurai()
    {
        SamuraiDesc.SetActive(true);
    }
    public void ShowSpartans()
    {
        SpartansDesc.SetActive(true);
    }
    public void ShowLewd()
    {
        LewdDesc.SetActive(true);
    }
    public void HideKnight()
    {
        KnightDesc.SetActive(false);
    }
    public void HideSamurai()
    {
        SamuraiDesc.SetActive(false);
    }
    public void HideSpartans()
    {
        SpartansDesc.SetActive(false);
    }
    public void HideLewd()
    {
        LewdDesc.SetActive(false);
    }

}
