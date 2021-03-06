using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHolder : MonoBehaviour
{
    public static AnimationHolder gm;
    public static float speed = .05f;
    
    private void Start()
    {
        gm = this;
    }
    public static IEnumerator fadeIn( SpriteRenderer sr)
    {
        for(float i = 0; i <= 1; i += speed)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
    public static IEnumerator fadeOut( SpriteRenderer sr)
    {
        for(float i = 1; i >= 0; i -= speed)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
    public static IEnumerator fade( SpriteRenderer sr)
    {
        for (float i = 0; i <= 1; i += speed)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
        for (float i = 1; i >= 0; i -= speed)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
            yield return null;
        }
    }
   
}
