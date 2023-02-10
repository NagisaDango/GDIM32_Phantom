using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalInstance : MonoBehaviour, IGroupable
{
    public Player Owner { get; set; }
    public IGroupable ParentGroup { get; set; } 
    public string DisplayName { get; private set; }

    public Sprite Icon { get; private set; }

    public SOAnimalDefinition.AnimalType Type { get; private set; }



    private int CurrentValue;


    public void Initialize(string name, int cost,SOAnimalDefinition.AnimalType type, Player owner, Sprite icon)
    {
        DisplayName = name;
        CurrentValue = cost;
        Type = type;
        Owner = owner;
        Icon = icon;
    }

    public int GetValue()
    {
        return CurrentValue;
    }
    public bool IsComposite()
    {
        return false;
    }

    public void AddToGroup(List<IGroupable> toAdd)
    {
        Debug.LogError("Should never be callee");
    }
    public void RemoveFromGroup(List<IGroupable> toRemove)
    {
        Debug.LogError("Should never be callee");
    }

    public void RemoveAndDestroy(List<IGroupable> toRemove)
    {
        Destroy(gameObject);
    }

    public List<IGroupable> GetSubGroups()
    {
        Debug.LogError("Should never be callee");
        return new List<IGroupable>();
    }
}
    