using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Injure;
    public AudioClip Death;


    void PlaySound(int TakingDamage)
    {
        if (TakingDamage == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(Injure);
        }
        else if (TakingDamage == 2)
        {
            GetComponent<AudioSource>().PlayOneShot(Death);
        }
    }
    void OnEnable()
    {
        CollisionCombatScript.EventPlaySound += PlaySound;
    }
    // When game object is disabled
    void OnDisable()
    {
        CollisionCombatScript.EventPlaySound -= PlaySound;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
