using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : BossProjectile
{
    Rigidbody2D rb;
    public override void Start()
    {
        base.Start();  
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        Vector2 pos = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.isDead = true;
        }
    }
}
