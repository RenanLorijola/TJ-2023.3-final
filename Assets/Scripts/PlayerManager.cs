using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Singleton { get; private set; }

    [SerializeField] private float maxInteractDistance = 4f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform equipmentHolderTransform;
    [SerializeField] private float equipAnimationDuration = 0.5f;
    [SerializeField] private int playerMaxHealth = 100;

    [SerializeField] private List<Equipment> equipments;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerHurtSound;

    public Transform ItemContent;
    public GameObject InventoryItem;


    public int CurrentHealth { get; private set; }
    public int MaxHealth => playerMaxHealth;
    
    private int equipedItemIndex = -1;
    private float equipAnimationTime = 0f;
    private bool equiping = false;

    private void Awake()
    {
        CurrentHealth = playerMaxHealth;
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Debug.LogWarning("PlayerManager already exists");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // Check Interactible
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxInteractDistance, LayerMask.GetMask("Interactible")))
        {
            var interactible = hit.transform.gameObject.GetComponent<Interactible>();
            if (interactible != null && interactible.AbleToInteract())
            {
                GameHudManager.Singleton.ShowInteract(true);
                Debug.Log("Interactible in range brahhh");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Interact");
                    interactible.Interact();
                }
            }
            else
            {
                GameHudManager.Singleton.ShowInteract(false);
            }
        }
        else
        {
            GameHudManager.Singleton.ShowInteract(false);
        }
        
        // Equipe Next Item
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextEquipment();
        }
        
        // Equip Animation
        if (equiping)
        {
            equipAnimationTime = Mathf.Max(0, equipAnimationTime - Time.deltaTime);
            float yPosition = Mathf.Lerp(-0.2f, 0, 1 - (equipAnimationTime/equipAnimationDuration));
            equipmentHolderTransform.localPosition = new Vector3(0, yPosition, 0);
            if (equipAnimationTime == 0)
            {
                equiping = false;
            }
        }
    }


    public void SetEquippedItem(string equipmentKey)
    {
        int index = equipments.FindIndex(x => x.EquipmentKey.Equals(equipmentKey));
        SetEquippedItem(index);
    }
    public void SetEquippedItem(int index)
    {
        bool hasEquipedNewItem = false;
        for (var i = 0; i < equipments.Count; i++)
        {
            var equip = equipments[i];
            if (i == index)
            {
                equip.Equip();
                equipedItemIndex = i;
                hasEquipedNewItem = true;
                equiping = true;
                equipAnimationTime = equipAnimationDuration;
            }
            else
            {
                equip.Unequip();
            }
        }

        if (!hasEquipedNewItem)
        {
            equipedItemIndex = -1;
        }
    }

    public List<Equipment> GetEquipments() => this.equipments;

    public void NextEquipment() // This is a temporary solution to test the itens
    {
        int nextEquipmentIndex = (equipedItemIndex + 1) % equipments.Count;
        SetEquippedItem(nextEquipmentIndex);
    }

    public void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;
        audioSource.PlayOneShot(playerHurtSound);
        GameHudManager.Singleton.PlayerHealthUpdated();
        Debug.Log(String.Format("Received Damage {0}/{1}", CurrentHealth, MaxHealth));
    }
}
