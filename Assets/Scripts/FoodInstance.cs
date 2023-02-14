using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInstance : IGroupable
{
    public Player Owner { get; set; }
    public IGroupable ParentGroup { get; set; }
    public string DisplayName { get; private set; }
    public Sprite Icon { get; private set; }
    public FoodType Type { get; private set; }

    private int growValue;
    private int currentValue;


    public void Initialize(string name, int value, FoodType type, Player owner, Sprite icon, int growValue)
    {
        DisplayName = name;
        this.currentValue = value;
        Type = type;
        Owner = owner;
        Icon = icon;
        this.growValue = growValue;
    }


    public int GetValue() { return currentValue; }
    public int GetGrowValue() { return growValue; }
}
