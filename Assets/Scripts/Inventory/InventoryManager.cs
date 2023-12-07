using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform ItemContent;
    public GameObject InventoryItem;
    private List<Equipment> equipments = new List<Equipment>();
    public void ListItems()
    {
        equipments = PlayerManager.Singleton.GetEquipments();

        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in equipments)
        {
            int grabbed_knife = GameManager.Singleton.GetFlag("grabbed_knife");
            int grabbed_gun = GameManager.Singleton.GetFlag("grabbed_gun");

            if ((item.name.ToLower() == "knife" && grabbed_knife == 1) ||
                (item.name.ToLower() == "gun" && grabbed_gun == 1))
            {
                if (!item.isActiveAndEnabled)
                {
                    ShowInventory(item);
                }
            }      
        }
    }

    private void ShowInventory(Equipment item)
    {
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        itemName.text = item.EquipmentName;
        itemIcon.sprite = item.Icon;
    }
}
