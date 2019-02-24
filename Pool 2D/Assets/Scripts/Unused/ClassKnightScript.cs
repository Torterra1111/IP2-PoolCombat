using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassKnightScript : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void OnMouseDown()
    {
        IsActive = true;
        IsAttack = true;
    }


    void Update()
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

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "PlayerKnight")
        {

            col.gameObject.GetComponent<ClassSamuraiScript>().hp = col.gameObject.GetComponent<ClassSamuraiScript>().hp - Attack;

            if (col.gameObject.GetComponent<ClassSamuraiScript>().hp <= 0 && IsAttack == true)
            {
                Destroy(col.gameObject);
            }
            IsAttack = false;

        }
    }*/
}
