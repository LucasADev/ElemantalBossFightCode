using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    //how to play is open
    public Rigidbody2D howToPlayRb;
    bool isopen = false;
    public Button[] buttons;
    public SpriteRenderer leftSr, rightSr;
    public static int unlocked = 2;
    public int current = 1;
    public string[] scenes = { "grass", "ice", "Ghost", "Tech" };
    void Start()
    {
        isopen = false;
        current = 1;
        buttons[0].OnPress += exit;
        buttons[1].OnPress += left;
        buttons[2].OnPress += right;
        buttons[3].OnPress += Play;
        buttons[4].OnPress += howToPlay;
        buttons[5].OnPress += off;
        MenuCamera.targetPos = new Vector2(current * 18, 0);
    }
    void Update()
    {
        howToPlayRb.transform.position = new Vector2(Camera.main.transform.position.x, howToPlayRb.transform.position.y);
    }
    void exit()
    {
        Application.Quit();
    }
    void left()
    {
        Debug.Log(current);
        if(current - 1 > 0)
        {
            current--;
            MenuCamera.targetPos = new Vector2(current * 18, 0);
            if(current - 1 <= 0)
            {
                leftSr.color = Color.gray;
            }
            else
            {
                leftSr.color = Color.green;
            }
            if (current + 1 > unlocked)
            {
                rightSr.color = Color.gray;
            }
            else
            {
                rightSr.color = Color.green;
            }
        }
    }
    void right()
    {
        
        Debug.Log(current);
        if(current + 1 <= unlocked)
        {
            current++;
            MenuCamera.targetPos = new Vector2(current * 18, 0);
            if (current + 1 > unlocked)
            {
                rightSr.color = Color.gray;
            }
            else
            {
                rightSr.color = Color.green;
            }
            if (current - 1 <= 0)
            {
                leftSr.color = Color.gray;
            }
            else
            {
                leftSr.color = Color.green;
            }
        }
    }
    void Play()
    {
        SceneManager.LoadScene(scenes[current - 1]);
    }
    void howToPlay()
    {
        if (!isopen)
        {
            StartCoroutine("go");
            isopen = true;
            
        }
        else
        {
            StartCoroutine("away");
            isopen = false;
        }
    }
    void off()
    {
        StartCoroutine("away");
        isopen = false;
    }
    IEnumerator go()
    {
        
        while((Camera.main.transform.position.y - howToPlayRb.transform.position.y) * 5 * Time.deltaTime > .01f)
        {
            howToPlayRb.velocity = (Camera.main.transform.position - howToPlayRb.transform.position) * 5;
            Debug.Log(howToPlayRb.velocity);
            yield return null;
        }
        howToPlayRb.velocity = new Vector2(0, 0);
        
    }
    IEnumerator away()
    {
        while((new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 15) - howToPlayRb.transform.position).sqrMagnitude * 5 > .05f)
        {
            howToPlayRb.velocity = (new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 15) - howToPlayRb.transform.position) * 5;
            Debug.Log(howToPlayRb.velocity);
            yield return null;
        }
        howToPlayRb.velocity = new Vector2(0, 0);
    }


}
