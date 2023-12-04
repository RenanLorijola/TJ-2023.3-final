using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEquipment : Equipment
{

    [SerializeField] private AudioSource knifeAudioSource;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Animator animator;


    [SerializeField] private AudioClip attackSound;
    
    
    private float cooldownTime = 0f;

    private static readonly int Attack = Animator.StringToHash("Attack");

    // Update is called once per frame
    void Update()
    {
        cooldownTime = Mathf.Max(0, cooldownTime - Time.deltaTime);
        if (CanUseEquipment())
        {
            if (Input.GetMouseButtonDown(0) && cooldownTime <= 0f)
            {
                cooldownTime = attackCooldown;
                animator.SetTrigger(Attack);
            } 
        }
    }

    public void CheckHit()
    {
        knifeAudioSource.PlayOneShot(attackSound);
    }
}
