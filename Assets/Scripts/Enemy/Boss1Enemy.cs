using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss1Enemy : Enemy
{
    
    public enum EnemyStatus
    {
        Idle,
        Chasing,
        Attacking,
        SpellBurst,
        SpellFollow,
        Staggered,
        Dead
    }

    [SerializeField] private float chaseTriggerDistance = 10f;
    [SerializeField] private float distanceToAttack = 1;
    [SerializeField] private float staggerCooldown = 3f;
    [SerializeField] private float AttackCooldown = 1f;
    [SerializeField] private float spellCooldown = 3f;


    [SerializeField]private GameObject hitCheckObjet;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip spellSound;

    [SerializeField] private GameObject followProjectilePrefab;
    [SerializeField] private GameObject burstProjectilePrefab;
    [SerializeField] private Transform projectileSpawnTransform;


    [SerializeField] private GameObject secondPhasePrefab;




    private EnemyStatus enemyStatus = EnemyStatus.Idle;
    
    
    private float staggerCooldownTime = 0f;
    private float attackCooldownTime = 0f;
    private float spellCooldownTime = 0f;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int SpellBurst = Animator.StringToHash("SpellBurst");
    private static readonly int SpellFollow = Animator.StringToHash("SpellFollow");


    protected override void Update()
    {
        // Debug.Log(String.Format("Zombie Status {0} | Zombie health {1}", enemyStatus, CurrentHealth));
        staggerCooldownTime = Mathf.Max(0,staggerCooldownTime - Time.deltaTime);
        attackCooldownTime = Mathf.Max(0,attackCooldownTime - Time.deltaTime);
        spellCooldownTime = Mathf.Max(0,spellCooldownTime - Time.deltaTime);


        if (enemyStatus == EnemyStatus.Idle)
        {
            if (Vector3.Distance(PlayerManager.Singleton.transform.position, transform.position) <= chaseTriggerDistance)
            {
                GameManager.Singleton.SetFlag("boss_fase", 1);
                spellCooldownTime = spellCooldown;
                enemyStatus = EnemyStatus.Chasing;
                animator.SetBool(Running, true);
            }
        } else if (enemyStatus == EnemyStatus.Chasing)
        {
            navigationAgent.destination = PlayerManager.Singleton.transform.position;
            if (Vector3.Distance(PlayerManager.Singleton.transform.position, transform.position) <= distanceToAttack && attackCooldownTime <= 0)
            {
                enemyStatus = EnemyStatus.Attacking;
                navigationAgent.enabled = false;
                animator.SetTrigger(Attack);
            }
            else if (spellCooldownTime <= 0)
            {
                navigationAgent.enabled = false;
                if (Random.Range(0, 3) != 0)
                {
                    animator.SetTrigger(SpellBurst);
                    enemyStatus = EnemyStatus.SpellBurst;
                }
                else
                {
                    animator.SetTrigger(SpellFollow);
                    enemyStatus = EnemyStatus.SpellFollow;
                }
            }
        }
    }


    public void SpellShoot()
    {
        audioSource.PlayOneShot(spellSound, 0.5f);
        if (enemyStatus == EnemyStatus.SpellBurst)
        {
            int numberOfProjectiles = 5;
            float degreesSteps = 5;
            Vector3 direction = transform.forward;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                var projectileObject = Instantiate<GameObject>(burstProjectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
                Vector3 pDirection = Quaternion.AngleAxis(degreesSteps * i - degreesSteps * numberOfProjectiles/(float)2, Vector3.up) * direction;
                var projectile = projectileObject.GetComponent<BasicProjectile>();
                projectile.Setup(pDirection);
            }
        } else if (enemyStatus == EnemyStatus.SpellFollow)
        {
            var projectileObject = Instantiate<GameObject>(followProjectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
        }
    }

    public void SpellEnd()
    {
        enemyStatus = EnemyStatus.Chasing;
        navigationAgent.enabled = true;
        spellCooldownTime = spellCooldown;
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
        enemyStatus = EnemyStatus.Chasing;
        navigationAgent.enabled = true;
        attackCooldownTime = AttackCooldown;
    }

    public void SttagerAnimationEnd()
    {
        enemyStatus = EnemyStatus.Chasing;
        spellCooldownTime = spellCooldown;
        animator.SetBool(Running, true);
        navigationAgent.enabled = true;
        staggerCooldownTime = staggerCooldown;
    }

    public override void ReceiveDamage(int damage)
    {
        if (enemyStatus != EnemyStatus.Dead)
        {
            base.ReceiveDamage(damage);
            Debug.Log(String.Format("Boss1 Status {0} | Boss1 health {1}", enemyStatus, CurrentHealth));

            int hurtSoundIndex = Random.Range(0, hurtSounds.Length);
            
            audioSource.PlayOneShot(hurtSounds[hurtSoundIndex], 0.5f);
            if (CurrentHealth <= 0)
            {
                Die();
            }
            else
            {
                if ((enemyStatus == EnemyStatus.Chasing || enemyStatus == EnemyStatus.Idle) && staggerCooldownTime <= 0)
                {
                    animator.SetTrigger(Hit);
                    enemyStatus = EnemyStatus.Staggered;
                    navigationAgent.enabled = false;
                }
            }
        }
    }

    private void Die()
    {
        GameManager.Singleton.SetFlag("boss_fase", 2);
        Debug.Log("Boss1 Died");
        animator.applyRootMotion = true;
        animator.SetTrigger(Death);
        enemyStatus = EnemyStatus.Dead;
        navigationAgent.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Invoke("SpawnSecondPhase", 4f);
    }

    private void SpawnSecondPhase()
    {
        Destroy(gameObject);
        Instantiate<GameObject>(secondPhasePrefab, transform.position, Quaternion.identity);
        GameHudManager.Singleton.Fade(1f, 0f, 1f, null);
    }
}
