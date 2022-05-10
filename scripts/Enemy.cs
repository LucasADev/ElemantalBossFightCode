using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float timeSinceLastHit = 0;
    public float health = 5;
    public Vector2 externalForces;
    public bool canMove;
    public float CollisionDamage;
    public ParticleSystem ps;
    public void checkDeath()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.health -= CollisionDamage;
            player.checkDeath();
            player.externalForces = collision.transform.position - transform.position;
            player.externalForces /= player.externalForces.magnitude / CollisionDamage;
            Debug.Log(player.externalForces);
        }
    }
    public virtual void Die()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
