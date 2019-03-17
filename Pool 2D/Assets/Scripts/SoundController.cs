using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject MusicData;
    public GameDataScript MusicDataScript;

    // Start is called before the first frame update
    void Start()
    {
        MusicData = GameObject.Find("MenuObj");
        if (MusicData != null)
        {
            MusicDataScript = MusicData.GetComponent<GameDataScript>();
        }

        //GetComponent<AudioSource>().PlayOneShot(MusicDataScript.);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
     
    }

}
