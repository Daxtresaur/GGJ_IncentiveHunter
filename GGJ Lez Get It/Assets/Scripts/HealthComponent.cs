using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public Action<int> OnDamage;
    public Action OnDie;
    [SerializeField] private int currentHP;
    private int maxHP;

    private void Start()
    {
        maxHP = currentHP;
    }

    public void Heal()
    {
        currentHP = maxHP;
    }

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

    private void Die()
    {
        OnDie.Invoke();
    }
}
