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
    private float maxForce = 150f;
    [SerializeField]
    private float minForce = 50f;

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

    //abilities activators
    public bool samuraiAbility = false;
    public bool spartansAbility = false;
    bool samuraiAbilityActive = false;
    bool spartansAbilityActive = false;

    //Game Controllers
    GameController gameControllerScript;
    GameObject gameController;

    GameDataScript SelectionMangerScript;
    GameObject TeamSelection;
    
    CameraController CameraContolScript;
    GameObject CameraControl;
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
    //ColourControllers
    public float ColourDuration = 0f;
    public float ColourDecrease = 155f;

    public float RedDuration = 300f;
    public float RedDecrease = 130f;

    Vector3 test;

    public GameObject hitEffect;
    public GameObject floatingDamagePrefab;

    void Start()
    {
        timeFromMovement = 0.0f;
        ballHasCollided = false;
        samuraiAbilityActive = false;
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        TeamSelection = GameObject.Find("GameData");
        CameraControl = GameObject.FindGameObjectWithTag("MainCamera");
        CameraContolScript = CameraControl.GetComponent<CameraController>();
        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameController>();
        }
        if (TeamSelection != null)
        {
            SelectionMangerScript = TeamSelection.GetComponent<GameDataScript>();
        }
        if (CameraControl != null)
        {
            CameraContolScript = CameraControl.GetComponent<CameraController>();
        }
        ballDead = false;
        isMoving = false;
        hpAndDamageText.text = "HP: " + hp.ToString() + hpAndDamageText.text + "DMG: " + Attack.ToString();
        if (SelectionMangerScript.SceneLoaded == "LevelGlacier")
        {
            rb.drag = 0.75f;
        }
        else
        {
            rb.drag = 0.95f;
        }
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

                if (force > maxForce)
                {
                    force = maxForce;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    //Accual movment
                    rb.AddForce(test * force);
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
        transform.rotation = Quaternion.identity;


        speed = rb.velocity.magnitude;

        if (isMoving)
        {
            //Make it look where its moving
            //gameObject.transform.rotation = localDirection;
            timeFromMovement += Time.deltaTime;
            if (speed < 0.2 && timeFromMovement > 1.5f)
            {
                rb.velocity = new Vector3(0, 0, 0);
                isMoving = false;
                timeFromMovement = 0.0f;
                gameControllerScript.playerActions++;
                Debug.Log("ball stopped and playerActions++");
                gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }

        if (speed < 0.2 && ballHasCollided)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            ballHasCollided = false;
        }

        if (samuraiAbility && !samuraiAbilityActive && hp < 3)
        {
            samuraiAbilityActive = true;
            SamuraiDamageBoost();
        }

        if (spartansAbility && !spartansAbilityActive)
        {
            if (gameObject.tag == "Player1" && gameControllerScript.player1DeadBalls >= 2)
            {
                spartansAbilityActive = true;
                SpartansDamageBoost();
            }

            if (gameObject.tag == "Player2" && gameControllerScript.player2DeadBalls >= 2)
            {
                spartansAbilityActive = true;
                SpartansDamageBoost();
            }
        }

        //health check is no more inside the collision statement, now the balls can disappear whenever they reach 0 hp

        if (hp <= 0 && !ballDead)
        {
            ColourDuration = 255f;
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

        //Thanos Snap
        if (ColourDuration > 0f)
        {
            Debug.Log(ColourDuration);
            byte L = System.Convert.ToByte(ColourDuration);
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(255, 255, 255, L);
            ColourDuration -= Time.deltaTime * ColourDecrease; //Decreases the time of shaking

        }
        else if (ballDead) //If the shake duration is less than 0 (it has ended)
        {
            ColourDuration = 0f; //Makes sure it has ended)
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(255, 255, 255, 0);
        }
        
        //Red oof
        if (RedDuration < 254)
        {
            Debug.Log(RedDuration);
            byte L = System.Convert.ToByte(RedDuration);
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(255, L, L,255);
            RedDuration += Time.deltaTime * RedDecrease; //Decreases the time of shaking
        }
        else if (!ballDead)
        {
            RedDuration = 300f; //Makes sure it has ended)
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(255, 255, 255, 255);
        }
        
        

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the tag is different from the collided object tag it runs the if statement 
        if (col.gameObject.tag != gameObject.tag && isMoving && !ballDead) //isMoving means that this statement will run ONLY in the ball's instance that has been moved by the player
        {
            //register reference to the first collision contact point
            Vector2 contactPoint = col.GetContact(0).point;

            //register a reference to the collided object script for simplicity and to prevents error when hitting something without tag
            CollisionCombatScript ballHitScript = col.gameObject.GetComponent<CollisionCombatScript>();

            if (ballHitScript != null)
            {
                Instantiate(hitEffect, contactPoint, Quaternion.identity);
                GameObject floatingDamageObj = Instantiate(floatingDamagePrefab, transform.position, Quaternion.identity);
                floatingDamageObj.GetComponent<FloatingDamageController>().SetText((Attack - ballHitScript.Armour).ToString());

                CameraContolScript.shakeDuration = 0.1f;
                ballHitScript.hp = ballHitScript.hp - (Attack - ballHitScript.Armour);
                ballHitScript.hpAndDamageText.text = " / ";
                ballHitScript.hpAndDamageText.text = "HP: " + ballHitScript.hp.ToString() + ballHitScript.hpAndDamageText.text + "DMG: " + ballHitScript.Attack.ToString();
                GetComponent<AudioSource>().PlayOneShot(Injure); 
                IsAttack = false;
                if(ballHitScript.hp > 0)ballHitScript.RedDuration = 0f;
                
                //ballHitScript.gameObject.GetComponent<SpriteRenderer>().material.color = Color.Lerp(Color.red, new Color(1, 1, 1, 1), Mathf.PingPong(Time.time, 1));
            }
        }

        ballHasCollided = true;
    }

    //we can use the coroutine to do death related stuff; particles, sound etc.
    IEnumerator DisableBall()
    {
        GetComponent<AudioSource>().PlayOneShot(Death);
        rb.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(2f); //the time in seconds must be equal to the clip lenght
        this.gameObject.SetActive(false);
    }
    void SamuraiDamageBoost()
    {      
        Attack++;
        hpAndDamageText.text = " / ";
        hpAndDamageText.text = "HP: " + hp.ToString() + hpAndDamageText.text + "DMG: " + Attack.ToString();
    }

    void SpartansDamageBoost()
    {
        Attack += 3;
        hpAndDamageText.text = " / ";
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
             while (t > 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, (t));
                t -= 1.0000000001f * Time.deltaTime;
            Debug.Log(t);
            } 
*/
