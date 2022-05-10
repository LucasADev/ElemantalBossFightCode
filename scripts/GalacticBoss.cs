using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalacticBoss : Boss
{
    public Player player;
    public Material mat;
    public GameObject bounds1;
    public GameObject sword;
    public GameObject astroid;
    public GameObject smallStar;
    public GameObject starSplode;
    public GameObject[] planets;
    public GameObject starSystem;
    private void Start()
    {
        phase = 1;
        Camera.main.orthographicSize = 10;
        health = 500;
        timeBetweenAttacks = 5;
    }
    public override void OnAttack()
    {
        if(health <= 325 && phase == 1)
        {
            phase = 2;
        }
        if(health <= 140 && phase == 2)
        {
            Destroy(bounds1);
            phase = 3;
            timeBetweenAttacks = 8.5f;
            Camera.main.orthographicSize = 15;
        }
    }
    public override void Die()
    {
        player.BossDie();
        base.Die();
    }
    public void Attack1_1()
    {
        attack = 2;
        for(int i = 0; i < 3; i++)
        {
            Invoke("planetShoot", i);
        }
    }
    void planetShoot()
    {
        Rigidbody2D rb = Instantiate(planets[Random.Range(0, planets.Length)], transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = (Player.position - (Vector2)transform.position).normalized * 15;
    }
    public void Attack1_2()
    {
        Gravity.G = 2;
        attack = 3;
        for(int i = 0; i < 7; i++)
        {
            Invoke("gravityAstroid", (float)i / 2.5f);
        }
    }
    void gravityAstroid()
    {
        Instantiate(astroid, transform.position, Quaternion.identity).AddComponent<Gravity>();
    }
    public void Attack1_3()
    {
        attack = 1;
        for(float i = 0; i < 360; i += 30)
        {
            GameObject gm = Instantiate(astroid, transform.position, Quaternion.identity);
            Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
            gm.GetComponent<SpeedFade>().fadeSpeed = 0.998f;
            rb.velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * 10, Mathf.Sin(i * Mathf.Deg2Rad) * 10);
        }
    }
    
    public void Attack2_1()
    {
        Gravity.G = .5f;
        for(int i  = 0; i < 3; i++)
        {
            Invoke("planetGrav", i);
        }
        attack = 2;
    }
    void planetGrav()
    {
        Instantiate(planets[0], transform.position, Quaternion.identity).AddComponent<Gravity>();
    }
    public void Attack2_2()
    {
        attack = 3;
        for(float i = 0; i < 10; i++)
        {
            Invoke("tripleStar", i / 3);
        }
        attack = 3;
    }
    void tripleStar()
    {
        
        Rigidbody2D rb = Instantiate(smallStar, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        Vector2 dir = Player.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 10;
        rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * 20;
        rb = Instantiate(smallStar, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        dir = Player.position - (Vector2)transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 0;
        rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * 20;
        rb = Instantiate(smallStar, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        dir = Player.position - (Vector2)transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += -10;
        rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * 20;
    }
    
    public void Attack2_3()
    {
        attack = 1;
    }
    GameObject s1, s2;
    public void Attack3_1()
    {
        //astroidsword
        s1 = Instantiate(sword, transform.position, Quaternion.identity);
        s2 = Instantiate(sword, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.identity);
        s1.transform.eulerAngles = new Vector3(0, 0, -160);
        s2.transform.eulerAngles = new Vector3(0, 0, 160);
        Invoke("startswing", 1);
        attack = 2;
    }
    void startswing()
    {
        StartCoroutine("swingl");
        StartCoroutine("swingr");
    }
    
    IEnumerator swingl()
    {
        for(float i = 160; i > -90; i -= 90 * Time.deltaTime)
        {
            s2.transform.eulerAngles = new Vector3(0, 0, i);
            yield return null;
        }
    }
    IEnumerator swingr()
    {
        for(float i = -160; i < 90; i += 90 * Time.deltaTime)
        {
            s1.transform.eulerAngles = new Vector3(0, 0, i);
            yield return null;
        }
        destSwords();
    }
    void destSwords()
    {
        Destroy(s1);
        Destroy(s2);
    }

    float angle = 0;
    public void Attack3_2()
    {
        //superspin
        for(float i = 0; i < 56; i++)
        {
            Invoke("shootStarspin", i / 24);
        }
        attack = 3;
    }
    void shootStarspin()
    {
        angle += Random.Range(7.0f, 14.0f);
        for(int i = 0; i < 4; i++)
        {
            Rigidbody2D rb = Instantiate(smallStar, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos((angle + 90 * i) * Mathf.Deg2Rad), Mathf.Sin((angle + 90 * i) * Mathf.Deg2Rad)) * 10;
        }
        
    }
    
    public void Attack3_3()
    {
        attack = 1;
        for(float i = -20; i < 20; i += 4)
        {
             GameObject gm = Instantiate(planets[0], new Vector2(i, 12), Quaternion.identity);
            Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
            rb.velocity += Vector2.down * 5;
            rb.velocity += (Player.position - (Vector2)gm.transform.position).normalized * 10;
            LineRenderer lr = gm.AddComponent<LineRenderer>();
            lr.startWidth = .3f;
            lr.startColor = Color.green;
            lr.material = mat;
            gm.AddComponent<linePrediction>();

        }
    }
    
    
    
}
