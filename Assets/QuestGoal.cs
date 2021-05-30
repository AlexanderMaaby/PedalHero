using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public QuestGoal(GoalType goalType, int requiredAmount)
    {
        this.goalType = goalType;
        this.requiredAmount = requiredAmount;
    }

    public QuestGoal(QuestGoal copy)
    {
        this.goalType = copy.goalType;
        this.requiredAmount = copy.requiredAmount;
    }

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }

    public void DistanceAdd(int distance)
    {
        if (goalType == GoalType.Distance)
            currentAmount += distance;
    }

    public void GoldEarned(int gold)
    {
        if (goalType == GoalType.GoldEarn)
            currentAmount += gold;
    }

}

public enum GoalType
{
    Distance,
    GoldEarn,
    Kill
}