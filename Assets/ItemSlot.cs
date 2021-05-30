using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool isEmpty;
    public Item item;
    public Image test;
    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Image frameSet;
    public Button thisBtn;
    public PlayerInfo player;
    public Sprite genericBackground;

    // Start is called before the first frame update
    void Start()
    {
        //frameSet.sprite = null;
        thisBtn.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        if (player.GetComponent<Inventory>().EquipItem(item) == true)
            {
                RemoveItem();
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item itemAdd)
    {
        item = itemAdd;
        isEmpty = false;
        UpdateAllInfo();
        switch (item.tier)
        {
            case 1:
                frameSet.sprite = frame1;
            break;
            case 2:
                frameSet.sprite = frame2;
                break;
            case 3:
                frameSet.sprite = frame3;
                break;
            default:
                frameSet.sprite = frame1;
                break;
        }
    }
    
    public void RemoveItem()
    {
        item = null;
        isEmpty = true;
        UpdateAllInfo();
        frameSet.sprite = frame1;
        player.GetComponent<Inventory>().PopulateAllItemSlots();
    }

    public void UpdateAllInfo()
    {
        if (item != null)
        {
            GetComponent<Image>().sprite = item.icon;
        }
        else
        {
            GetComponent<Image>().sprite = genericBackground;
        }
    }
}
