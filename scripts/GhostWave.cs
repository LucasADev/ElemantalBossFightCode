using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWave : Projectile
{
    Rigidbody2D rb;
    public SpriteRenderer sr;
    public float fadeSpeed = 1;
    public float ScaleSpeed = 1;
    float a = 1;
    public float s = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 180);
        s += ScaleSpeed * Time.deltaTime;
        a -= fadeSpeed * Time.deltaTime;

        if(a <= 0)
        {
            Destroy(gameObject);
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
        transform.localScale = new Vector2(s, s);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            basicPlayerCollision(collision);
        }
        
        
    }
}
