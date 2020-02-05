using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight = true;
    private int lifeValue = 100;

    public float speed;
    public Text lifeText;
    public Text endText;
    public GameObject death;
    public Vector2 jumpHeight;

    Animator anim;

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void gameOver()
    {
        if (lifeValue <= 0)
        {
            endText.text = "You lose!";
            death.SetActive(false);
        }
    }
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lifeText.text = "Health: " + lifeValue.ToString();
        endText.text = "";

    }

    
    void Update()
    {

        
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Buff"))
        {
            speed += 10;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            lifeValue -= 30;
            lifeText.text = "Health: " + lifeValue.ToString();
            other.gameObject.SetActive(false);
            gameOver();

        }
        if (other.gameObject.CompareTag("Load2"))
        {
            SceneManager.LoadScene("Level 2");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(jumpHeight , ForceMode2D.Impulse);
            }
        }
    }
}
