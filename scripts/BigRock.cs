using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRock : MonoBehaviour
{
    public float speed;
    public GameObject smallRock;
    private void Awake()
    {
        Invoke("kill", 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("enemyBullet"))
        {
            kill();
            CancelInvoke();
        }
    }
    void kill()
    {
        for(int i = 0; i < 360; i += 30)
        {
            Rigidbody2D rb = Instantiate(smallRock, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos((float)i * Mathf.Deg2Rad), Mathf.Sin((float)i * Mathf.Deg2Rad)) * speed;
        }
        Destroy(gameObject);
    }
}
