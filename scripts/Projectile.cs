using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public float damage;
    public float kb;
    public float time;
   
    private void Start()
    {
      
        Invoke("destroy", time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemey"))
        {
            basicEnemeyCollision(collision);
        }
       
    }
    public void basicPlayerCollision(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        player.health -= damage;
        player.externalForces += rb.velocity / 10 * kb;
        player.timeSinceLastHit = Time.time;
        player.checkDeath();
        destroy();
    }
    public void basicEnemeyCollision(Collider2D collision)
    {
        Enemy enemey = collision.GetComponent<Enemy>();
        enemey.externalForces += rb.velocity / (rb.velocity.magnitude / kb);
        enemey.timeSinceLastHit = Time.time;
        enemey.health -= damage;
        enemey.checkDeath();
        destroy();
    }
    public virtual void destroy()
    {
        if(ps != null)
        {
            Instantiate(ps, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
