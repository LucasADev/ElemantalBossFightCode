using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linePrediction : MonoBehaviour
{
    LineRenderer lr;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + rb.velocity * 5);
        Invoke("stopShow", 1);
    }
    void stopShow()
    {
        Destroy(lr);
    }
   
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
