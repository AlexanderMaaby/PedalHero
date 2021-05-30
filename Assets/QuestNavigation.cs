using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNavigation : MonoBehaviour
{
    public GameObject theQuestMeny;
    public Button acceptButton;
    public Text descText;
    public Text titleText;
    public Text goldText;
    public Text xpText;
    public Button closeButton;

    public Text activeDescText;
    public Text activeTitleText;
    public Text activeProgressText;

    public Button questComplete;

    public QuestGiver questGiver;

    // Start is called before the first frame update
    void Start()
    {
        acceptButton.onClick.AddListener(OnAcceptclick);
        closeButton.onClick.AddListener(OnCloseClick);
        questComplete.onClick.AddListener(OnCompleteClick);
    }

    private void OnCompleteClick()
    {
        questGiver.CompleteQuest();
        UpdateAllText();
    }

    private void OnCloseClick()
    {
        theQuestMeny.SetActive(false);
    }

    private void OnAcceptclick()
    {
            questGiver.AcceptQuest();
            UpdateAllText();
    }

    private void OnEnable()
    {
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();
        Debug.Log(GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().ToString());
        UpdateAllText();
    }

    public void UpdateAllText()
    {
        descText.text = questGiver.questAvailable.description;
        titleText.text = questGiver.questAvailable.title;
        goldText.text = questGiver.questAvailable.goldReward + "";
        xpText.text = questGiver.questAvailable.experienceReward + "";
        if (questGiver.questActive != null)
        {
            activeDescText.text = questGiver.questActive.description;
            activeTitleText.text = questGiver.questActive.title;
            activeProgressText.text = "Progress: " + questGiver.questActive.questGoal.currentAmount + "/" + questGiver.questActive.questGoal.requiredAmount;
        }
        else
        {
            activeDescText.text = "No active quest";
            activeTitleText.text = "";
            activeProgressText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
