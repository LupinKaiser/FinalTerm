using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;
    public float thrust;
    private BoxCollider2D playerCollider;
    


    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public float maxHealth = 3;
    public float currentHealth;

    public Health healthBar;
    private Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerCollider = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);

        }
        else if (direction < 0f)
        {
            player.velocity =  new Vector2(direction * speed, player.velocity.y);
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            player.AddForce(-Vector3.right * thrust, ForceMode2D.Impulse);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround())
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround());

       if(currentHealth == 0)
        {
            SceneManager.LoadScene("Fail");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Win")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(collision.tag == "Space")
        {
            SceneManager.LoadScene("Fail");
        }
        if(collision.tag == "Enemy")
        {
            currentHealth = currentHealth - 1;
            healthBar.setHealth(currentHealth);
            FindObjectOfType<AudioManager>().Play("PlayerHit");
           
        }
        if (collision.tag == "Asteroid")
        {
            currentHealth = currentHealth - 2;
            healthBar.setHealth(currentHealth);
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            
        }
        if(collision.tag == "Void")
        {
            SceneManager.LoadScene("Fail");
        }
    }

    private bool isTouchingGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


}
