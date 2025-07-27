using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    private AudioSource myAudio;
    SFX sfx;

    public float speed;
    [HideInInspector]public bool isFacingRight = true;
    public LayerMask groundLayer;
    public bool isGrounded;
    public Collider2D myFeetCollider;
    public float jumpSpeed;

    private bool canJump = false;

    [HideInInspector]public bool isDead;

    public float attackTime;
    bool canAttack;

    public Image weaponImage;
    public Sprite[] weaponSprites;
    public GameObject[] weapons;
    GameObject currentWeapon;
    int index = 0;

    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip weaponSound;
    public AudioClip deathSound;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sfx = FindObjectOfType<SFX>();
        weaponImage.sprite = weaponSprites[index];
        currentWeapon = weapons[index];
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
            if(!once)
            {
                once = true;
                sfx.PlayAnySound(deathSound);
            }
            anim.SetTrigger("dead");
            Invoke(nameof(RestartGame), 1f);
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        if(SceneManager.GetActiveScene().name == "Game")
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.SpawnPlayerAtCheckPoint();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
            myAudio.enabled = true;
        }
        else
        {
            anim.SetBool("isWalking", false);
            myAudio.enabled = false;
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
                sfx.PlayAnySound(jumpSound);
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

    void OnFire(InputValue value)
    {
        if(value.isPressed && !canAttack)
        {
            canAttack = true;
            StartCoroutine(AttackWithTime());
        }
    }

    IEnumerator AttackWithTime()
    {
        if(canAttack)
        {
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(attackTime);
            canAttack = false;
        }
        else
        {
            yield return null;
        }
    }

    void OnSwitch(InputValue value)
    {
        if(value.isPressed)
        {
            sfx.PlayAnySound(weaponSound);
            if(index +1 == weaponSprites.Length)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            weaponImage.sprite = weaponSprites[index];
            currentWeapon = weapons[index];
        }
    }

    public void ShootProjectile()
    {
        sfx.PlayAnySound(attackSound);
        Instantiate(currentWeapon,transform.position + currentWeapon.GetComponent<Projectile>().offset, Quaternion.identity);
    }
}
