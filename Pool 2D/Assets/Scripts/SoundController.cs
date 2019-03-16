using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        TeamSelectionManager PlayingMusic = gameObject.GetComponent<TeamSelectionManager>();
        GetComponent<AudioSource>().PlayOneShot(PlayingMusic.Music);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
