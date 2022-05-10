using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject spawn;
    public float timeForSpawn = 1;
    public float scaleSpeed = 1;
    public float fadeSpeed = 3;
    public float deathTime = 4;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2(1, 0);
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, deathTime);
        Invoke("startFade", 2);
        Invoke("spawnMob", timeForSpawn);
    }
    void startFade()
    {
        StartCoroutine("fade");
    }
    void spawnMob()
    {
        Instantiate(spawn, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(0, (1 - transform.localScale.y) * Time.deltaTime * scaleSpeed);
    }
    IEnumerator fade()
    {
        for(float i = 1; i >= -.1f; i -= fadeSpeed * Time.deltaTime)
        {
            sr.color = new Color(sr.color.a, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
    
}
