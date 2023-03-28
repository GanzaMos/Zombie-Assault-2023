using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHealth = 5;
    
    [SerializeField] [NotNull] ParticleSystem hitParticleEffect;
    public ParticleSystem HitParticleEffect { get { return hitParticleEffect; } }

    int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void DecreasingHealth(int decreasingAmount)
    {
        currentHealth -= decreasingAmount;
        
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Debug.Log("Enemy is dead!");
    }

}
