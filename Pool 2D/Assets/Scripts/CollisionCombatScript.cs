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
    
    bool ballDead;
    LineRenderer lineRenderer;
    



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        lineRenderer = GetComponent<LineRenderer>();
        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameController>();
        }

        ballDead = false;
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

                //Direction is a new point to go to.
                direction = (Vector2)(transform.position - mousePosition); 


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
                direction = (Vector2)(transform.position - mousePosition); // direction = (Vector2)(mousePosition - transform.position) THIS IS NORMAL>>>>>>> 5cfeab362485c42cdfad7908bc93c5a9488f4693

                //Distance between the two
                force = Vector3.Distance(transform.position, mousePosition);
                force = force * 6;

                if (Input.GetMouseButtonUp(0))
                {
                    if (force > maxForce)
                    {
                        force = maxForce;
                    }
                    //Accual movment
                    rb.AddForce(direction * force * multiplier);
                    IsActive = false;
                    isMoving = true;

                    //deactivate ring and reset linerenderer vertices 
                    lineRenderer.SetPosition(0, Vector3.zero);
                    lineRenderer.SetPosition(1, Vector3.zero);
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
            if (gameObject.tag == "Player1" && !ballDead)
            {
                ballDead = true;
                gameControllerScript.player1DeadBalls++;
            }
            if (gameObject.tag == "Player2" && !ballDead)
            {
                ballDead = true;
                gameControllerScript.player2DeadBalls++;
            }

            StartCoroutine(DisableBall());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the tag is different from the collided object tag it runs the if statement 
        if (col.gameObject.tag != gameObject.tag && !isMoving)  //Not moving as main hits are taken when you hit a person when they are not moving.
        {
            //register a reference to the collided object script for simplicity and to prevents error when hitting something without tag
            CollisionCombatScript ballHitScript = col.gameObject.GetComponent<CollisionCombatScript>();

            if (ballHitScript != null)
            {
                //If they hit you. they will call the varibles of what was hit. then do the maths
                ballHitScript.hp = ballHitScript.hp - Attack;
                ballHitScript.hpAndDamageText.text = " / ";
                ballHitScript.hpAndDamageText.text = "HP: " + ballHitScript.hp.ToString() + ballHitScript.hpAndDamageText.text + "DMG: " + ballHitScript.Attack.ToString();
                GetComponent<AudioSource>().PlayOneShot(Injure); //Moved this here untill a more suficent way of fiixng damage sound is found as sound would play always.
                IsAttack = false;
            }
        }
    
    }

    //we can use the coroutine to do death related stuff; particles, sound etc.
    IEnumerator DisableBall()
    {
        /*ball.GetComponent<CircleCollider2D>().enabled = false;
        ball.GetComponent<SpriteRenderer>().enabled = false;
        ball.GetComponent<CollisionCombatScript>().enabled = false;
        ball.GetComponent<Rigidbody2D>().IsSleeping();*/
        yield return new WaitForSeconds(0.5f); 
        this.gameObject.SetActive(false);
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