using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float timeRemaining = 1200;
    private int minutesTotal = 1200;
    private int monstersKilled = 0;
    private int xpEarned = 0;
    public bool timerIsRunning = false;
    public Text timeText;
    public Transform player;
    public GameObject thisPlayer;
    public int damage;
    public int level;
    private int maxMana;
    public int mana;
    private int maxHealth;
    public int health;
    public int armor;
    public int critChance;
    public int powerModifier;
    private int ftp;
    private int roadEventNum;
    public float tempSpeedBoost;
    private int targetPowerInt;

    //AnimationStuff
    public Animator animator;

    public Healthbar healthBar;
    public Healthbar manaBar;

    //RoadsideEventUI
    public Button Option1;
    public Button Option2;
    public Text ButtonText1;
    public Text ButtonText2;
    public Text DescriptionText;
    public GameObject roadEventUI;
    private int roadSideIterator;
    private int combatIterator;
    public GameObject popUpUI;
    public Text popUpText;

    //CombatStuff
    public GameObject[] objectsToSpawn;
    public GameObject targetEnemy;
    public float distanceToSpawnFromPlayer;
    public Vector2 xRange;
    public Vector2 yRange;
    public GameObject combatText;
    private string enemyName;
    private bool playerStoppedCombat;

    //CombatUI
    public Button attackButton;
    public GameObject combatUI;
    public Button spellOneBtn;
    public Button spellTwoBtn;
    public Button spellThreeBtn;

    //connection stuff ANT+
    public GameObject fitnessStuff;
    public Text wattTxt;
    public Text distanceTxt;
    public Text speedTxt;
    public FitnessEquipmentDisplay test;

    //Training related stuff
    public Text targetPower;
    public ExpeditionRewards expRewards;

    //Quest related
    public GameObject questGiver;

    //Effects related objects
    public GameObject batsFlying;

    //EndMenuStuff
    public GameObject endMenu;
    public Text distanceTravTxt;
    public Text enemiesSlainTxt;
    public Text experienceEarnedTxt;
    private bool gameIsDone;

    //SettingsUI
    public GameObject settingsMenu;
    public Button intensityUp;
    public Button intensityDown;
    public Button settingsButton;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        Option1.onClick.AddListener(Option1OnClick);
        Option2.onClick.AddListener(Option2OnClick);
        attackButton.onClick.AddListener(AttackButtonClick);
        spellOneBtn.onClick.AddListener(SpellOneClick);
        spellTwoBtn.onClick.AddListener(SpellTwoClick);
        spellThreeBtn.onClick.AddListener(SpellThreeClick);
        settingsButton.onClick.AddListener(SettingsClick);
        intensityUp.onClick.AddListener(IntensityUpClick);
        intensityDown.onClick.AddListener(IntensityDownClick);
        //Set all player values from playeprefs history
        level = (PlayerPrefs.GetInt("level") / 500) + 1;
        health = 100 + (level * 10);
        maxHealth = 100 + (level * 10);
        critChance = level;
        mana = level * 30;
        maxMana = level * 30;
        armor = 0;
        damage = 20 + (level * 2);
        if (health < 50)
        {
            health = 50;
            maxHealth = 50;
        }
        if (mana < 100)
        {
            mana = 100;
            maxMana = 100;
        }
        //set Health in the healthbar etc
        healthBar.SetMaxHealth(health);
        manaBar.SetMaxHealth(mana);
        //Generate a random amount of road side events
        roadSideIterator = UnityEngine.Random.Range(500, 1000);
        combatIterator = roadSideIterator +500;
        ftp = PlayerPrefs.GetInt("playerftp");
        fitnessStuff.GetComponent<FitnessEquipmentDisplay>().SetTrainerUserConfiguration(7, PlayerPrefs.GetInt("playerweight"));
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver");
        targetPowerInt = (int)(ftp * 0.8);
    }

    private void IntensityDownClick()
    {
        targetPowerInt -= 5;
        fitnessStuff.GetComponent<FitnessEquipmentDisplay>().SetTrainerTargetPower(targetPowerInt);
        targetPower.text = "" + targetPowerInt;
    }

    private void IntensityUpClick()
    {
        targetPowerInt += 5;
        fitnessStuff.GetComponent<FitnessEquipmentDisplay>().SetTrainerTargetPower(targetPowerInt);
        targetPower.text = "" + targetPowerInt;
    }

    private void SettingsClick()
    {
        if (!settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(true);
        }
        else
            settingsMenu.SetActive(false);
    }

    private void SpellThreeClick()
    {
        //fitnessStuff.GetComponent<FitnessEquipmentDisplay>().SetTrainerTargetPower(180);
        if (mana >= 200)
        {
            CastSpell(3, 200);
        }
        //targetPower.text = "" + 180;
    }

    private void SpellTwoClick()
    {
        //fitnessStuff.GetComponent<FitnessEquipmentDisplay>().SetTrainerTargetPower(135);
        if (mana >= 100)
        {
            CastSpell(2, 100);

        }
       // targetPower.text = "" + 135;
    }

    private void SpellOneClick()
    {
        //fitnessStuff.GetComponent<FitnessEquipmentDisplay>().SetTrainerTargetPower(90);
        // targetPower.text = "" + 90;
        if (mana >= 50)
        {
            CastSpell(1, 50);
        }
    }

    private void AttackButtonClick()
    {
        //AttacksHappenHere
        //Damage random variation
        double y = damage * 0.2;
        int variation = Mathf.RoundToInt((float)y);
        Debug.Log("Variation er " + variation);
        int randomizeDamage = UnityEngine.Random.Range(damage - variation, damage + variation);
        int x = UnityEngine.Random.Range(1, 101);
        //CheckIfCrit
        if (x <= critChance)
        {
            randomizeDamage = randomizeDamage * 2;
        }
        targetEnemy.GetComponent<Enemy>().TakeDamage(randomizeDamage, 0);
        TakeDamage(targetEnemy.GetComponent<Enemy>().DealDamage());
        healthBar.SetHealth(health);
    }

    private void Option2OnClick()
    {
        DrawRoadEventRewards(roadEventNum, false);
        roadEventUI.SetActive(false);
    }

    private void Option1OnClick()
    {
        DrawRoadEventRewards(roadEventNum, true);
        roadEventUI.SetActive(false);
    }
    private void UpdateFitnessMachineText()
    {
        wattTxt.text = "Power: " + fitnessStuff.GetComponent<FitnessEquipmentDisplay>().instantaneousPower +" watt";
        speedTxt.text = "Speed: " + fitnessStuff.GetComponent<FitnessEquipmentDisplay>().speed.ToString("#.0");
        distanceTxt.text = "Distance: " + fitnessStuff.GetComponent<FitnessEquipmentDisplay>().distanceTraveled + "m";
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !gameIsDone)
        {
            ExpeditionIsDone();
            gameIsDone = true;
        }
        //set speed to bike speed | This isnt perfect so might have to adjust this later
        if (!playerStoppedCombat)
        {
            float currentSpeed = fitnessStuff.GetComponent<FitnessEquipmentDisplay>().speed;
            if (currentSpeed != 0f)
            {
                thisPlayer.GetComponent<WaypointsFree.WaypointsTraveler>().MoveSpeed = currentSpeed + tempSpeedBoost;
            }
            else
            {
                thisPlayer.GetComponent<WaypointsFree.WaypointsTraveler>().MoveSpeed = 0.01f + tempSpeedBoost;
                //thisPlayer.GetComponent<WaypointsFree.WaypointsTraveler>().MoveSpeed = 0.01f;
            }
        }
        UpdateTimer();
        UpdateFitnessMachineText();
        CheckIfRoadSide();
        CheckIfCombat();

        int tempSpeed = (int)this.GetComponent<WaypointsFree.WaypointsTraveler>().MoveSpeed;
        animator.SetInteger("speed",tempSpeed);
        if (targetEnemy != null)
        {
            Combat();
        }

        //all this input is for testing, and does not exist on mobile app, therefore might not need to be removed
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            DrawRoadSideEvent();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            StartCombat();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            targetEnemy.GetComponent<Enemy>().TakeDamage(200, 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            GameObject bats = Instantiate(batsFlying, this.transform);
            Destroy(bats, 5);
        }
        //If combat is active keep it going.
    }

    private void UpdateTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                //Time has ran out and the expedition is over
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                ExpeditionIsDone();
            }
        }
    }

    private void ExpeditionIsDone()
    {
        int x = PlayerPrefs.GetInt("totalDistanceRide");
        x += fitnessStuff.GetComponent<FitnessEquipmentDisplay>().distanceTraveled;
        PlayerPrefs.SetInt("totalDistanceRide", x);
        questGiver.GetComponent<QuestGiver>().questActive.questGoal.DistanceAdd(x);
        expRewards.SubmitAllRewards();
        x = PlayerPrefs.GetInt("monstersKilled");
        x += monstersKilled;
        PlayerPrefs.SetInt("monstersKilled", x);
        x = PlayerPrefs.GetInt("totalTimeRide");
        x += minutesTotal;
        PlayerPrefs.SetInt("totalTimeRide", x);
        int o = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("level", o + xpEarned);
        //ShowIsFInishedGUI
        distanceTravTxt.text = "Distance travelled: " + fitnessStuff.GetComponent<FitnessEquipmentDisplay>().distanceTraveled;
        enemiesSlainTxt.text = "Enemies slain : " + monstersKilled;
        experienceEarnedTxt.text = "Experience earned: " + xpEarned;
        endMenu.SetActive(true);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartCombat()
    {
        //Spawn the enemy
        int x = UnityEngine.Random.Range(0, objectsToSpawn.Length);
        enemyName = objectsToSpawn[x].name;
        Spawn(objectsToSpawn[x]);
        //Stop the cycling 
        thisPlayer.GetComponent<WaypointsFree.WaypointsTraveler>().MoveSpeed = 0f;
        playerStoppedCombat = true;
        //Show the battle UI
        combatUI.SetActive(true);
    }
    public void Combat()
    {
        //Close the battle UI
        //Start cycling
        //Destroy the enemy if dead
        if (targetEnemy.GetComponent<Enemy>().IsDead())
        {
            int enemyTier = targetEnemy.GetComponent<Enemy>().tier;
            DrawPrize(enemyTier);
            xpEarned += targetEnemy.GetComponent<Enemy>().xp;
            Destroy(targetEnemy);
            combatUI.SetActive(false);
            thisPlayer.GetComponent<WaypointsFree.WaypointsTraveler>().MoveSpeed = 5f;
            playerStoppedCombat = false;
            //reset any power pot
            powerModifier = 1;
            monstersKilled++;
            questGiver.GetComponent<QuestGiver>().questActive.questGoal.EnemyKilled();
        }
    }

    private void DrawPrize(int tier)
    {
        expRewards.getRandomTreasure(tier);
    }
    //Spawn an enemy and set it as the players target
    void Spawn(GameObject spawnObject)
    {
        Vector3 spawnPos = player.position;
        Vector3 playerDirection = player.transform.forward;
        //spawnPos.y += 6;
       // spawnPos.z += distanceToSpawnFromPlayer;
        Vector3 spawnPlace = spawnPos + playerDirection * distanceToSpawnFromPlayer;
        Instantiate(spawnObject, spawnPlace, Quaternion.identity);
        Debug.Log(enemyName +"(Clone)");
        targetEnemy = GameObject.FindGameObjectWithTag("enemy");
        targetEnemy.transform.rotation = player.transform.rotation;
    }
    public void DrinkPotion(bool hp, bool manaBool, bool power)
    {
        if (hp)
        {
            health += maxHealth / 3;
        }
        if (manaBool)
        {
            mana += maxMana / 3;
        }
        if (power)
        {
            powerModifier = 2;
        }
        healthBar.SetHealth(health);
        manaBar.SetHealth(mana);
    }

    public void TakeDamage(int damage)
    {
        int temp = damage - armor;
        if (temp > 0)
        {
            health -=  temp;
        }
        if (temp <= 0 )
        {
            temp = 0;
            combatText.GetComponent<Text>().color = Color.gray;
        }
        else
        {
            combatText.GetComponent<Text>().color = Color.yellow;
        }
        combatText.GetComponent<Text>().text = "" + temp;
        combatText.SetActive(true);
        Invoke("HideCombatText", 1);
    }
    private void HideCombatText()
    {
        combatText.SetActive(false);
    }
    public void CheckIfRoadSide()
    {
        if (roadSideIterator < fitnessStuff.GetComponent<FitnessEquipmentDisplay>().distanceTraveled)
        {
            DrawRoadSideEvent();
            roadSideIterator += 500;
        }
    }

    public void CheckIfCombat()
    {
        if (combatIterator < fitnessStuff.GetComponent<FitnessEquipmentDisplay>().distanceTraveled)
        {
            StartCombat();
            combatIterator += 650;
        }
    }

    public void DrawRoadSideEvent()
    {
        int number = UnityEngine.Random.Range(1, 11);
        roadEventNum = number;
        string description = "";
        string button1 = "";
        string button2 = ""; 

        switch (number)
            {
            case 1:
                description = "By the side of the road you see a hitchhiker, would you stop to help even if you are raiding a bicycle?";
                button1 = "Of course";
                button2 = "No way";
                break;
            case 2:
                description = "You see a bottle of red goo by the side of the road, do you pick it up?";
                button1 = "Yummy!";
                button2 = "No iew";
                break;
            case 3:
                description = "A tusken raider stands in the middle of the road, what do you do?";
                button1 = "Yell really loud";
                button2 = "Bike straight at it";
                break;
            case 4:
                description = "At the side of the road you see a piece of paper, it seems to be a note of some sort.";
                button1 = "Read it";
                button2 = "Leave it";
                break;
            case 5:
                description = "You come upon a strange golden shrine that looks like a place of worship.";
                button1 = "Pray";
                button2 = "Desecrate";
                break;
            case 6:
                description = "There are two merchants at the side of the road, one at the left and one at the right.";
                button1 = "Left (200g)";
                button2 = "Right (300g)";
                break;
            case 7:
                description = "You come upon a cyclist laying lifeless as the side of the road.";
                button1 = "Check";
                button2 = "Ride on";
                break;
            case 8:
                description = "From the sky emerges a banana and a donut attached to fishing lines.";
                button1 = "Take banana";
                button2 = "Take donut";
                break;
            case 9:
                description = "A statue of a big warrior stands tall, with a spot for a offering.";
                button1 = "Offer 100 gold";
                button2 = "Offer 200 gold";
                break;
            case 10:
                description = "A small cave is located next to the road.";
                button1 = "Explore";
                button2 = "Ignore";
                break;
            default:
                description = "This event should not exist, yet it does?! Weird!";
                button1 = "Weird!";
                button2 = "Very!";
                break;
            }
        ButtonText1.text = button1;
        ButtonText2.text = button2;
        DescriptionText.text = description;

        roadEventUI.SetActive(true);
    }

    private void DrawRoadEventRewards(int rewardNum, bool clickedYes)
    {
        String text = "";
        if (clickedYes)
        {
          
            switch(rewardNum)
            {
                case 1:
                    text = "As you slow down, the hitchhiker rips off a costume and reveals itself as a monster. Prepare to fight!";
                    StartCombat();
                    break;
                case 2:
                    text = "As the liquid goop touch your tongue the foulest taste you could ever imagine hits you. You lose 10% health, but somehow regain 50% mana";
                    health -= maxHealth / 10;
                    mana += maxMana / 2;
                    break;
                case 3:
                    text = "The tusken raider leaves, but as it does you hear a voice saying. 'Sand people are easily startled, but they will soon be back, in greater numbers'";
                    break;
                case 4:
                    text = "It's a shopping list for milk, bread and mana potions, somehow in your own handwriting. Also stuck to the list is 200 gold";
                    expRewards.GetSpecificReward(200, 0, 0, 0);
                    break;
                case 5:
                    text = "As you sit down and pray 100 gold appears out of nowhere.";
                    expRewards.GetSpecificReward(100, 0, 0, 0);
                    break;
                case 6:
                    if (expRewards.GetGold() >= 200)
                    {
                        text = "You buy a bunch of underpriced health potions for 200 gold";
                        int x = PlayerPrefs.GetInt("healthpotamount");
                        PlayerPrefs.SetInt("healthpotamount", x + 4);
                        expRewards.ReduceGold(200);
                    }
                    else
                    {
                        text = "You can't afford anything and the merchant shoos you away!";
                    }
                    break;
                case 7:
                    text = "The cyclist is dead, and what killed it comes upon you. Fight!";
                    StartCombat();
                    break;
                case 8:
                    text = "Banana is a good choice some voice says, you gain some resources";
                    expRewards.getRandomTreasure(2);
                    break;
                case 9:
                    if (expRewards.GetGold() >= 100)
                    {
                        text = "The god thanks you for your offering, you obtain some resources.";
                        expRewards.ReduceGold(100);
                        expRewards.getRandomTreasure(3);
                    }
                    else
                    {
                        text = "You can't offer what you don't have, the god is angry.";
                    }
                    break;
                case 10:
                    text = "The cave is filled with bats who start chasing you, your speed increases briefly.";
                    SpawnBats();
                    Invoke("SetSpeedBoostToZero", 10);
                    Invoke("SpawnBats", 5);
                    tempSpeedBoost = 3f;
                    break;
                default:
                    text = "Dette er en test på case default KNAPP YEP";
                    break;
            }
        }
        else
        {
            switch (rewardNum)
            {
                case 1:
                    text = "As you ride past in a hurry the hitchhiker reveals to be a monster. Smart choice, you gain some experience points.";
                    int x = PlayerPrefs.GetInt("level");
                    PlayerPrefs.SetInt("level", x + 300);
                    break;
                case 2:
                    text = "Tossing the bottle away was probably a good decision, as it hits the ground the liquid burns into the ground.";
                    break;
                case 3:
                    text = "The tusken raider leaves, but as it does you hear a voice saying. 'Sand people are easily startled, but they will soon be back, in greater numbers'";
                    break;
                case 4:
                    text = "A piece of paper means nothing to you as you fly by, efficiency is key. ";
                    break;
                case 5:
                    text = "You desecrate the shrine, finding riches worth 300 gold. But the actions haunt you, reducing your damage slightly.";
                    expRewards.GetSpecificReward(300, 0, 0, 0);
                    damage -= 1;
                    break;
                case 6:
                    if (expRewards.GetGold() >= 300)
                    {
                        text = "You buy a bunch of underpriced mana potions for 300 gold";
                        int y = PlayerPrefs.GetInt("manapotamount");
                        PlayerPrefs.SetInt("manapotamount", y + 4);
                        expRewards.ReduceGold(300);
                    }
                    else
                    {
                        text = "You can't afford anything and the merchant shoos you away!";
                    }
                    break;
                case 7:
                    text = "You pedal away quickly, the cyclist looks dead anyway...";
                    break;
                case 8:
                    text = "You knew deep inside donut was the wrong choice. You gain nothing, but more calories to burn.";
                    break;
                case 9:
                    if (expRewards.GetGold() >= 200)
                    {
                        text = "The god tanks you for your offering, you obtain some resources.";
                        expRewards.ReduceGold(200);
                        expRewards.getRandomTreasure(4);
                    }
                    else
                    {
                        text = "You can't offer what you don't have, the god is angry.";
                    }
                    break;
                case 10:
                    text = "Ignoring the cave doesn't help, a flock of bats starts chasing you. Increasing your speed.";
                    tempSpeedBoost = 2f;
                    SpawnBats();
                    Invoke("SetSpeedBoostToZero", 10);
                    Invoke("SpawnBats", 5);
                    break;
                default:
                    text = "Dette er en test på case default KNAPP NP";
                    break;
            }
        }
        popUpText.text = text;
        popUpUI.SetActive(true);
        Invoke("HidePopUpUI", 4);
    }
    private void HidePopUpUI()
    {
        popUpUI.SetActive(false);
    }

    private void SpawnBats()
    {
        GameObject bats = Instantiate(batsFlying, this.transform);
        Destroy(bats, 5);
    }

    private void SetSpeedBoostToZero()
    {
        tempSpeedBoost = 0;
    }

    private void CastSpell(int tier, int manaCost)
    {
        //AttacksHappenHere
        //Damage random variation
        double y = damage * 0.2;
        int variation = Mathf.RoundToInt((float)y);
        Debug.Log("Variation er " + variation);
        int randomizeDamage = UnityEngine.Random.Range(damage - variation, damage + variation);
        int x = UnityEngine.Random.Range(1, 101);
        randomizeDamage += (randomizeDamage / 2) * tier;
        //CheckIfCrit
        if (x <= critChance)
        {
            randomizeDamage = randomizeDamage * 2;
        }
        targetEnemy.GetComponent<Enemy>().TakeDamage(randomizeDamage, 2);
        TakeDamage(targetEnemy.GetComponent<Enemy>().DealDamage());
        healthBar.SetHealth(health);
        mana -= manaCost;
        manaBar.SetHealth(mana);
    }
}
