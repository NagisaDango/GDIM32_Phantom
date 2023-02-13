using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FoodDefinition")]
public class SOFoodDefinition : ScriptableObject
{
    public enum FoodType
    {
        SoyBean,
        Insect,
        Carrot,
        Corn,
        Hay
    }

    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int displayCost;
    [SerializeField] private string displayDescription;
    [SerializeField] private FoodInstance prefab;
    [SerializeField] private FoodType type;
    [SerializeField] private int growValue;


    //public FoodInstance Spawn(Player owner, Transform location)
    //{
    //    FoodInstance instance = Instantiate(prefab, location.position, location.rotation);
    //    return instance;
    //}
    public FoodInstance Spawn()
    {
        return new FoodInstance();
    }

    public string GetName() { return displayName; }
    public Sprite GetIcon() { return icon; }
    public int GetCost() { return displayCost; }
    public string GetDescription() { return displayDescription; }
    public FoodType GetAnimalType() { return type; }
    public int GetGrowValue() { return growValue; }

    public FoodType GetType() { return type; }
}
