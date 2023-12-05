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
            if (!item.isActiveAndEnabled)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemName.text = item.EquipmentKey;
                itemIcon.sprite = item.Icon;
            }            
        }
    }
}
