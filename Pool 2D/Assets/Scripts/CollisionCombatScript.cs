using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCombatScript : MonoBehaviour
{
    //Movement Controllers
    [SerializeField]
    private float force = 50;
    [SerializeField]
    private float maxForce = 200;
    [SerializeField]
    private float minForce = 50;
    private Vector2 direction;
    public Rigidbody2D rb;
    //Stat controllers
    public float ballForce;
    public float hp;
    public float Attack;
    //Attack Controllers
    private bool IsActive = false;
    private bool IsAttack = false;
    public bool interactable = false;
    //Game Controllers
    GameController gameControllerScript;
    GameObject gameController;
    //Events
    public delegate void PlaySound(int TakingDamage);
    public static event PlaySound EventPlaySound;
    //Sound Control
    public AudioClip Injure;
    public AudioClip Death;
   
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameController>();
        }
    }

    void OnMouseDown()
    {
        IsActive = true;
        IsAttack = true;
    }


    void Update()
    {
        if (interactable)
        {
            if (IsActive == true)
            {
                //Getting mouse position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Where the ball should go is = to the ball position
                direction = (Vector2)(transform.position - mousePosition); // direction = (Vector2)(mousePosition - transform.position) THIS IS NORMAL

                if (Input.GetMouseButtonUp(0))
                {
                    rb.AddForce(direction * force);
                    IsActive = false;
                    gameControllerScript.playerActions++;
                }

                if (Input.GetAxis("Mouse ScrollWheel") > 0 && force < maxForce)
                {
                    force += 50;
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0 && force > minForce)
                {
                    force -= 50;
                }

            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the tag is different from the collided object tag it runs the if statement 
        if (col.gameObject.tag != gameObject.tag)
        {
            //If they hit you. they will call the varibles of what was hit. then do the maths
            col.gameObject.GetComponent<CollisionCombatScript>().hp = col.gameObject.GetComponent<CollisionCombatScript>().hp - Attack;
            if (col.gameObject.GetComponent<CollisionCombatScript>().hp <= 0 && IsAttack == true)
            {
                GetComponent<AudioSource>().PlayOneShot(Death);
                DisableBall(col.gameObject);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(Injure);
                Debug.Log("Sound plays?");
            }
            IsAttack = false;

        }
    
    }

    //instead of using unity's built in Destroy method that removes a needed Game Object to populate playerBalls array, we disable the object's component 
    public void DisableBall(GameObject ball)
    {
        ball.GetComponent<CircleCollider2D>().enabled = false;
        ball.GetComponent<SpriteRenderer>().enabled = false;
        ball.GetComponent<CollisionCombatScript>().enabled = false;
        ball.GetComponent<Rigidbody2D>().IsSleeping();
    }
}
/*
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

    */