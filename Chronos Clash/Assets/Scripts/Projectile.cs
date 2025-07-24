using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Player player;
    [SerializeField] float speed;
    Rigidbody2D rb;
    public Vector3 offset;
    [SerializeField] float scale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        if (player.isFacingRight)
        {
            rb.velocity = Vector2.right * speed * Time.fixedDeltaTime;
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            rb.velocity = Vector2.left * speed * Time.fixedDeltaTime;
            transform.localScale = new Vector3(-scale, scale, scale);
        }
        Destroy(gameObject, 1.5f);
    }
}
