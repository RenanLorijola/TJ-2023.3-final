using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyAnimationEventsHelper : MonoBehaviour
{

    private ZombieEnemy zombieEnemy;

    private void Awake()
    {
        zombieEnemy = transform.parent.gameObject.GetComponent<ZombieEnemy>();
    }
    
    public void AttackHitCheck()
    {
        zombieEnemy.AttackHitCheck();
    }


    public void AttackAnimationEnded()
    {
        zombieEnemy.AttackAnimationEnded();
    }

    public void StaggerAnimationEnd()
    {
        zombieEnemy.SttagerAnimationEnd();
    }
}
