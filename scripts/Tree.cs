using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    bool active = false;
    public SpriteRenderer sr;
    private void Start()
    {
        transform.localScale = new Vector2(1, 0);
        Destroy(gameObject, 3);
        Invoke("act", 1);
    }
    void act()
    {
        active = true;
        Invoke("fade", 1);
    }
    void fade()
    {
        StartCoroutine("fadeOut");

    }
    public IEnumerator fadeOut()
    {
        for (float i = 1; i >= -.1f; i -= .05f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
    private void Update()
    {
        if (active)
        {
            transform.localScale += new Vector3(0, 1 - transform.localScale.y) * Time.deltaTime * 15;
           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.health -= 3;
            player.checkDeath();
            player.timeSinceLastHit = Time.time;

        }
    }

}
