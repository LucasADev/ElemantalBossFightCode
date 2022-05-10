using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProj : Projectile
{
    public bool rotates;
    private void Awake()
    {
        Invoke("destroy", time);
    }
    private void Update()
    {
        
        if (rotates) 
        {

            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            basicPlayerCollision(collision);
        }
    }
}
