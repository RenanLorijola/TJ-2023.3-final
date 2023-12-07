using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float maximumTraveledDistance = 100f;

    private Vector3 movementDirection;
    private float currentTraveledDistance = 0f;



    public void Setup(Vector3 direction)
    {
        movementDirection = direction;
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDirection * (projectileSpeed * Time.deltaTime);
        currentTraveledDistance += projectileSpeed * Time.deltaTime;
        if (currentTraveledDistance >= maximumTraveledDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();

        if (playerManager != null)
        {
            Debug.Log("PlayerHit");
            playerManager.ReceiveDamage(damage);
            Destroy(gameObject);
        }    
    }
}
