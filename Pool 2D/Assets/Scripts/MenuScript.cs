using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    //Game Object controllers
    public GameObject initialState;
    public GameObject teamSelectionState;
    public GameObject mapSelectionState;
    public GameObject helpMenu;
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
    //Help Menu Control
    public GameObject HScreen1;
    public GameObject HScreen2;
    public GameObject HScreen3;
    public GameObject HScreen4;
    public GameObject HScreen5;
    public GameObject HScreen6;
    public GameObject HScreen7;
    public GameObject HScreen8;
    public GameObject HScreen9;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;
    int Help = 1;
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
        helpMenu.SetActive(false);

        teamSelectionManager.player1Turn = true;
        teamSelectionManager.player2Turn = false;

        gameDataScript.player1selection = 0;
        gameDataScript.player2selection = 0;
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
    }

    public void HelpButton()
    {
        helpMenu.SetActive(true);
        initialState.SetActive(false);
        HelpControl();
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

    public void Buttonup()
    {
        Help++;
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
        HelpControl();
    }

    public void Buttondown()
    {
        Help--;
        gameObject.GetComponent<AudioSource>().PlayOneShot(ButtonClick);
        HelpControl();
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
        HelpControl();
    }

    void Update()
    {
        
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

    public void HelpControl()
    {
        if(helpMenu.activeInHierarchy == true)
        {

            if (Help == 9)
            {
                HScreen9.SetActive(true);
                HScreen8.SetActive(false);
                ArrowLeft.SetActive(false);
            }
            if (Help == 8)
            {
                HScreen9.SetActive(false);
                HScreen8.SetActive(true);
                HScreen7.SetActive(false);
                ArrowLeft.SetActive(true);
            }
            if (Help == 7)
            {
                HScreen8.SetActive(false);
                HScreen7.SetActive(true);
                HScreen6.SetActive(false);
            }
            if (Help == 6)
            {
                HScreen7.SetActive(false);
                HScreen6.SetActive(true);
                HScreen5.SetActive(false);
            }
            if (Help == 5)
            {
                HScreen6.SetActive(false);
                HScreen5.SetActive(true);
                HScreen4.SetActive(false);
            }
            if (Help == 4)
            {
                HScreen5.SetActive(false);
                HScreen4.SetActive(true);
                HScreen3.SetActive(false);
            }
            if (Help == 3)
            {
                HScreen4.SetActive(false);
                HScreen3.SetActive(true);
                HScreen2.SetActive(false);
            }
            if (Help == 2)
            {
                HScreen3.SetActive(false);
                HScreen2.SetActive(true);
                HScreen1.SetActive(false);
                ArrowRight.SetActive(true);
            }
            if (Help == 1)
            {
                HScreen1.SetActive(true);
                HScreen2.SetActive(false);
                ArrowRight.SetActive(false);
            }
            }

        }

    }

