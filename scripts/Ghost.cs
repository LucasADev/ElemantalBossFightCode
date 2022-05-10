using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public ParticleSystem ps;
    bool dashing = false;
    float sqrdist;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);   
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sqrdist = (Player.position - (Vector2)transform.position).sqrMagnitude;
        if(sqrdist < 16)
        {
            dashing = true;
            
            ps.startColor = Color.red;
            rb.velocity += (Player.position - (Vector2)transform.position).normalized * 10;
        }
        else
        {
            dashing = false;
            ps.startColor = Color.green;
            rb.velocity = (Player.position - (Vector2)transform.position).normalized * 7;
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dashing)
        {
            if (collision.CompareTag("Player"))
            {

                Player player = collision.GetComponent<Player>();
                player.health -= 5;
                player.checkDeath();
                player.timeSinceLastHit = Time.time;
                Destroy(gameObject);
            }
        }
    }
}
