using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int armor;
    public int xp;
    public int tier;
    public GameObject floatingText;
    public GameObject fireDamage;
    public GameObject powDamage;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("level"))
        {
            int playerLevel = (PlayerPrefs.GetInt("level") / 500) + 1;
            health = (20 * tier) + (playerLevel * 10);
            damage = 3 + (1 * tier);
            armor = 0;
            xp = (tier * playerLevel) + 200;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
            return false;
    }

    public void TakeDamage(int dmg, int damagetype)
    {
        int temp = dmg - armor;
        health -= temp;
        spawnText(temp);
        if (damagetype == 0)
        {
            GameObject text = Instantiate(powDamage, this.transform);
        }
        else
        {
            GameObject text = Instantiate(fireDamage, this.transform);
        }
    }
    public int DealDamage()
    {
        int variation = (damage / 100) * 20;
        int realdamage = Random.Range(damage - variation, damage + variation);
        return realdamage;
    }
    public int GetTier()
    {
        return tier;
    }
    public int GetXp()
    {
        return xp;
    }
    //soawn comebat text?
    void spawnText(int damage)
    {
        Transform thisTransform = this.transform;
        GameObject text = Instantiate(floatingText, this.transform);
        text.GetComponent<TextMesh>().text = "" + damage;
        Destroy(text, 1);
    }
}
