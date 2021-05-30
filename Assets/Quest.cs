using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal questGoal;

    public Quest(int xcount)
    {
        switch (xcount)
        {
            case 0 - 1:
                title = "Ride the lightning";
                description = "Get up to speed by riding for 10km in Expedition mode.";
                experienceReward = 500;
                goldReward = 1000;
                questGoal = new QuestGoal(GoalType.Distance, 10000);
                break;
            case 2:
                title = "Ride the extreme lightning ";
                description = "Get up to speed by riding for 20km in Expedition mode.";
                experienceReward = 1200;
                goldReward = 2500;
                questGoal = new QuestGoal(GoalType.Distance, 20000);
                break;
            case 3:
                title = "Monster Hunter";
                description = "Kill 10 enemies during expedition";
                experienceReward = 800;
                goldReward = 800;
                questGoal = new QuestGoal(GoalType.Kill, 10);
                break;
            case 4:
                title = "Monster Hunter";
                description = "Kill 20 enemies during expedition";
                experienceReward = 1800;
                goldReward = 1800;
                questGoal = new QuestGoal(GoalType.Kill, 20);
                break;
            case 5:
                title = "Monster Hunter Extreme";
                description = "Kill 30 enemies during expedition";
                experienceReward = 2200;
                goldReward = 2200;
                questGoal = new QuestGoal(GoalType.Kill, 30);
                break;
            case 6:
                title = "Treasure Hunter ";
                description = "Collect 500 gold during expedition";
                experienceReward = 500;
                goldReward = 500;
                questGoal = new QuestGoal(GoalType.GoldEarn, 500);
                break;
            case 7:
                title = "Treasure Hunter 2";
                description = "Collect 1000 gold during expedition";
                experienceReward = 1200;
                goldReward = 1200;
                questGoal = new QuestGoal(GoalType.GoldEarn, 1000);
                break;
            case 8:
                title = "Treasure Hunter Extreme";
                description = "Collect 2000 gold during expedition";
                experienceReward = 2500;
                goldReward = 2500;
                questGoal = new QuestGoal(GoalType.GoldEarn, 2000);
                break;
            default:
                title = "Break the extreme lightning ";
                description = "Get up to speed by riding for 30km in Expedition mode.";
                experienceReward = 3200;
                goldReward = 3500;
                questGoal = new QuestGoal(GoalType.Distance, 30000);
                break;
        }
    }
    public Quest(Quest copy)
    {
        title = copy.title;
        description = copy.description;
        experienceReward = copy.experienceReward;
        goldReward = copy.goldReward;
        questGoal = new QuestGoal(copy.questGoal);
    }

    public void Complete()
    {
        isActive = false;
    }
}
