using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Attack;

   
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

            //If they hit you. they will call the varibles of what was hit. then do the maths
            ballHitScript.hp = ballHitScript.hp - Attack;
            ballHitScript.hpAndDamageText.text = " / ";
            ballHitScript.hpAndDamageText.text = "HP: " + ballHitScript.hp.ToString() + ballHitScript.hpAndDamageText.text + "DMG: " + ballHitScript.Attack.ToString();

            if (ballHitScript.hp <= 0)
            {
                DisableBall(col.gameObject);
            }
        }
    }

    //instead of using unity's Destroy method that removes a needed Game Object to populate playerBalls array, we disable the object's component 
    public void DisableBall(GameObject ball)
    {
        ball.GetComponent<CircleCollider2D>().enabled = false;
        ball.GetComponent<SpriteRenderer>().enabled = false;
        ball.GetComponent<CollisionCombatScript>().enabled = false;
        ball.GetComponent<Rigidbody2D>().IsSleeping();
        ball.SetActive(false);
    }

}
