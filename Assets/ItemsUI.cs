using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour
{
    public Button openItems;
    public Button closeItems;
    public Button item1Btn;
    public Button item2Btn;
    public Button item3Btn;

    public Text item1txt;
    public Text item2txt;
    public Text item3txt;

    public GameObject ItemUI;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        openItems.onClick.AddListener(OpenItemClick);
        closeItems.onClick.AddListener(CloseItemClick);
        item1Btn.onClick.AddListener(ItemOneClick);
        item2Btn.onClick.AddListener(ItemTwoClick);
        item3Btn.onClick.AddListener(ItemThreeClick);
        item1txt.text = ""+ PlayerPrefs.GetInt("healthpotamount");
        item2txt.text = "" + PlayerPrefs.GetInt("manapotamount");
        item3txt.text = "" + PlayerPrefs.GetInt("powerpotamount");
    }

    private void ItemThreeClick()
    {
        int temp = PlayerPrefs.GetInt("powerpotamount");
        if (temp > 0)
        {
            temp -= 1;
            player.DrinkPotion(false, false, true);
            PlayerPrefs.SetInt("powerpotamount", temp);
            item3txt.text = "" + temp;
        }
        ItemUI.SetActive(false);
    }

    private void ItemTwoClick()
    {
        int temp = PlayerPrefs.GetInt("manapotamount");
        if (temp > 0)
        {
            temp -= 1;
            player.DrinkPotion(false, true, false);
            PlayerPrefs.SetInt("manapotamount", temp);
            item2txt.text = "" + temp;
        }
        ItemUI.SetActive(false);
    }

    private void ItemOneClick()
    {
        int temp = PlayerPrefs.GetInt("healthpotamount");
        if (temp > 0)
        {
            temp -= 1;
            player.DrinkPotion(true, false, false);
            PlayerPrefs.SetInt("healthpotamount", temp);
            item1txt.text = "" + temp;
        }
        ItemUI.SetActive(false);
    }

    private void CloseItemClick()
    {
        ItemUI.SetActive(false);
    }

    private void OpenItemClick()
    {
        ItemUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
