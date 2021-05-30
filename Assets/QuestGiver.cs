using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest questAvailable;
    public Quest questActive;

    public void Start()
    {
        int xcount = Random.Range(1, 10);
        questAvailable = new Quest(xcount);
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CompleteQuest()
    {
        if (questActive.questGoal.IsReached())
        {
            int xpReward = questActive.experienceReward;
            int goldReward = questActive.goldReward;
            int x = PlayerPrefs.GetInt("level");
            PlayerPrefs.SetInt("level",xpReward + x);
            int y = PlayerPrefs.GetInt("playerGold");
            PlayerPrefs.SetInt("playerGold", goldReward + y);
            questActive = null;
        }
    }


    public Quest GetActiveQuest()
    {
        return questActive;
    }

    public void AcceptQuest()
    {
        questActive = new Quest(questAvailable);
    }
}

