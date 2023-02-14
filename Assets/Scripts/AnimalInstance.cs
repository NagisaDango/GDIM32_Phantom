using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public abstract class AnimalInstance : MonoBehaviour, IGroupable
{
    public Player Owner { get; set; }
    public IGroupable ParentGroup { get; set; } 
    public string DisplayName { get;  set; }
    public Sprite Icon { get;  set; }
    public SOAnimalDefinition.AnimalType Type { get; set; }
    public int Weight;

    public int AdultGrowthValue { get; private set; }

    private float currentGrowth = 0;
    private int currentValue;
    private List<FoodType> preferedFood;


    public void Initialize(string name, int value, SOAnimalDefinition.AnimalType type, Player owner, Sprite icon, int adultGrwothValue, List<FoodType> preferedFood)
    public void Initialize(string name, int value, SOAnimalDefinition.AnimalType type, Player owner, Sprite icon, int adultGrwothValue, int weight)
    {
        DisplayName = name;
        this.currentValue = value;
        Type = type;
        Owner = owner;
        Icon = icon;
        AdultGrowthValue = adultGrwothValue;
        this.preferedFood = preferedFood;
        Weight = weight;
    }

    public void AddGrowth(int amount) { currentGrowth += amount; }

    public float GetGrowth() { return currentGrowth; }

    public int GetWeight()
    {
        return Weight;
    }

    public int GetGrowth()
    {
        return currentGrowth;
    }

    public float GetGrowthRate()
    {
        return currentGrowth <= AdultGrowthValue ? (currentGrowth / AdultGrowthValue) : 1;
    }
    public List<FoodType> GetPreferedFood() { return preferedFood; }

    public int GetValue()
    {
        return currentValue;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        AnimalInstance animal = other.gameObject.GetComponent<AnimalInstance>();
        if (other.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            backpackManager.AddItem(this);
            backpackManager.createNewItem(thisAnimal);
            if (backpackManager.addable)
            {
                Destroy(other.gameObject);
                //other.gameObject.SetActive(false);
            }
        }
    }*/

    //public bool IsComposite()
    //{
    //    return false;
    //}

    //public void AddToGroup(List<IGroupable> toAdd)
    //{
    //    Debug.LogError("Should never be called");
    //}
    //public void RemoveFromGroup(List<IGroupable> toRemove)
    //{
    //    Debug.LogError("Should never be called");
    //}

    //public void RemoveAndDestroy(List<IGroupable> toRemove)
    //{
    //    Destroy(gameObject);
    //}

    //public List<IGroupable> GetSubGroups()
    //{
    //    Debug.LogError("Should never be called");
    //    return new List<IGroupable>();
    //}
}
    