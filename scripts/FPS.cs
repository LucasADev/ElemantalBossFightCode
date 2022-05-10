using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpshow;
    float avg = 0;
    float frames = 0;
    // Start is called before the first frame update
    void Start()
    {
        fpshow = GetComponent<Text>();
        InvokeRepeating("show", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        avg += 1 / Time.unscaledDeltaTime;
    }
    void show()
    {
        
        fpshow.text = Mathf.Round(avg / frames).ToString();
        frames = 0;
        avg = 0;
    }
}

