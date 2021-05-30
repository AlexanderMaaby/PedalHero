using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMarket : MonoBehaviour
{

    public Button item1;
    public Button item2;
    public Button item3;
    public Button close;
    public GameObject thisMeny;
    //ShopUI
    public Text hpPotCost;
    public Text manaPotCost;
    public Text powerPotCost;
    public Text goldUpdatedText;
    //pricesAndShopData
    private int shopLevel;
    private int healthpotcost;
    private int manapotcost;
    private int powerpotcost;
    public int playerGold;
    //cantAffordElements
    public GameObject cantAfford;
    public Button cantAffordButton;
    //upgrade
    public Button upgradeButton;
    public Text upgradeText;
    
    // Start is called before the first frame update
    void Start()
    {
        item1.onClick.AddListener(Option1OnClick);
        item2.onClick.AddListener(Option2OnClick);
        item3.onClick.AddListener(Option3OnClick);
        close.onClick.AddListener(OptionCloseClick);
        cantAffordButton.onClick.AddListener(OptionCloseThis);
        upgradeButton.onClick.AddListener(UpgradeOnClick);

        if (!PlayerPrefs.HasKey("healthpotamount"))
        {
            PlayerPrefs.SetInt("healthpotamount", 0);
        }
        if (!PlayerPrefs.HasKey("manapotamount"))
        {
            PlayerPrefs.SetInt("manapotamount", 0);
        }
        if (!PlayerPrefs.HasKey("powerpotamount"))
        {
            PlayerPrefs.SetInt("powerpotamount", 0);
        }
        if (!PlayerPrefs.HasKey("shoplevel"))
        {
            PlayerPrefs.SetInt("shoplevel", 1);
        }
    }

    private void UpgradeOnClick()
    {
        int x = PlayerPrefs.GetInt("shoplevel");
        int y = PlayerPrefs.GetInt("playerWood");
        if (y >= ((PlayerPrefs.GetInt("shoplevel") * 25) + 500))
        {
            int temp = PlayerPrefs.GetInt("shoplevel");
            PlayerPrefs.SetInt("shoplevel", temp + 1);
            PlayerPrefs.SetInt("playerWood", y - ((PlayerPrefs.GetInt("shoplevel") * 25) + 500));
        }
    }

    private void OnEnable()
    {
        playerGold = PlayerPrefs.GetInt("playerGold");
        //set the prices based on shop level
        UpdateShopPrices();
        upgradeText.text = "" + ((PlayerPrefs.GetInt("shoplevel") * 25) + 500);
    }

    private void OptionCloseThis()
    {
        cantAfford.SetActive(false);
    }

    private void OptionCloseClick()
    {
        thisMeny.SetActive(false);
    }

    //Buy a mana pot
    private void Option2OnClick()
    {
        if (playerGold > manapotcost)
        {
            playerGold -= manapotcost;
            int x = PlayerPrefs.GetInt("manapotamount");
            x++;
            PlayerPrefs.SetInt("manapotamount", x);
            PlayerPrefs.SetInt("playerGold", playerGold);
            goldUpdatedText.text = "Gold: " + playerGold;
        }
        else
        {
            //player can not afford it
            cantAfford.SetActive(true);
        }
    }
    //Buy a power pot
    private void Option3OnClick()
    {
        if (playerGold >= powerpotcost)
        {
            playerGold -= powerpotcost;
            int x = PlayerPrefs.GetInt("powerpotamount");
            x++;
            PlayerPrefs.SetInt("powerpotamount", x);
            PlayerPrefs.SetInt("playerGold", playerGold);
            goldUpdatedText.text = "Gold: " + playerGold;
        }
        else
        {
            //player can not afford it
            cantAfford.SetActive(true);
        }
    }
    //Buy a health pot
    private void Option1OnClick()
    {
        if (playerGold >= healthpotcost)
        {
            playerGold -= healthpotcost;
            int x = PlayerPrefs.GetInt("healthpotamount");
            x++;
            PlayerPrefs.SetInt("healthpotamount", x);
            PlayerPrefs.SetInt("playerGold", playerGold);
            goldUpdatedText.text = "Gold: " + playerGold;
        }
        else
        {
            //player can not afford it
            cantAfford.SetActive(true);
        }
    }

    private void UpdateShopPrices()
    {
        healthpotcost = 505 - (5 * PlayerPrefs.GetInt("shoplevel"));
        manapotcost = 755 - (5 * PlayerPrefs.GetInt("shoplevel"));
        powerpotcost = 1005 - (5 * PlayerPrefs.GetInt("shoplevel"));
        hpPotCost.text = "" + healthpotcost;
        manaPotCost.text = "" + manapotcost;
        powerPotCost.text = "" + powerpotcost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
