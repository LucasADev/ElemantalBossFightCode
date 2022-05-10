using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public SpriteRenderer sr;
    
    public float timeBetweenAttacks = 2;
    public int attack = 1;
    public int phase = 1;
    public bool CanAttack = true;
    public delegate void empty();
    public event empty IdleAction;
    public virtual void Update()
    {
        sr.color = Color.Lerp(Color.gray, Color.white, Time.time - timeSinceLastHit);
        if(IdleAction != null)
        {
            IdleAction();
        }
        if (CanAttack)
        {
            CanAttack = false;
            Invoke("canAttack", timeBetweenAttacks);
            OnAttack();
            Invoke($"Attack{phase}_{attack}", timeBetweenAttacks);
        }
    }
    public virtual void OnAttack()
    {

    }
    public virtual void onPhaseChange()
    {

    }
    public void canAttack()
    {
        CanAttack = true;
    }
    
}
