using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{

    [SerializeField] private string equipmentKey;
    [SerializeField] private string equipmentName;

    public string EquipmentKey => equipmentKey;
    public string EquipmentName => equipmentName;
    public Sprite Icon;

    public static Equipment Current { get; private set; }

    private void OnEnable()
    {
        if (Current != null)
        {
            Current.Unequip();
        }

        Current = this;
    }

    private void OnDisable()
    {
        if (Current == this)
        {
            Current = null;
        }
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        OnEquip();
    }

    public void Unequip()
    {
        gameObject.SetActive(false);
        OnUnequip();
    }

    protected virtual void OnEquip()
    {

    }

    protected virtual void OnUnequip()
    {

    }

    protected bool CanUseEquipment()
    {
        return !GameManager.Singleton.PlayingEvent && Time.timeScale > 0f;
    }

}
