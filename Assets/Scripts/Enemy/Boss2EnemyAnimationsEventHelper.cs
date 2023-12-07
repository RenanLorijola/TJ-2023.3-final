using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2EnemyAnimationsEventHelper : MonoBehaviour
{

    private Boss2Enemy enemy;

    private void Awake()
    {
        enemy = transform.parent.gameObject.GetComponent<Boss2Enemy>();
    }
    
    public void AttackHitCheck()
    {
        enemy.AttackHitCheck();
    }


    public void AttackAnimationEnded()
    {
        enemy.AttackAnimationEnded();
    }

    public void StaggerAnimationEnd()
    {
        enemy.SttagerAnimationEnd();
    }
}