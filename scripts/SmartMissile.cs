using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMissile : MonoBehaviour
{

    public Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {
        Vector2 force = (Player.position - (Vector2)transform.position).normalized;
        rb.velocity += force;
        rb.velocity = rb.velocity.normalized * 10;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 270); 
    }
}
