using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
	public float jumpForce;
    public Text countText;
    public Text livesText;
    public Text winText;
    public Text loseText;


    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    private bool facingRight = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        count = 0;
		SetCountText ();
        lives = 3;
        SetLivesText();
        winText.text = "";
        loseText.text = "";
    }
	
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce (movement * speed);

        if (count == 4) 
        {
            transform.position = new Vector3(67.1f, -8.28f);
        }
		
		if (Input.GetKey("escape"))
		{
		Application.Quit();
		}
		
		if (facingRight == false && moveHorizontal > 0)
			{
				Flip();
			}
			else if (facingRight == true && moveHorizontal < 0)
			{
				Flip();
			}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector2 Scaler = transform.localScale;
		Scaler.x = Scaler.x * -1;
		transform.localScale = Scaler;
	
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText ();
		}

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
		{
			if(Input.GetKey(KeyCode.UpArrow))
			{
				rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			}
		}
		
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString ();

        if (count >= 8)
            winText.text = "You win!";
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            winText.text = "Game Over!";
            this.gameObject.SetActive(false);
        }
    }
}

