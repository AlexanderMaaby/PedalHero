using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public Text playerName;
    public Text playerLevel;
    public Text playerGoldTxt;
    public Text woodText;
    public Text foodTxt;
    public Text ironTxt;
    public Button getXp;
    public int healthPot;
    public int manaPot;
    public int powerPot;
    private GameObject questGiverObj;
    public GameObject questGiverBoing;

    public string name;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("name"))
        {
            PlayerPrefs.SetString("name", "Flammekastar");
        }
        if (!PlayerPrefs.HasKey("playerGold"))
        {
            PlayerPrefs.SetInt("playerGold", 0);
        }
        if (!PlayerPrefs.HasKey("playerWood"))
        {
            PlayerPrefs.SetInt("playerWood", 0);
        }
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 0);
        }
        if (!PlayerPrefs.HasKey("playerIron"))
        {
            PlayerPrefs.SetInt("playerIron", 0);
        }
        if (!PlayerPrefs.HasKey("playerFood"))
        {
            PlayerPrefs.SetInt("playerFood", 0);
        }
        if (!PlayerPrefs.HasKey("totalDistanceRide"))
        {
            PlayerPrefs.SetInt("totalDistanceRide", 0);
        }
        if (!PlayerPrefs.HasKey("monstersKilled"))
        {
            PlayerPrefs.SetInt("monstersKilled", 0);
        }
        if (!PlayerPrefs.HasKey("totalTimeRide"))
        {
            PlayerPrefs.SetInt("totalTimeRide", 0);
        }
        playerName.text = PlayerPrefs.GetString("name");
        playerLevel.text = "Level: " + CalculateLevel(PlayerPrefs.GetInt("level"));
        playerGoldTxt.text = "Gold: " + PlayerPrefs.GetInt("playerGold");
        woodText.text = "Wood: " + PlayerPrefs.GetInt("playerWood");
        ironTxt.text = "Iron: " + PlayerPrefs.GetInt("playerIron");
        foodTxt.text = "Food: " + PlayerPrefs.GetInt("playerFood");

        if (GameObject.FindGameObjectWithTag("QuestGiver") == null)
        {
            questGiverObj = Instantiate(questGiverBoing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            int level = PlayerPrefs.GetInt("level");
            level += 250;
            PlayerPrefs.SetInt("level", level);
            playerLevel.text = "Level: " + CalculateLevel(PlayerPrefs.GetInt("level"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            int gold = PlayerPrefs.GetInt("playerGold");
            PlayerPrefs.SetInt("playerGold", 2000);
            playerGoldTxt.text = "Gold: " + PlayerPrefs.GetInt("playerGold");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private int CalculateLevel(int experience)
    {
        return (experience / 500) + 1;
    }
}
