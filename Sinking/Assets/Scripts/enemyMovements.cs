using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovements : MonoBehaviour
{
    //Variables
    public float detectionRadius = 0.9f;
    public float movementSpeed = 7;
    public bool canMove = false;
    public bool movementDirection = false;
    public bool isFollowing = false;
    private Animator myAnimator;

    public Transform playerTarget;
    private Rigidbody2D myRB;
    private CircleCollider2D detectionZone;
    private Vector2 up;
    private Vector2 down;
    private Vector2 zero;

    //Start is called before the first frame update
    void Start()
    {
        up = new Vector2(0, movementSpeed);
        down = new Vector2(0, -movementSpeed);
        zero = new Vector2(0, 0);

        playerTarget = GameObject.Find("playerSprite").transform;

        //Assign our Rigidbody Component to our Rigidbody variable in our code.
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        detectionZone = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectionZone.radius = detectionRadius;

        if (isFollowing == false)
        {
            myAnimator.SetBool("IsWalking", false);
            myRB.velocity = zero;
        }

        else if (isFollowing == true)
        {
            myAnimator.SetBool("IsWalking", true);
            Vector3 lookPos = playerTarget.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            myRB.rotation = angle;
            lookPos.Normalize();

            myRB.MovePosition(transform.position + (lookPos * movementSpeed * Time.deltaTime));
        }

        // Oscillating movement between two triggers
        if (canMove == true)
        {
            if (movementDirection == true)
            {
                myRB.velocity = up;
            }

            else if (movementDirection == false)
            {
                myRB.velocity = down;
            }
            
        }
    }

    // runs when our enemy physically collides with something.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    // runs when an object leaves our enemy's trigger volume or when our enemy leaves another trigger volume.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("playerSprite"))
        {
            isFollowing = true;
        }
    }
}   
