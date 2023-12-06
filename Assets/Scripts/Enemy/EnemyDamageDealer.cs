using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();

        if (playerManager != null)
        {
            Debug.Log("PlayerHit");
            playerManager.ReceiveDamage(damage);
        }
    }
}
