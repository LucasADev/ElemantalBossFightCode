using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoss : Boss
{
    public Player player;
    public Material mat;
    public Sprite phase2;
    public BoxCollider2D colliders;
    public LineRenderer lr;
    float offset = 0;
    Vector2 targetPos;
    public Rigidbody2D rb;
    public float speed = 30;
    public GameObject icicle;
    public GameObject BigIce;
    // Start is called before the first frame update
    void Start()
    {
        health = 350;
        phase = 1;
    }
    public override void Die()
    {
        player.BossDie();
        if(MainMenuButtonManager.unlocked == 2)
        {
            MainMenuButtonManager.unlocked = 3;
        }
        base.Die();
    }
    public override void OnAttack()
    {
        if(health <= 175 && phase == 1)
        {
            onPhaseChange();
            phase = 2;
        }
        if(phase == 1)
        {
            switch (attack)
            {
                case 1:
                    targetPos = new Vector2(0, 7);
                    GoTo();
                    break;
                case 2:
                    rb.velocity = new Vector2(0, 0);
                    idle();
                    break;
                case 3:
                    IdleAction += GoTo;
                    break;
            }
        }
        else
        {
            switch (attack) 
            {
                case 1:

                    break;
                case 2:
                    IdleAction += b1;
                    break;
                case 3:
                    IdleAction -= b1;
                    lr.enabled = false;
                    break;
            }

        }
    }
    public void b1()
    {
        lr.enabled = true;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, Player.position);
        lr.startWidth += Time.deltaTime * (1 - lr.startWidth);
    }
    public void Attack1_1()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject gm = Instantiate(icicle, new Vector2(-25, -10 + i * 4), Quaternion.identity);
            gm.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            LineRenderer lr = gm.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = .3f;
            lr.startColor = new Color(0, 0, .5f);
            gm.AddComponent<linePrediction>();
        }
        for(int i = 0; i < 15; i++)
        {
            GameObject gm = Instantiate(icicle, new Vector2(-20 + i * 4, -13), Quaternion.identity);
            gm.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            LineRenderer lr = gm.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = .3f;
            gm.AddComponent<linePrediction>();
        }
        attack = 2;
    }
    public void Attack1_2()
    {
        for(int i = 0; i < 20; i++)
        {
            Invoke("shootIcicle", (float)i / 10);
        }
        attack = 3;
    }
    void shootIcicle()
    {
        Rigidbody2D rb = Instantiate(icicle, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = (Vector3)Player.position - transform.position;
        rb.velocity /= rb.velocity.magnitude / 15;
        rb.velocity += new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }
    public void Attack1_3()
    {
        attack = 1;
        for(int i = 0; i < 3; i++)
        {
            
            Invoke("iceCircle", i);
        }
    }
    void iceCircle()
    {
        offset += 5;
        for (float i = 0; i < 360; i += 30)
        {
            Rigidbody2D rb = Instantiate(icicle, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Mathf.Cos((i + offset) * Mathf.Deg2Rad), Mathf.Sin((i + offset) * Mathf.Deg2Rad));
            rb.velocity *= 10;
        }
        targetPos = new Vector2(Random.Range(-10, 10), Random.Range(-5, 5));
    }
    public override void onPhaseChange()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().sprite = phase2;
        colliders.size = new Vector2(colliders.size.x + 2, colliders.size.y + 4);
        Invoke("Attack2_1", 10);
        IdleAction -= GoTo;
    }
    public void Attack2_1()
    {
        attack = 2;
        for(int i = 0; i < 2; i++)
        {
            Invoke("shootBig", i);
        }
    }
    void shootBig()
    {
        lr.startWidth = .01f;
        Rigidbody2D rb = Instantiate(BigIce, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = (Vector3)Player.position - transform.position;
        rb.velocity /= rb.velocity.magnitude / 10;
    }
    public void Attack2_2()
    {
        for (int i = 0; i < 40; i++)
        {
            Invoke("justmiss", (float)i / 10);
        }
        for(int i = 0; i < 3; i++)
        {
            Invoke("iceCircle", i);
        }

            timeBetweenAttacks = 7;
        attack = 3;
    }
    void justmiss()
    {
        Rigidbody2D rb = Instantiate(icicle, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = Player.position - (Vector2)transform.position;
        
        rb.velocity += new Vector2(5, 5);
        rb.velocity /= rb.velocity.magnitude / 10;
        Rigidbody2D rbs = Instantiate(icicle, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        rbs.velocity = Player.position - (Vector2)transform.position;
        
        rbs.velocity -= new Vector2(5, 5);
        rbs.velocity /= rbs.velocity.magnitude / 10;
    }
    public void Attack2_3()
    {
        for(int i = 0; i < 3; i++)
        {
            Invoke("spin", i);
        }
        attack = 1;
    }
    int mult = 1;
    void spin()
    {
        mult *= -1;
        for(int j = 0; j < 7; j++)
        {
            GameObject gm = Instantiate(icicle, new Vector2(Player.position.x + 9 * mult, Player.position.y - 3 + j * 2.5f), Quaternion.identity);
            Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
            rb.velocity = (Vector3)Player.position - gm.transform.position;
            rb.velocity /= rb.velocity.magnitude / 10;
            LineRenderer lr = gm.AddComponent<LineRenderer>();
            lr.material = mat;
            lr.startWidth = .2f;
            lr.startColor = new Color(0, 0, 1, .5f);
            gm.AddComponent<linePrediction>();
        }
    }
    void idle()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position += new Vector3(0, Mathf.Sin(Time.time));
    }
    void GoTo()
    {
        if(Mathf.Round(transform.position.x) != Mathf.Round(targetPos.x) && Mathf.Round(transform.position.y) != Mathf.Round(targetPos.y))
        {
            Debug.Log(targetPos);
            rb.velocity = targetPos - (Vector2)transform.position;
            rb.velocity /= rb.velocity.magnitude / speed;
        }
        else
        {
            Debug.Log("false");
            rb.velocity = new Vector2(0, 0);
        }
        
    }


}
