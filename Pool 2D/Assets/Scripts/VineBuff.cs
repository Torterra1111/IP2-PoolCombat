using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the tag is different from the collided object tag it runs the if statement 

            //register reference to the first collision contact point


            //register a reference to the collided object script for simplicity and to prevents error when hitting something without tag
            CollisionCombatScript ballHitScript = col.gameObject.GetComponent<CollisionCombatScript>();

            if (ballHitScript != null)
            {
            ballHitScript.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(255, 255, 255,155);
            }
    }


    }


