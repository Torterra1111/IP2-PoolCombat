using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitScript : MonoBehaviour
{
    public Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();     
    }

    void OnTriggerEnter2D (Collider2D Col)
    {
        CollisionCombatScript BallHitScript = Col.gameObject.GetComponent<CollisionCombatScript>();
        BallHitScript.hp = BallHitScript.hp + 1;
        BallHitScript.hpAndDamageText.text = " / ";
        BallHitScript.hpAndDamageText.text = "HP: " + BallHitScript.hp.ToString() + BallHitScript.hpAndDamageText.text + "DMG: " + BallHitScript.Attack.ToString();
        gameObject.SetActive(false);
    }

}
