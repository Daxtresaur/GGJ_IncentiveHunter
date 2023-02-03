using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public Action<int> OnDamage;
    public Action OnDie;
    private int currentHP;
    //private int maxHP;

    public void Damage(int damage)
    {
        OnDamage?.Invoke(damage);
        if(currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
        else
        {
            currentHP -= damage;
        }
    }

    public void Die()
    {
        OnDie.Invoke();
    }
}
