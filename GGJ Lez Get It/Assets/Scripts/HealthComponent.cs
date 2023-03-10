using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public AudioClip heartBeat;
    public Action<int> OnDamage;
    public Action OnDie;
    [SerializeField] public PostProcessDeathEffect DeathEffect;
    [SerializeField] public int currentHP;
    private int maxHP;

    private void Start()
    {
        maxHP = currentHP;
    }

    public void Update()
    {
        if(currentHP <= -70)
        {
            SoundManager.instance.PlayAmbience(heartBeat, false);
        }
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
        //Debug.Log(DeathEffect.colorGradingLayer.saturation.value);
        DeathEffect.colorGradingLayer.saturation.value = value;
    }
}
