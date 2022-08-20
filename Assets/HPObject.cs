using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
    public float maxhp;

    public float currentHP;
    public bool isDead = false;

    public virtual void Start()
    {
        currentHP = maxhp;
    }

    public virtual void getDamage(float d)
    {
        currentHP -= d;
        if (currentHP <= 0 && !isDead)
        {
            die();
        }
    }

    public virtual void die()
    {
        isDead = true;
    }
}
