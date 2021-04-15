using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    public float speed = 3f;
    public int playerHealth = 3;
    public float bulletLifespan = 1;
    public float bulletspeed = 6;
    public AudioClip shootSoundEffect;
    public AudioClip pickupSoundEffect;
    public AudioClip hitSoundEffect;
    public AudioClip deathSoundEffect;
    public GameObject bullet;
    private AudioSource speaker;
    private Animator myAnimator;
    private Vector2 zero;
    private SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerHealth = 3;
        speaker = GetComponent<AudioSource>();
        zero = new Vector2(0, 0);
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0)
        {
            //new quaternion is 4 values
            speaker.clip = deathSoundEffect;
            speaker.Play();

            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
            playerHealth = 3;
        }

        Vector2 velocity = myRB.velocity;
        //controllers
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;

        myRB.velocity = velocity;

        //directional animations
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnimator.SetBool("WalkingUp", true);
            myAnimator.SetBool("WalkingSide", false);
            myAnimator.SetBool("WalkingDown", false);
            myAnimator.SetBool("Idle", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            myAnimator.SetBool("WalkingSide", true);
            myAnimator.SetBool("WalkingDown", false);
            myAnimator.SetBool("WalkingUp", false);
            myAnimator.SetBool("Idle", false);
            myRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            myAnimator.SetBool("WalkingSide", true);
            myAnimator.SetBool("WalkingDown", false);
            myAnimator.SetBool("WalkingUp", false);
            myAnimator.SetBool("Idle", false);
            myRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            myAnimator.SetBool("WalkingDown", true);
            myAnimator.SetBool("WalkingSide", false);
            myAnimator.SetBool("WalkingUp", false);
            myAnimator.SetBool("Idle", false);
        }

        if (myRB.velocity == zero)
        {
            myAnimator.SetBool("WalkingDown", false);
            myAnimator.SetBool("WalkingSide", false);
            myAnimator.SetBool("WalkingUp", false);
            myAnimator.SetBool("Idle", true);
        }

        //bullets
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletspeed);
            speaker.clip = shootSoundEffect;
            speaker.Play();


            Destroy(b, bulletLifespan);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletspeed);

            speaker.clip = shootSoundEffect;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletspeed, 0);

            speaker.clip = shootSoundEffect;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject b = Instantiate(bullet, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletspeed, 0);

            speaker.clip = shootSoundEffect;
            speaker.Play();

            Destroy(b, bulletLifespan);
        }
    }

    //a collision event
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("enemy"))
        {
            playerHealth--;
            speaker.clip = hitSoundEffect;
            speaker.Play();
        }

        else if ((collision.gameObject.name.Contains("pickup")) && (playerHealth < 3))
        {
            playerHealth++;
            collision.gameObject.SetActive(false);
            speaker.clip = pickupSoundEffect;
            speaker.Play();
        }
    }
}
