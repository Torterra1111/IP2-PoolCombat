using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCombatScript : MonoBehaviour
{
    //Movement Controllers
    [SerializeField]
    private float force = 50f;
    [SerializeField]
    private float maxForce = 200f;
    [SerializeField]
    private float minForce = 50f;
    public float multiplier;
    private Vector2 direction;
    public Rigidbody2D rb;
    //bool to check ball's motion caused by the active player    
    public bool isMoving;
    //reference velocity magnitude; gets updated each frame
    public float speed; 
    //Stat controllers
    public float ballForce;
    public float hp;
    public float Attack;
    //Attack Controllers
    public bool IsActive = false;
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

    public Text hpAndDamageText;
    public Canvas canvas;
    //Spite additiosn
    public GameObject ring;
    //public GameObject CharacterRing;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameController>();
        }

        isMoving = false;
        canvas.worldCamera = Camera.main;
        hpAndDamageText.text = "HP: " + hp.ToString() + hpAndDamageText.text + "DMG: " + Attack.ToString();
    }

    void OnMouseDown()
    {
        IsActive = true;
        IsAttack = true;
        if (interactable)
        {
            ring.SetActive(true);
        }
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

                
                force = Vector3.Distance(transform.position, mousePosition);
                force = force * 6;

                if (Input.GetMouseButtonUp(0))
                {
                    if (force > maxForce)
                    {
                        force = maxForce;
                    }

                    rb.AddForce(direction * force * multiplier);
                    IsActive = false;
                    isMoving = true;
                    ring.SetActive(false);
                }

                /*
                if (Input.GetAxis("Mouse ScrollWheel") < 0 && force > minForce)
                {
                    force -= 50;
                }
                */
            }
        }

        //lock gameobject rotation
        gameObject.transform.rotation = Quaternion.identity;

        speed = rb.velocity.magnitude;
        if(speed < 0.2 && isMoving)
        {
            rb.velocity = new Vector3(0, 0, 0);
            isMoving = false;
            gameControllerScript.playerActions++;
        }

        if (speed < 0.2)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        //health check is no more inside the collision statement, now the balls can disappear whenever they reach 0 hp
        if (hp <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(Death);
            DisableBall(gameObject);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(Injure);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the tag is different from the collided object tag it runs the if statement 
        if (col.gameObject.tag != gameObject.tag && isMoving)
        {
            //register a reference to the collided object script for simplicity and to prevents error when hitting something without tag
            CollisionCombatScript ballHitScript = col.gameObject.GetComponent<CollisionCombatScript>();

            if (ballHitScript != null)
            {
                //If they hit you. they will call the varibles of what was hit. then do the maths
                ballHitScript.hp = ballHitScript.hp - Attack;
                ballHitScript.hpAndDamageText.text = " / ";
                ballHitScript.hpAndDamageText.text = "HP: " + ballHitScript.hp.ToString() + ballHitScript.hpAndDamageText.text + "DMG: " + ballHitScript.Attack.ToString();

                IsAttack = false;
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