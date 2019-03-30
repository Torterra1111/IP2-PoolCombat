using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public int player1selection = 0;
    public int player2selection = 0;
    public string SceneLoaded;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
