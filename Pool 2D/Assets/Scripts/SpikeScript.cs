using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{


    public Rigidbody2D rb;
    public float ballForce;
    public float hp;
    public float Attack;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {



       
    }


    void OnCollisionEnter2D(Collision2D dol)
    {

        Debug.Log("AAA THINGS HURT");
        //if the tag is different from the collided object tag it runs the if statement 
        if (dol.gameObject.tag != gameObject.tag)
        {

            //If they hit you. they will call the varibles of what was hit. then do the maths
            dol.gameObject.GetComponent<CollisionCombatScript>().hp = dol.gameObject.GetComponent<CollisionCombatScript>().hp - Attack;

            if (dol.gameObject.GetComponent<CollisionCombatScript>().hp <= 0)
            {
                Destroy(dol.gameObject);
            }


        }

    }

}
