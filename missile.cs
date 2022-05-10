using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : Projectile
{
    private void Start()
    {

        Invoke("destroy", time);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 270);
        if (!collision.CompareTag("Enemey") && !collision.CompareTag("enemyBullet") && !collision.CompareTag("bullet"))
        {
            destroy();
        }
    }

    public override void destroy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3);
        foreach(Collider2D collider in colliders) 
        {
            
            if (collider.CompareTag("Player"))
            {
                Vector2 dif = collider.transform.position - transform.position;
                Player player = collider.GetComponent<Player>();
                player.health -= 10 / (.5f + dif.sqrMagnitude / 4);
                player.checkDeath();
            }

        }
        base.destroy();
    }
}
