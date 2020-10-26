using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed = 15;
    public Text score;
    public Text winText;
    public Text livesText;
    private int scoreValue = 0;
    private int lives;
    public GameObject player;
    public float hozMovement;
    public float vertMovement;
    Animator anim;
    private bool facingRight = true;
    //public bool isJumping = false;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    public bool onGround = true; 

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        lives = 3;
        livesText.text = "Lives: " + lives.ToString();
        anim = GetComponent<Animator>();
    }

    //Constantly checks this block of code for various things, such as movement.
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));  
        
    if (facingRight == false && hozMovement > 0)
        {
        Flip();
        }

    else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }
    }   
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            onGround = false;
            anim.SetInteger("State", 3);
        }

        if (Input.GetKeyUp(KeyCode.W) && onGround)

        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A) && onGround)

        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.D) && onGround)

        {
            anim.SetInteger("State", 1);
        }
        
        if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) && onGround)

        {
            anim.SetInteger("State", 0);
        }
       
        else if (onGround != true)
        {
            anim.SetInteger("State", 3);
        }

    //Checks to see if sprite should be flipped.
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        } 
    }

    //This will check for whether or not the player has interacted with an entity.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Checks for coins
        if (collision.collider.tag == "Coin")
        {
            //checks if the player is still on the first stage by checking the coin count.
            if (scoreValue < 4) {
                scoreValue += 1;
                score.text = "Score: " + scoreValue.ToString();
            //if you are on the second level, use the modified score display.
            } else {
                scoreValue += 1;
                score.text = "Score: " + (scoreValue - 5).ToString();
            }
            Destroy(collision.collider.gameObject);
            SetScoreText ();
        }
        //checks for enemies
        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            SetLivesText ();
            Destroy(collision.collider.gameObject);
        }
        //If the score is 4, warp player to new level and reset the point counter.
        if (scoreValue == 4) 
        {
        transform.position = new Vector3(42.7f, 0.0f, 0f); 
        scoreValue +=1;
        score.text = "Score: " + (scoreValue - 5).ToString();
        lives = 3;
        SetLivesText ();
        }
    }
    
    //Check if you're colliding with the ground.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {   
            onGround = true;

            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
            }
        }     
    }


    //Checks for the win condition.
    public void SetScoreText()
    {
        if (scoreValue == 9)
        {
            winText.text = "You win! Game created by Chase Rook.";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }
    }

    //Checks for life count.
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives <= 0)
        {
            Destroy(player);
            winText.text = "You lose! Game created by Chase Rook.";
        }
    }

    //Checks to see if sprite should be flipped.
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    } 
}

