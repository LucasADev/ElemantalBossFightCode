using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFade : Projectile
{
    public float fadeSpeed = .95f;
    private void Update()
    {
        rb.velocity *= fadeSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            basicPlayerCollision(collision);
        }
    }
}

