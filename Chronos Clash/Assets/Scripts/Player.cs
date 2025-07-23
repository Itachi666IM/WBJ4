using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator anim;

    public float speed;
    [HideInInspector]public bool isFacingRight = true;
    public LayerMask groundLayer;
    public bool isGrounded;
    public Collider2D myFeetCollider;
    public float jumpSpeed;

    private bool canJump = false;

    [HideInInspector]public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            isGrounded = myFeetCollider.IsTouchingLayers(groundLayer);
            FlipSprite();
        }
        else
        {
            anim.SetTrigger("dead");
            Invoke(nameof(RestartGame), 1f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveDirection.x * speed * Time.fixedDeltaTime,0);
        if(Mathf.Abs(playerVelocity.x) > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        rb.velocity = playerVelocity;
    }

    private void FixedUpdate()
    { 
        if(!isDead)
        {
            Walk();
            if(canJump)
            {
                rb.velocity = Vector2.up * jumpSpeed * Time.fixedDeltaTime;
                canJump = false;
            }
        }

    }

    void FlipSprite()
    {
        if(moveDirection.x<0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            isFacingRight = false;
        }
        if(moveDirection.x>0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
        }
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
}
