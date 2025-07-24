using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string myColor;
    public float speed;
    private Rigidbody2D myRigidbody;
    public LayerMask groundLayer;
    public Transform detector;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(detector.position,Vector2.down,0.7f,groundLayer);
        if(hit.point == Vector2.zero)
        {
            speed = -speed;
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = Vector2.left * speed * Time.fixedDeltaTime;
    }

}
