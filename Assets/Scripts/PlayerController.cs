﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator anim;

    public float speed;
	public float jumpForce;
    public Text countText;
    public Text livesText;
    public Text winText;
    public Text loseText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

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

        anim = GetComponent<Animator>();

        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce (movement * speed);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
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
        if (collision.collider.tag == "Ground")
        {
            anim.SetBool("Ground", true);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetBool("Ground", false);
                anim.SetInteger("State", 2);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            } 
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == 4)
        {
            transform.position = new Vector2(67.1f, -8.28f);
        }

        if (count >= 8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            winText.text = "You win!";
            
        }
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

