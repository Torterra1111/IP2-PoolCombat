using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    //Music
    public AudioClip MapMusic;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(MapMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
     
    }

}
