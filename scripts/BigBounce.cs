using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBounce : MonoBehaviour
{
    public GameObject icicle;
    private void Awake()
    {
        Destroy(gameObject, 3);
        InvokeRepeating("spawn", 1, 1);
    }
    
    void spawn()
    {
        for (int i = 0; i < 360; i += 90)
        {
            Rigidbody2D rb = Instantiate(icicle, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad)) * 10;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.health -= 3;
            player.timeSinceLastHit = Time.time;
            player.checkDeath();
        }
    }
}
