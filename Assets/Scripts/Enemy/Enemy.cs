using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int enemyMaxHealth = 100;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected NavMeshAgent navigationAgent;
    [SerializeField] protected Animator animator;
    
    public int CurrentHealth { get; private set; }

    public int MaxHealth => enemyMaxHealth;


    protected virtual void Awake()
    {
        CurrentHealth = enemyMaxHealth;
    }

    protected virtual void Update()
    {
    }

    public virtual void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}
