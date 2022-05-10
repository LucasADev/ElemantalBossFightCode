using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public delegate void empty();
    public event empty OnPress;
    private void Start()
    {
 
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPress?.Invoke();
        }
      
    }
   
}
