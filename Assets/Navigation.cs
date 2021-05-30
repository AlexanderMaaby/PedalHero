using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    public Button questButton;
    public Button shopButton;
    public Button achievButton;
    public Button settingsButton;
    public Button settingsCloseButton;
    public Button statsButton;
    public Button statsCloseButton;
    //Stats related Text
    public GameObject statsUImenu;
    public GameObject questsUI;
    public Text stat1;
    public Text stat2;
    public Text stat3;
    public Text stat4;
    public GameObject settingsUI;
    public GameObject shopUI;
    public GameObject equipmentNavigation;
    public Button equipmentBtn;
    //settingsRelated
    public Text weightTxt;
    public Text ftptext;
    public Button confirmSettings;

    // Start is called before the first frame update
    void Start()
    {
        shopButton.onClick.AddListener(ShopOnClick);
        settingsButton.onClick.AddListener(SettingsOnClick);
        settingsCloseButton.onClick.AddListener(SettingsCloseClick);
        confirmSettings.onClick.AddListener(SettingsConfirmedClick);
        statsButton.onClick.AddListener(StatsOnClick);
        statsCloseButton.onClick.AddListener(StatsCloseClick);
        questButton.onClick.AddListener(QuestOnClick);
        equipmentBtn.onClick.AddListener(EquipmentOnClick);
    }

    private void EquipmentOnClick()
    {
        equipmentNavigation.SetActive(true);
    }

    private void QuestOnClick()
    {
        questsUI.SetActive(true);
    }

    private void StatsCloseClick()
    {
        statsUImenu.SetActive(false);
    }

    private void StatsOnClick()
    {
        stat1.text = "Experience earned: " + PlayerPrefs.GetInt("level") + "xp";
        stat2.text = "Total distance: " + PlayerPrefs.GetInt("totalDistanceRide") + "m";
        stat3.text = "Total time spent on expedition: " + PlayerPrefs.GetInt("totalTimeRide") + " minutes";
        stat4.text = "Monsters killed: " + PlayerPrefs.GetInt("monstersKilled");
        statsUImenu.SetActive(true);
    }

    private void SettingsConfirmedClick()
    {
        
        int weight = int.Parse(weightTxt.text);
        int ftp = int.Parse(ftptext.text);
        PlayerPrefs.SetInt("playerweight", weight);
        PlayerPrefs.SetInt("playerftp", ftp);
    }

    private void SettingsOnClick()
    {
        settingsUI.SetActive(true);
    }

    private void SettingsCloseClick()
    {
        settingsUI.SetActive(false);
    }

    private void ShopOnClick()
    {
        shopUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
