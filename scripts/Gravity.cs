using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Rigidbody2D rb;
    public static float G = 2f;
    public static List<Gravity> gravity = new List<Gravity>();
    public float mass = 1;
    private void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
        gravity.RemoveRange(0, gravity.Count);
    }
    void Start()
    {
        gravity.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
       
       
        float sqrdis = (Player.position - (Vector2)transform.position).sqrMagnitude;
        Vector2 forc = (Player.position - (Vector2)transform.position).normalized;

        forc *= G * (5 / sqrdis);
        rb.velocity += forc;

    }
}
