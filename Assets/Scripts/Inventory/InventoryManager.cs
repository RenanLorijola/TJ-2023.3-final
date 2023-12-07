using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            bool grabbed_knife = GameManager.Singleton.GetFlag("grabbed_knife") != 0 || SceneManager.GetActiveScene().name == "Floresta";
            bool grabbed_gun = GameManager.Singleton.GetFlag("grabbed_gun") != 0 || SceneManager.GetActiveScene().name == "Floresta";

            if ((item.name.ToLower() == "knife" && grabbed_knife) ||
                (item.name.ToLower() == "gun" && grabbed_gun))
            {
                ShowInventory(item);
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
