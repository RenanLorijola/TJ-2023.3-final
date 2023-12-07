using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieEnemy : Enemy
{
    
    public enum ZombieStatus
    {
        Idle,
        Chasing,
        Attacking,
        Staggered,
        Dead
    }

    [SerializeField] private float chaseTriggerDistance = 10f;
    [SerializeField] private float distanceToAttack = 1;
    [SerializeField] private float staggerCooldown = 3f;
    [SerializeField] private float AttackCooldown = 1f;


    [SerializeField]private GameObject hitCheckObjet;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] hurtSounds;



    private ZombieStatus enemyStatus = ZombieStatus.Idle;
    
    
    private float staggerCooldownTime = 0f;
    private float attackCooldownTime = 0f;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Hit = Animator.StringToHash("Hit");
    

    protected override void Update()
    {
        // Debug.Log(String.Format("Zombie Status {0} | Zombie health {1}", enemyStatus, CurrentHealth));
        staggerCooldownTime = Mathf.Max(0,staggerCooldownTime - Time.deltaTime);
        attackCooldownTime = Mathf.Max(0,attackCooldownTime - Time.deltaTime);
        if (GameManager.Singleton.GetFlag("boss_fase") == 3)
        {
            ReceiveDamage(999);
        }
        if (enemyStatus == ZombieStatus.Idle)
        {
            if (Vector3.Distance(PlayerManager.Singleton.transform.position, transform.position) <= chaseTriggerDistance)
            {
                enemyStatus = ZombieStatus.Chasing;
                animator.SetBool(Running, true);
            }
        } else if (enemyStatus == ZombieStatus.Chasing)
        {
            navigationAgent.destination = PlayerManager.Singleton.transform.position;
            if (Vector3.Distance(PlayerManager.Singleton.transform.position, transform.position) <= distanceToAttack && attackCooldownTime <= 0)
            {
                enemyStatus = ZombieStatus.Attacking;
                navigationAgent.enabled = false;
                animator.SetTrigger(Attack);
            }
        }
    }


    public void AttackHitCheck()
    {
        hitCheckObjet.SetActive(true);
        Invoke("AttackHitCheckEnd", 0.1f);
    }

    public void AttackHitCheckEnd()
    {
        hitCheckObjet.SetActive(false);
    }


    public void AttackAnimationEnded()
    {
        enemyStatus = ZombieStatus.Chasing;
        navigationAgent.enabled = true;
        attackCooldownTime = AttackCooldown;
    }

    public void SttagerAnimationEnd()
    {
        enemyStatus = ZombieStatus.Chasing;
        animator.SetBool(Running, true);
        navigationAgent.enabled = true;
        staggerCooldownTime = staggerCooldown;
    }

    public override void ReceiveDamage(int damage)
    {
        if (enemyStatus != ZombieStatus.Dead)
        {
            base.ReceiveDamage(damage);
            Debug.Log(String.Format("Zombie Status {0} | Zombie health {1}", enemyStatus, CurrentHealth));

            int hurtSoundIndex = Random.Range(0, hurtSounds.Length);
            
            audioSource.PlayOneShot(hurtSounds[hurtSoundIndex], 0.5f);
            if (CurrentHealth <= 0)
            {
                Die();
            }
            else
            {
                if ((enemyStatus == ZombieStatus.Chasing || enemyStatus == ZombieStatus.Idle) && staggerCooldownTime <= 0)
                {
                    animator.SetTrigger(Hit);
                    enemyStatus = ZombieStatus.Staggered;
                    navigationAgent.enabled = false;
                }
            }
        }
    }

    private void Die()
    {
        Debug.Log("Die");
        animator.applyRootMotion = true;
        animator.SetTrigger(Death);
        enemyStatus = ZombieStatus.Dead;
        navigationAgent.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Collider>().enabled = false;
    }
}
