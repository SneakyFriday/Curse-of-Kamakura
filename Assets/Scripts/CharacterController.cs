using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    #region Variables
    public Rigidbody2D rb;
    public float jumpForce;
    public float moveSpeed = 5;
    private float moveInput;
    public float checkRadius;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxHealth = 200;
    public int currentHealth;

    public bool facingRight = true;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Animator anim;

    public healthBar playerhb;

    public int extraJump;

    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        playerhb.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        #region Controll Input + Character Flip
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        #endregion
    }

    void Update()
    {

        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        // Jumping
        if (Input.GetKeyDown("space") && isGrounded)
        {
            anim.SetTrigger("takeOf");
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        // Animator Settings
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        if (facingRight)
            GetComponent<PlayerWeapon>().attackOffset.x *= -1;
        else
            GetComponent<PlayerWeapon>().attackOffset.x *= -1;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerhb.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Player died");
        }
    }

}
