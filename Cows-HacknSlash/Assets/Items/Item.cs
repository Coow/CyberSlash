using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject{
    
    public int Id;
    public string Name;
    public Sprite sprite;

    public string description;

    public int HowManyCanStack = 1;

    public enum State
    {
        standard, chestplate, leggings, helmet, boots, rings, weapon
    }

    public State current;
}