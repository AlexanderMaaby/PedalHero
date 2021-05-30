using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpeditionRewards : MonoBehaviour
{
    public Text goldTxt;
    public Text lumberTxt;
    public Text ironTxt;
    public Text foodTxt;

    private int gold;
    private int lumber;
    private int iron;
    private int food;

    public GameObject questGiver;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("getRandomReward", 5f, 5f);
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getRandomReward()
    {
        int x = Random.Range(1, 11);
        switch (x) {
            case 1:
                lumber += Random.Range(1, 3);
                break;
            case 2:
                iron += Random.Range(1, 3);
                break;
            case 3:
                food += Random.Range(1, 3);
                break;
            default:
                gold += Random.Range(1, 20);
                break;
        }
        UpdateAllText();
    }

    public void getRandomTreasure(int tier)
    {
        int x = Random.Range(1, 11);
        switch (x)
        {
            case 1:
                lumber += (Random.Range(1, 3)) * tier;
                break;
            case 2:
                iron += (Random.Range(1, 3)) * tier;
                break;
            case 3:
                food += (Random.Range(1, 3)) * tier;
                break;
            case 4 - 5:
                int y = PlayerPrefs.GetInt("healthpotamount");
                y += 1 * tier;
                PlayerPrefs.SetInt("healthpotamount", y);
                break;
            default:
                gold += (Random.Range(1, 20) + 10) * tier;
                break;
        }
        gold += 10 * tier;
        UpdateAllText();
    }

    public void GetSpecificReward(int gold, int wood, int iron, int food)
    {
        this.gold += gold;
        lumber += wood;
        this.iron += iron;
        this.food += food;
        UpdateAllText();
    }

    public void SubmitAllRewards()
    {
        int x = PlayerPrefs.GetInt("playerGold");
        x += gold;
        PlayerPrefs.SetInt("playerGold", x);
        questGiver.GetComponent<QuestGiver>().questActive.questGoal.GoldEarned(x);
        x = PlayerPrefs.GetInt("playerWood");
        x += lumber;
        PlayerPrefs.SetInt("playerWood", x);
        x = PlayerPrefs.GetInt("playerIron");
        x += iron;
        PlayerPrefs.SetInt("playerIron", x);
        x = PlayerPrefs.GetInt("playerFood");
        x += food;
        PlayerPrefs.SetInt("playerFood", x);
    }

    public void ReduceGold(int goldReduce)
    {
        gold -= goldReduce;
        UpdateAllText();
    }


    public int GetGold()
    {
        return gold;
    }

    private void UpdateAllText()
    {
        goldTxt.text = "Gold: " + gold;
        lumberTxt.text = "Wood: " + lumber;
        ironTxt.text = "Iron: " + iron;
        foodTxt.text = "Food: " + food;
    }
}
