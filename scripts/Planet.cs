using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject smallAstroid;
    public float angle = 0;
    public Vector2 PlanetDistance;
    public float Speed;
    public List<moon> moons = new List<moon>();
    private void Awake()
    {
        foreach(moon m in moons)
        {
            m.Moon = Instantiate(m.Moon);
        }
    }
    private void Update()
    {
        foreach(moon moon in moons)
        {
            if(moon.Moon != null)
            {
                moon.angle += moon.Speed * Time.deltaTime;
                moon.Moon.transform.position = new Vector2(Mathf.Cos(moon.angle) * moon.PlanetDistance.x, Mathf.Sin(moon.angle) * moon.PlanetDistance.y) + (Vector2)transform.position;
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemey") && !collision.CompareTag("enemyBullet") && !collision.CompareTag("bullet")) 
        {
            if (collision.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                player.health -= 10;
                player.timeSinceLastHit = Time.time;
                return;
            }
            for(int i = 0; i < 360; i += 45)
            {
                GameObject gm = Instantiate(smallAstroid, transform.position, Quaternion.identity);
                Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * 10, Mathf.Sin(i * Mathf.Deg2Rad) * 10);
                gm.GetComponent<SpeedFade>().fadeSpeed = .995f;
            }
            Destroy(gameObject);
        }
    }
}
[System.Serializable]
public class moon
{
    public float angle = 0;
    public GameObject Moon;
    public Vector2 PlanetDistance;
    public float Speed;
    
}
