using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LeafBoss : Boss
{
    public Player player;
    public SpriteRenderer fade;
    public Vector2[] positions;
    public Vector2[] directions;
    public GameObject summons;
    public GameObject warningTree;
    public GameObject tree;
    public GameObject Bigrocks;
    public GameObject spike;
    // Start is called before the first frame update
    void Start()
    {
        health = 275;
    }
    public override void OnAttack()
    {
        switch (attack)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
        }

    }
    
    

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        player.BossDie();
        if(MainMenuButtonManager.unlocked == 1)
        {
            MainMenuButtonManager.unlocked = 2;
        }
        base.Die();
    }
    public void Attack1_1()
    {
        attack = 2;
        for(int i = 0; i < 15 / (.5f + (health / 100)); i++)
        {
            Invoke("trees", (float)i / 5 * (health / 100));
        }
        timeBetweenAttacks = 4;
    }
    void trees()
    {
        Vector2 targetPos = new Vector2(Random.Range(-18, 18), Random.Range(-10, 3.5f));
        Instantiate(warningTree, targetPos, Quaternion.identity);
        Instantiate(tree, new Vector2(targetPos.x + 2, targetPos.y), Quaternion.identity);

    }
    public void Attack1_2()
    {
        for(int i = 0; i < 3; i++)
        {
            Instantiate(Bigrocks, new Vector2(-10 + i * 10, 2), Quaternion.identity);
        }
        attack = 3;
    }
    public void Attack1_3()
    {
        for(int i = 0; i < 4; i++)
        {
            spikeSpawn(i);
        }
        attack = 4;
    }
    void spikeSpawn(int i)
    {
        Rigidbody2D rb = Instantiate(spike, directions[i], Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = directions[i] * (-2 / (health / 100));
    }
    public void Attack1_4()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(summons, new Vector2(i * 4 - 3, 9), Quaternion.identity);
        }
        attack = 1;
    }
}
