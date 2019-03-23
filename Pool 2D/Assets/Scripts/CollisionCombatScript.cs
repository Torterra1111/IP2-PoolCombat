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
    public float timeFromMovement;
    public bool ballHasCollided;
    //reference velocity magnitude; gets updated each frame
    public float speed; 
    //Stat controllers
    public float ballForce;
    public int hp;
    public int Attack;
    public int Armour;
    bool ballDead;
    //Attack Controllers
    public bool IsActive = false;
    private bool IsAttack = false;
    public bool interactable = false;
    public bool samuraiAbility = false;
    bool samuraiAbilityActive = false;

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
    LineRenderer lineRenderer;
    LineRenderer Liners;

    Vector3 test;

 

    void Start()
    {
        timeFromMovement = 0.0f;
        ballHasCollided = false;
        samuraiAbilityActive = false;
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        lineRenderer = GetComponent<LineRenderer>();
        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameController>();
        }

        ballDead = false;
        isMoving = false;
        hpAndDamageText.text = "HP: " + hp.ToString() + hpAndDamageText.text + "DMG: " + Attack.ToString();
    }

    void OnMouseDown()
    {
        if (interactable && gameControllerScript.playerActions < gameControllerScript.maxActionPerTurn)
        {
            IsActive = true;
            IsAttack = true;
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

                //Direction is a new point to go to.
                direction = (Vector2)(transform.position - mousePosition);
                test = (Vector3)(transform.position - mousePosition);

                //raycast to draw the trajectory still in progress, math is simple but im dumb
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction); // layer mask 11 "Walls"

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, mousePosition);
                
                /*
                if (hit.collider != null)
                {
                    lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    lineRenderer.SetPosition(1, mousePos);
                }*/

                //Where the ball should go is = to the ball position
                // direction = (Vector2)(mousePosition - transform.position) THIS IS NORMAL>>>>>>> 


                force = Vector3.Distance(transform.position, mousePosition);
                force = force * 6;

                if (Input.GetMouseButtonUp(0))
                {
                    if (force > maxForce)
                    {
                        force = maxForce;
                    }
                    //Accual movment
                    rb.AddForce(test * force * multiplier);
                    IsActive = false;
                    isMoving = true;

                    DisableOtherBalls();

                    //deactivate ring and reset linerenderer vertices 
                    lineRenderer.SetPosition(0, Vector3.zero);
                    lineRenderer.SetPosition(1, Vector3.zero);
                    ring.SetActive(false);
                }
            }
        }

        //lock gameobject rotation
        gameObject.transform.rotation = Quaternion.identity;

        speed = rb.velocity.magnitude;

        if (isMoving)
        {
            timeFromMovement += Time.deltaTime;
            if (speed < 0.2 && timeFromMovement > 1.5f)
            {
                rb.velocity = new Vector3(0, 0, 0);
                isMoving = false;
                timeFromMovement = 0.0f;
                gameControllerScript.playerActions++;
                Debug.Log("ball stopped and playerActions++");
            }
        }

        if (speed < 0.2 && ballHasCollided)
        {
            rb.velocity = new Vector3(0, 0, 0);
            ballHasCollided = false;
        }

        if (samuraiAbility && !samuraiAbilityActive && hp < 3)
        {
            samuraiAbilityActive = true;
            SamuraiDamageBoost();
        }

        //health check is no more inside the collision statement, now the balls can disappear whenever they reach 0 hp

        if (hp <= 0 && !ballDead)
        {
            
            if (gameObject.tag == "Player1")
            {
                gameControllerScript.player1DeadBalls++;
            }
            if (gameObject.tag == "Player2")
            {
                gameControllerScript.player2DeadBalls++;
            }
            ballDead = true;
            StartCoroutine(DisableBall());
        }       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the tag is different from the collided object tag it runs the if statement 
        if (col.gameObject.tag != gameObject.tag && isMoving && !ballDead) //isMoving means that this statement will run ONLY in the ball's instance that has been moved by the player
        {
            //register a reference to the collided object script for simplicity and to prevents error when hitting something without tag
            CollisionCombatScript ballHitScript = col.gameObject.GetComponent<CollisionCombatScript>();

            if (ballHitScript != null)
            {
                //If they hit you. they will call the varibles of what was hit. then do the maths
                ballHitScript.hp = ballHitScript.hp - (Attack - ballHitScript.Armour);
                ballHitScript.hpAndDamageText.text = " / ";
                ballHitScript.hpAndDamageText.text = "HP: " + ballHitScript.hp.ToString() + ballHitScript.hpAndDamageText.text + "DMG: " + ballHitScript.Attack.ToString();
                GetComponent<AudioSource>().PlayOneShot(Injure); //Moved this here untill a more suficent way of fiixng damage sound is found as sound would play always.
                IsAttack = false;
            }
        }

        ballHasCollided = true;
    }

    //we can use the coroutine to do death related stuff; particles, sound etc.
    IEnumerator DisableBall()
    {
            GetComponent<AudioSource>().PlayOneShot(Death);
            yield return new WaitForSeconds(1.6f); //the time in seconds must be equal to the clip lenght
            this.gameObject.SetActive(false);
    }

    void SamuraiDamageBoost()
    {      
                Attack++;
                hpAndDamageText.text = "HP: " + hp.ToString() + hpAndDamageText.text + "DMG: " + Attack.ToString();
    }

    void DisableOtherBalls()
    {
        if (gameObject.tag == "Player1")
        {
            foreach (GameObject ball in gameControllerScript.player1Balls)
            {
                CollisionCombatScript collisionCombatScript = ball.GetComponent<CollisionCombatScript>();
                collisionCombatScript.interactable = false;
            }
        }

        if (gameObject.tag == "Player2")
        {
            foreach (GameObject ball in gameControllerScript.player2Balls)
            {
                CollisionCombatScript collisionCombatScript = ball.GetComponent<CollisionCombatScript>();
                collisionCombatScript.interactable = false;
            }
        }
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
