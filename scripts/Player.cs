using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public SpriteRenderer fade;
    GameObject healthbar;
    public GameObject healthBarPrefab;
    public GameObject menu;
    public bool inveinceble = false;
    public float timeSinceLastHit;
    public static Vector2 position;
    public float damageMultiplyer;
    public LayerMask bounds;
    public static LayerMask boundss;
    public Sprite[] facing;
    public SpriteRenderer sr;
    public bool alive = true;
    #region guns
    public static gun pistol;
    public static gun devGun;
    #endregion
    public ParticleSystem cloud;
    public gun activeGun;
    public GameObject proj1;
    public static float moveSpeed = 10;
    public float health = 30;
    public float kbMultiplyer;
    public Ability ability;
    public Vector2 externalForces;
    public Rigidbody2D rb;
    int dir;
    float mX;
    float mY;
    float scalepower;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = Instantiate(healthBarPrefab, new Vector2(-15, 7), Quaternion.identity);
        pistol = new gun(this, 2, 1, .4f, 25, 1, proj1, 5, 3);

        devGun = new gun(this, 20, 10, .2f, 30, 10, proj1, 0, 3);
        activeGun = pistol;
        boundss = bounds;
        sr = GetComponent<SpriteRenderer>();
        ability = new Dash(rb, gameObject, cloud);
        ability.time = 1;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.transform.localScale = new Vector2(health / 10, 1);
        healthbar.transform.position = new Vector2(-15 + healthbar.transform.localScale.x * 2, healthbar.transform.position.y);
        sr.color = Color.Lerp(new Color(.2f, .2f, .2f, 1), Color.white, Time.time - timeSinceLastHit);
        position = transform.position;
        externalForces *= .98f;
        scalepower = 1 + rb.velocity.sqrMagnitude / moveSpeed;
        transform.localScale = new Vector2(transform.localScale.x, 1 + Mathf.Sin(Time.time * scalepower) / 10);

        animationHandeler();
        sr.sprite = facing[dir];
        if (alive)
        {
            rb.velocity = new Vector2(mX * moveSpeed, mY * moveSpeed) + externalForces;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ability.CanUse)
                {
                    ability.OnUse();
                    ability.CanUse = false;
                    Invoke("usable", ability.time);
                }


            }
            if (Input.GetMouseButton(0))
            {
                if (activeGun.canShoot)
                {
                    activeGun.canShoot = false;
                    Invoke("canuse", activeGun.shootSpeed);
                    activeGun.Shoot(transform.position);

                }
            }
        }
        
    }
    public void BossDie()
    {

        Invoke("back", 1);
        StartCoroutine("fadeIn");
        
    }
    IEnumerator fadeIn()
    {
        // sr.color = new Color(0, 0, 0, 0);
        for (float i = 0; i <= 1.1f; i += .05f)
        {

            Debug.Log(i + ", " + fade.color.a);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, i);
            yield return new WaitForSeconds(.04f);
        }
    }
    void back()
    {
        Debug.Log("mk");
        SceneManager.LoadScene("MainMenu");
    }
    public void checkDeath()
    {
        inveinceble = true;
        Invoke("uninvincable", .3f);
        if(health <= 0)
        {
            Die();
        }
    }
    public void uninvincable()
    {
        inveinceble = false;
    }
    public void Die()
    {
        alive = false;
        menu.SetActive(true);
    }
    void animationHandeler()
    {
        if(mY != 0)
        {
            if(mY > 0)
            {
                dir = 1;
            }
            else
            {
                dir = 0;
            }
            return;
        }
        if(mX > 0)
        {
            dir = 3;
        }
        else if(mX < 0)
        {
            dir = 2;
        }
        
        
    }
    void canuse()
    {
        activeGun.canShoot = true;
    }
    
    void usable()
    {
        ability.CanUse = true;
    }
    private void FixedUpdate()
    {
        mX = Input.GetAxisRaw("Horizontal");
        mY = Input.GetAxisRaw("Vertical");
    }
}
public class Ability
{
    public Sprite sprite;
    public bool CanUse = true;
    public float time;
    public float power = 1;
    public Ability(Sprite sprite, float time, float power)
    {
        this.sprite = sprite;
        this.time = time;
        this.power = power;
    }
    public Ability()
    {

    }
    public virtual void OnUse()
    {

    }
}
public class Dash : Ability
{
    public Rigidbody2D rb;
    public GameObject gm;
    public ParticleSystem cloud;
    public Dash(Rigidbody2D rb, GameObject gm, ParticleSystem cloud)
    {
        this.rb = rb;
        this.gm = gm;
        this.cloud = cloud;
    }
    public override void OnUse()
    {
        cloud.transform.position = gm.transform.position - ((Vector3)rb.velocity * power) / 2.5f;
        cloud.Play();
        RaycastHit2D ray = Physics2D.Raycast(gm.transform.position, rb.velocity * power / 2.5f, Vector2.Distance(gm.transform.position, rb.velocity * power / 2.5f), Player.boundss);
        Debug.DrawRay(gm.transform.position, rb.velocity * power / 2.5f, Color.red, 100);
        if (ray.collider == null)
        {
            gm.transform.position += ((Vector3)rb.velocity * power) / 2.5f;
        }
        else gm.transform.position = ray.point;
        
        
    }
}
public class gun : MonoBehaviour
{
    public float time;
    public Player player;
    public float damage;
    public float kb;
    public int shootCount;
    public float shootSpeed;
    public bool canShoot = true;
    public float bulletSpeed;
    public float bulletSpread;
    public GameObject projectile;
    public gun(Player player, float damaage, int shootCount, float shootSpeed, float bulletSpeed, float bulletSpread, GameObject projectile, float kb, float time)
    {
        this.player = player;
        this.damage = damaage;
        this.shootCount = shootCount;
        this.shootSpeed = shootSpeed;
        this.bulletSpeed = bulletSpeed;
        this.bulletSpread = bulletSpread;
        this.time = time;
        this.projectile = projectile;
        this.kb = kb;
    }
    public virtual void Shoot(Vector2 pos)
    {
        Vector2 mouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for(int i = 0; i < shootCount; i++)
        {
            GameObject gm = Instantiate(projectile, pos, Quaternion.identity);
            Rigidbody2D rb = gm.GetComponent<Rigidbody2D>();
            rb.velocity = mouspos - pos;
            rb.velocity /= rb.velocity.magnitude / bulletSpeed;
            rb.velocity += new Vector2(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread)) ;
            Projectile proj = gm.GetComponent<Projectile>();
            proj.damage = player.damageMultiplyer * damage;
            proj.kb = kb * player.kbMultiplyer;
            proj.time = time;
        }
    }
}

