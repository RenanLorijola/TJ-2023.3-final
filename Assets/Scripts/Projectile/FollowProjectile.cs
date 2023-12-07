using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 5.5f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float maximumTraveledDistance = 100f;

    private float currentTraveledDistance = 0f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerManager.Singleton.PlayerCamera.transform.position, projectileSpeed * Time.deltaTime);
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
