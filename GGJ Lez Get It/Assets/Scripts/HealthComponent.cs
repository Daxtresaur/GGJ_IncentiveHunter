using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public Action<int> OnDamage;
    public Action OnDie;
    [SerializeField] public PostProcessDeathEffect DeathEffect;
    [SerializeField] private int currentHP;
    private int maxHP;

    private void Start()
    {
        maxHP = currentHP;
    }

    public void Heal()
    {
        currentHP = maxHP;
        AdjustGrayScale(currentHP);
    }

    public void Damage(int damage)
    {
        OnDamage?.Invoke(damage);
        if(currentHP <= -100)
        {
            currentHP = -100;
            AdjustGrayScale(currentHP);
            Die();
        }
        else
        {
            currentHP -= damage;
            AdjustGrayScale(currentHP);
        }
    }

    private void Die()
    {
        OnDie.Invoke();
    }

    private void AdjustGrayScale(float value)
    {
        DeathEffect.colorGradingLayer.saturation.value = value;
    }
}
