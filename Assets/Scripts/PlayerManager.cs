using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private float maxInteractDistance = 4f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform equipmentHolderTransform;
    [SerializeField] private float equipAnimationDuration = 0.5f;

    [SerializeField] private List<Equipment> equipments;


    private int equipedItemIndex = -1;
    private float equipAnimationTime = 0f;
    private bool equiping = false;

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

    public void NextEquipment() // This is a temporary solution to test the itens
    {
        int nextEquipmentIndex = (equipedItemIndex + 1) % equipments.Count;
        SetEquippedItem(nextEquipmentIndex);
    }
}
