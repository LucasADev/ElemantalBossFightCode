using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public SpriteRenderer sr;
    public Button[] buttons;
    private void Start()
    {
        
        buttons[0].OnPress += retry;
        buttons[1].OnPress += Menu;
        StartCoroutine("halfFade");
    }
    void retry()
    {
        StartCoroutine("fadeIn");
        Invoke("restart", 1);
    }
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void exit()
    {
        Application.Quit();
    }
    IEnumerator halfFade()
    {
        sr.sortingOrder = 1;
        for (float i = 0; i <= .6f; i += .05f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
    IEnumerator fadeIn()
    {
        sr.sortingOrder = 3;
        for (float i = 0; i <= 1.1f; i += .05f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
    IEnumerator fadeOut()
    {
        for (float i = 1; i >= -.1f; i -= .05f)
        {

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
}
