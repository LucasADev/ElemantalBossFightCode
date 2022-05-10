using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public List<Planet> planets = new List<Planet>();
    public GameObject smallStar;
    private void Update()
    {
        foreach(Planet planet in planets)
        {
            planet.angle += planet.Speed * Time.deltaTime ;
            planet.transform.position = new Vector2(Mathf.Cos(planet.angle) * planet.PlanetDistance.x, Mathf.Sin(planet.angle) * planet.PlanetDistance.y) + (Vector2)transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemey"))
        {
            if (collision.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                player.health -= 10;
                player.timeSinceLastHit = Time.time;
                return;
            }
            for (int i = 0; i < 360; i += 45)
            {
                Rigidbody2D rb = Instantiate(smallStar, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * 10, Mathf.Sin(i * Mathf.Deg2Rad) * 20);
            }

        }
    }
}
