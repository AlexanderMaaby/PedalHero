using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool inventoryEnabled;

    private int allSlots;
    private int enabledSlots;
    public GameObject[] slot;

    public Item equippedSword;
    public Item equippedWheel;
    public Item equippedHelm;

    public List<Item> itemsCarried = new List<Item>();

    public GameObject slotHolder;

    public GameObject equip1; //helm
    public GameObject equip2; //wheel
    public GameObject equip3; //sword

    void Start()
    {
        allSlots = 20;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        PopulateAllItemSlots();
    }

    public bool EquipItem(Item equipItem)
    {

        string type = equipItem.GetType().ToString();
        if (type.Trim().Equals("Helm"))
        {
            if (equippedHelm != null)
            {
                itemsCarried.Add(equippedHelm);
            }
            equippedHelm = equipItem;
            itemsCarried.Remove(equipItem);
            equip1.GetComponent<ItemSlot>().AddItem(equipItem);
            PopulateAllItemSlots();
            return true;
        }
        else if (type.Trim().Equals("Weapon"))
        {
            if (equippedSword != null)
            {
                itemsCarried.Add(equippedSword);
            }
            equippedSword = equipItem;
            itemsCarried.Remove(equipItem);
            equip3.GetComponent<ItemSlot>().AddItem(equipItem);
            PopulateAllItemSlots();
            return true;
        }
        else if (type.Trim().Equals("Wheel"))
        {
            if (equippedWheel != null)
            {
                itemsCarried.Add(equippedWheel);
            }
            equippedWheel = equipItem;
            itemsCarried.Remove(equipItem);
            equip2.GetComponent<ItemSlot>().AddItem(equipItem);
            PopulateAllItemSlots();
            return true;
        }
        else
            return false;
    }

    void Update()
    {
    }

    public void PopulateAllItemSlots()
    {
        int reach = itemsCarried.Count;
        if (reach > allSlots)
        {
            reach = allSlots;
        }
        Debug.Log("items carried: " + reach);
        for (int i = 0; i < reach; i++)
        {
            if (itemsCarried[i] != null)
            {
                slot[i].GetComponent<ItemSlot>().AddItem(itemsCarried[i]);
            }
        }
    }
}
