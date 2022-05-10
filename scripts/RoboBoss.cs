using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboBoss : Boss
{
    public Player players;
    public LineRenderer laser2;
    SpriteRenderer fade;
    public SpriteRenderer laserWarning;
    public SpriteRenderer target;
    public LayerMask player;
    public Transform missilePoint, gunPoint, LaserPoint;
    public GameObject missile;
    public GameObject goodMissile;
    public LineRenderer laser;
    public GameObject bullet;
    bool lastlase = true;
    int laserswipepos = 90;
    int laserSWipeDir = 5;
    Vector2 opsos1, opos2;
    // Start is called before the first frame update
    private void Start()
    {
        health = 450;
        phase = 1;
        timeBetweenAttacks = 3;
    }
    public override void Die()
    {
        players.BossDie();
        if(MainMenuButtonManager.unlocked == 4)
        {
            MainMenuButtonManager.unlocked++;
        }
        base.Die();
    }
    public void Attack1_1()
    {
        target.enabled = true;
        laser.enabled = false;
        for(int i = 0; i < 20; i++)
        {
            Invoke("shootBullet", (float)i / 10);
        }
        attack = 2;
    }
    void LaserWarning()
    {
        fade = laserWarning;
        
        if (lastlase)
        {
            fade.transform.position = new Vector2(-9, 0);
            return;
        }
        fade.transform.position = new Vector2(9, 0);
    }
    void shootBullet()
    {
        Rigidbody2D rb = Instantiate(bullet, gunPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>() ;
        rb.velocity = (Player.position - (Vector2)gunPoint.transform.position).normalized * 35;
        
    }
    IEnumerator laserThin()
    {
        for(float i = 1; i >= 0; i -= 0.01f)
        {
            laser.startWidth = i;
            yield return null;
        }
    }
    IEnumerator laserThin2()
    {
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            laser2.startWidth = i;
            yield return null;
        }
    }
    public void Attack1_2()
    {
        target.enabled = false;
        Invoke("fadeinstart", 1.5f);
        LaserWarning();
        for(int i = 0; i < 10; i++)
        {
            Invoke("shootmissile", (float)i / 5);
        }
        attack = 3;
    }
    void shootmissile()
    {
        Rigidbody2D rb = Instantiate(missile, missilePoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = (Player.position - (Vector2)missilePoint.position).normalized * 25;
    }
    public void Attack1_3()
    {
        laser.startWidth = 1;
        StartCoroutine("fadeOut");
        Invoke("thin", 1);
        laser.enabled = true;
        attack = 1;
        if (lastlase)
        {
            lastlase = false;
            StartCoroutine("laserSwipeFromRight");
        }
        else 
        { 
            StartCoroutine("laserSwipeFromLeft");
            lastlase = true;
        }
        Invoke("show", 1.5f);
        
    }
    public override void OnAttack()
    {
        if (health <= 225 && phase == 1)
        {
            phase = 2;
            onPhaseChange();
        }
    }
    public override void onPhaseChange()
    {
        timeBetweenAttacks = 5.5f;
    }
    void thin()
    {
        StartCoroutine("laserThin");
    }
    void show()
    {
        target.enabled = true;
    }
    IEnumerator laserSwipeFromRight()
    {
        for (float i = 90; i < 300; i += 180 * Time.deltaTime)
        {
            RaycastHit2D ray = Physics2D.Raycast(LaserPoint.position, new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad)), 1000, player);
            if(ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    Player player = ray.collider.GetComponent<Player>();
                    player.timeSinceLastHit = Time.time;
                    player.health -= .5f;
                    player.checkDeath();
                }
                laser.SetPosition(0, LaserPoint.position);

                laser.SetPosition(1, ray.point);
            }
            yield return null;
        }
       
    }
    IEnumerator laserSwipeFromLeft()
    {
        for (float i = 430; i > 230; i -= 180 * Time.deltaTime)
        {
            RaycastHit2D ray = Physics2D.Raycast(LaserPoint.position, new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad)), 1000, player);
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    Player player = ray.collider.GetComponent<Player>();
                    player.timeSinceLastHit = Time.time;
                    player.health -= 3;
                    player.checkDeath();
                }
                laser.SetPosition(0, LaserPoint.position);

                laser.SetPosition(1, ray.point);
            }
            yield return null;
        }

    }
    void fadeinstart()
    {
        StartCoroutine("fadeIn");
    }
    IEnumerator fadeIn()
    {
        for (float i = 0; i <= .6f; i += 0.01f)
        {
            laserWarning.color = new Color(laserWarning.color.r, laserWarning.color.g, laserWarning.color.b, i);
            yield return null;
        }
    }
    IEnumerator fadeOut()
    {
        for (float i = .6f; i >= 0; i -= 0.01f)
        {
            laserWarning.color = new Color(laserWarning.color.r, laserWarning.color.g, laserWarning.color.b, i);
            yield return null;
        }
    }
    public void Attack2_1()
    {
        
        for(float i = 0; i < 5; i++)
        {
            Invoke("doubleRocket", i / 3);
        }
        attack = 2;
    }
    public void Attack2_2()
    {
        attack = 3;
        laser.startWidth = 1;
        laser2.startWidth = 1;
        float angle = Mathf.Atan2(Player.position.y - transform.position.y, Player.position.x - transform.position.x) * Mathf.Rad2Deg;
        angle += 30;
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        opsos1 = direction;
        angle = Mathf.Atan2(Player.position.y - transform.position.y, Player.position.x - transform.position.x) * Mathf.Rad2Deg;
        angle -= 30;
        direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        opos2 = direction;
        laser.enabled = true;
        StartCoroutine("doubleLaser", attack);
        for(int i = 0; i < 3; i++)
        {
            Invoke("shootBullet", i);
        }
    }
    void doubleRocket()
    {
        int dir = 1;
        for (int i = 0; i < 2; i++)
        {
            dir *= -1;
            Instantiate(goodMissile, new Vector2(transform.position.x + 2 * dir, transform.position.y), Quaternion.identity);
        }
    }
    public void Attack2_3()
    {
        attack = 1;
        for(int i = 0; i < 5; i++)
        {
            Invoke("shootBullet", (float)i / 5);
            Invoke("shootmissile", (float)i / 5);
        }
    }
    IEnumerator doubleLaser(int attack)
    {
        while(this.attack == attack)
        {
            
            
            RaycastHit2D ray = Physics2D.Raycast(LaserPoint.position, opsos1, 1000, player);
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    Player player = ray.collider.GetComponent<Player>();
                    player.timeSinceLastHit = Time.time;
                    player.health -= 3;
                    player.checkDeath();
                }
                laser.SetPosition(0, LaserPoint.position);

                laser.SetPosition(1, ray.point);
            }
           
            RaycastHit2D rayw = Physics2D.Raycast(LaserPoint.position,   opos2, 1000, player);
            if (rayw.collider != null)
            {
                if (rayw.collider.CompareTag("Player"))
                {
                    Player player = rayw.collider.GetComponent<Player>();
                    player.timeSinceLastHit = Time.time;
                    player.health -= 3;
                    player.checkDeath();
                }
                laser2.SetPosition(0, LaserPoint.position);

                laser2.SetPosition(1, rayw.point);
            }
            yield return null;
        }
        StartCoroutine("laserThin");
        StartCoroutine("laserThin2");
    }
    
}

   