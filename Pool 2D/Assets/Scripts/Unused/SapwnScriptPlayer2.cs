using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapwnScriptPlayer2 : MonoBehaviour
{
    public GameObject auberginePrefab;
    public GameObject peachPrefab;

    public GameObject gameData;
    public GameDataScript gameDataScript;

    void Start()
    {
        gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameDataScript = gameData.GetComponent<GameDataScript>();
        }

        if (gameDataScript.player2selection == 2)
        {
            GameObject aubergine = Instantiate(auberginePrefab, transform.position, Quaternion.identity);
        }

        if (gameDataScript.player2selection == 1)
        {
            GameObject peach = Instantiate(peachPrefab, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
