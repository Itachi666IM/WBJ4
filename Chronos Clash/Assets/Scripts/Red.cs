using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : BossProjectile
{
    Rigidbody2D rb;
    Boss boss;
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<Boss>();
    }

    private void Update()
    {
        Vector2 pos = Vector2.MoveTowards(rb.position, boss.redOrbTarget.position, speed * Time.fixedDeltaTime);
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
