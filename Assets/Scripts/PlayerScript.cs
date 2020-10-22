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
 
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        lives = 3;
        livesText.text = "Lives: " + lives.ToString();
    }
 
    // I had a ton of trouble here but things finally work, thank god
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetScoreText ();
        }

        if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            SetLivesText ();
            Destroy(collision.collider.gameObject);
        }
 
    }
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            
            if (Input.GetKey("escape"))
            {
            Application.Quit();
            }
        }
    }

    private void SetScoreText ()
    {
        if (scoreValue == 4)
        {
            winText.text = "You win! Game created by Chase Rook.";
        }
    
    }

      void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives <= 0)
        {
            Destroy(player);
            winText.text = "You lose! Game created by Chase Rook.";
        }
    }
}



