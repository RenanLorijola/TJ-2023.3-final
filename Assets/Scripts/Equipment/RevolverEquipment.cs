using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverEquipment : Equipment
{

    [SerializeField] private Camera playerCamera;
    [SerializeField] private int damage = 40;
    [SerializeField] private int damageDebug = 300;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private int maxRounds = 6;
    [SerializeField] private ParticleSystem gunshotParticleSystem;


    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip emptySound;




    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Reload = Animator.StringToHash("Reload");

    private bool reloading;
    private float cooldownTime;
    private int currentRounds = 0;

    public bool CanShoot => CanUseEquipment() && cooldownTime <= 0 && !reloading;


    // Update is called once per frame

    private void Awake()
    {
        currentRounds = maxRounds;
    }

    protected override void OnUnequip()
    {
        reloading = false;
    }

    void Update()
    {
        cooldownTime = Mathf.Max(0, cooldownTime - Time.deltaTime);
        if (CanUseEquipment())
        {
            if (Input.GetMouseButtonDown(0) && CanShoot)
            {
                cooldownTime = shootCooldown;
                if (currentRounds <= 0)
                {
                    audioSource.PlayOneShot(emptySound);
                }
                else
                {
                    audioSource.PlayOneShot(shootSound);
                    animator.SetTrigger(Shoot);
                    currentRounds--;
                    gunshotParticleSystem.Play();
                    CheckHit(damage);
                }
            }
            else if (Input.GetMouseButtonDown(2) && CanShoot)
            {
                audioSource.PlayOneShot(shootSound);
                animator.SetTrigger(Shoot);
                currentRounds--;
                gunshotParticleSystem.Play();
                CheckHit(damageDebug);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                audioSource.PlayOneShot(reloadSound);
                animator.SetTrigger(Reload);
            }
        }
    }

    public void ReloadAnimationEnd()
    {
        reloading = false;
        currentRounds = maxRounds;
    }

    private void CheckHit(int this_damage)
    {
        var ray = playerCamera.ScreenPointToRay(playerCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999, LayerMask.GetMask("Enemy")))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReceiveDamage(this_damage);
            }
            else
            {
                Debug.Log("Missfire ?");
                Debug.Log(hit.transform.gameObject);
            }
        }
    }
}
