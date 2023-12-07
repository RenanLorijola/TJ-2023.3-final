using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1EnemyAnimationEventsHelper : MonoBehaviour
{

    private Boss1Enemy enemy;

    private void Awake()
    {
        enemy = transform.parent.gameObject.GetComponent<Boss1Enemy>();
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

    public void SpellShoot()
    {
        enemy.SpellShoot();
    }

    public void SpellEnd()
    {
        enemy.SpellEnd();
    }
}
