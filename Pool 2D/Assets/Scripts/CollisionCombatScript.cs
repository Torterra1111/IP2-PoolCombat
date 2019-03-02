using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCombatScript : MonoBehaviour
{
    [SerializeField]
    private float force = 50;
    [SerializeField]
    private float maxForce = 200;
    [SerializeField]
    private float minForce = 50;

    private Vector2 direction;

    public Rigidbody2D rb;
    public float ballForce;
    public float hp;
    public float Attack;
    private bool IsActive = false;
    private bool IsAttack = false;

    public bool interactable = false;

    GameController gameControllerScript;
    GameObject gameController;

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
                direction = (Vector2)(mousePosition - transform.position);

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
            Debug.Log("AAA THINGS HURT");
            //If they hit you. they will call the varibles of what was hit. then do the maths
            col.gameObject.GetComponent<CollisionCombatScript>().hp = col.gameObject.GetComponent<CollisionCombatScript>().hp - Attack;

            if (col.gameObject.GetComponent<CollisionCombatScript>().hp <= 0 && IsAttack == true)
            {
                DisableBall(col.gameObject);
            }
            IsAttack = false;

        }

    }

    //instead of using unity's built in Destroy method that removes a needed Game Object to populate playerBalls array, we disable the object's component 
    public void DisableBall(GameObject ball)
    {
        ball.GetComponent<CircleCollider2D>().enabled = false;
        ball.GetComponent<SpriteRenderer>().enabled = false;
        ball.GetComponent<CollisionScript>().enabled = false;
        ball.GetComponent<Rigidbody2D>().IsSleeping();
    }
}
