using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonLeaf : Enemy
{
    public float speed = 6;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        externalForces *= .98f;
        rb.velocity += externalForces;
        if (canMove)
        {
            transform.localScale = new Vector2(transform.localScale.x, 1 + Mathf.Sin(Time.time * 4) / 10);
            rb.velocity = Player.position - (Vector2)transform.position;
            rb.velocity /= rb.velocity.magnitude / speed;
            
        }
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (!player.inveinceble)
            {
                player.health -= CollisionDamage;
                player.timeSinceLastHit = Time.time;
                player.checkDeath();
                canMove = false;
                Invoke("canMoveNow", 1);
                rb.velocity = new Vector2(0, 0);
            }
           
       
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (!player.inveinceble)
            {
                player.health -= CollisionDamage;
                player.timeSinceLastHit = Time.time;
                player.checkDeath();
                canMove = false;
                Invoke("canMoveNow", 1);
                rb.velocity = new Vector2(0, 0);
            }
            

        }
    }
    void canMoveNow()
    {
        canMove = true;
    }
}
