using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : Boss
{
    public Player player;
    public SpriteRenderer warning;
    public float speed = 10;
    public float waveSpeeed = .5f;
    public Vector2 targetPos;
    public Rigidbody2D rb;
    public GameObject wave;
    public GameObject zombieWarning;
    public GameObject GhostDash;

    private void Start()
    {
        health = 325;
        timeBetweenAttacks = 4;
    }
    public override void Die()
    {
        player.BossDie();
        if(MainMenuButtonManager.unlocked == 3)
        {
            MainMenuButtonManager.unlocked = 4;
        }
        base.Die();
    }
    public override void OnAttack()
    {
        if(phase == 1)
        {
            if(health <= 100)
            {
                onPhaseChange();
                phase = 2;
            }
            switch (attack) 
            {
                case 1:
                    //IdleAction += GoToPoint;
                    warning.enabled = true;
                    break;
                case 2:
                    // IdleAction -= GoToPoint;
                    warning.enabled = false;
                    rb.velocity = new Vector2(0, 0);
                    break;
                case 3:
                    Invoke("enable", 2);
                    break;
            }
        }
        else
        {
            switch (attack)
            {
                case 1:
                    rb.velocity = new Vector2(0, 0);
                    IdleAction -= GoToPoint;
                    break;
                case 2:
                    targetPos = new Vector2(0, 7);
                    IdleAction += GoToPoint;
                    break;
                case 3:
                    targetPos = new Vector2(0, 0);
                    break;
            }
        }
    }
    void enable()
    {
        warning.enabled = true;
    }
    public override void onPhaseChange()
    {
        warning.enabled = false;
    }
    public void Attack1_1()
    {
        targetPos = new Vector2(Random.Range(-10, 10), Random.Range(-7, 7));
        for(int i = 0; i < 10; i++)
        {
            Invoke("shootWave", (float)i / 5);
        }
        attack = 2;
    }
    void shootWave()
    {
        GameObject gm = Instantiate(wave, transform.position, Quaternion.identity);
        Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
        rb.velocity = ((Vector3)Player.position - transform.position).normalized * 15;
    }
    public void Attack1_2()
    {
        for(int i = 0; i < 3; i++)
        {
            Invoke("bigWave", i);
        }
        attack = 3;
    }
    void bigWave()
    {
        GameObject gm = Instantiate(wave, transform.position, Quaternion.identity);
        gm.GetComponent<GhostWave>().ScaleSpeed = 3;
        Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
        rb.velocity = (Player.position - (Vector2)transform.position).normalized * 10;
    }
    void spawnGrave()
    {
    }
    public void Attack1_3()
    {
        for(int i = 0; i < 360; i += 30)
        {
            GameObject gm = Instantiate(wave, new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * 20, Mathf.Sin(i * Mathf.Deg2Rad) * 20), Quaternion.identity);
            Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
            rb.velocity = (new Vector3(0, 0) - gm.transform.position) * waveSpeeed / 10;
        }
        attack = 1;
    }
    void GoToPoint()
    {
        rb.velocity = (targetPos - (Vector2)transform.position).normalized * speed;
    }
    public void Attack2_1()
    {
        attack = 2;
    }
    public void Attack2_2()
    {
        attack = 3;
        for(float i = 0; i < 36; i++)
        {
            Invoke("spin", i / 15);
        }
    }
    int dir = 1;
    public void Attack2_3()
    {
        spinpoint = 0;
        for(int i = 0; i < 2; i++)
        {
            Invoke("side", (float)i * 1.5f);
        }
        attack = 1;
    }
    int spinpoint = 0;
    void spin()
    {
        spinpoint += 15;
        GameObject gm = Instantiate(wave, transform.position, Quaternion.identity);
        gm.GetComponent<GhostWave>().fadeSpeed = .025f;
        Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Mathf.Cos(spinpoint * Mathf.Deg2Rad), Mathf.Sin(spinpoint * Mathf.Deg2Rad)) * 5;

    }
    void side()
    {
        GameObject gm = Instantiate(wave, new Vector2(18 * dir, 0), Quaternion.identity);
        Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-dir * 9, 0);
        gm.transform.localScale = new Vector2(12, 12);
            dir *= -1;

    }
}

