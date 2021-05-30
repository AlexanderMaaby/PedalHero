using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int tier;
    public int value;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Wheel", fileName = "Wheel.asset")]
public class Wheel : Item
{
    public float speed;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Weapon.asset")]
public class Weapon : Item
{
    public int damage;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Items/Helm", fileName = "Helm.asset")]
public class Helm : Item
{
    public int armor;
}