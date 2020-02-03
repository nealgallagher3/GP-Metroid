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
        lifeText.text = "Health: " + lifeValue.ToString();
        endText.text = "";

    }

    
    void Update()
    {

        
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

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
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
