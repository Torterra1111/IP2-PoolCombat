using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    Rigidbody2D rb;
    public int Attack;
    public GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        //if the tag is different from the collided object tag it runs the if statement 
        if (col.gameObject.tag != gameObject.tag)
        {
            CollisionCombatScript ballHitScript = col.gameObject.GetComponent<CollisionCombatScript>();

            if (ballHitScript != null)
            {
                //spawn hit effect sprite
                Vector2 contactPoint = col.GetContact(0).point;
                Instantiate(hitEffect, contactPoint, Quaternion.identity);

                //If they hit you. they will call the varibles of what was hit. then do the maths
                ballHitScript.hp = ballHitScript.hp - Attack;
                ballHitScript.hpAndDamageText.text = " / ";
                ballHitScript.hpAndDamageText.text = "HP: " + ballHitScript.hp.ToString() + ballHitScript.hpAndDamageText.text + "DMG: " + ballHitScript.Attack.ToString();
            }
        }
    }
}
