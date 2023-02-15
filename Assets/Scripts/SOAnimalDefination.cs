using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AnimalDefinition")]
public class SOAnimalDefinition : ScriptableObject 
{
    public enum AnimalType
    {
       Cow,
       Pig,
       Sheep,
       Chiken,
       Duck
    }

    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int displayCost;
    [SerializeField] private int sellValue;
    [SerializeField] private string displayDescription;
    [SerializeField] private AnimalInstance prefab;
    [SerializeField] private AnimalType type;
    [SerializeField] private int adultGrowthValue;
    [SerializeField] private int weight;

    [SerializeField] private List<FoodType> preferedFood;

    public AnimalInstance Spawn(Player owner, Transform location)
    {
        AnimalInstance instance = Instantiate(prefab, location.position, location.rotation);
        return instance;
    }

    public string GetName() { return displayName; }
    public Sprite GetIcon() { return icon; }
    public int GetCost() { return displayCost; }
    public int GetSellValue() { return sellValue; }
    public string GetDescription() { return displayDescription; }
    public AnimalType GetAnimalType() { return type; }
    public int GetAdultGrowthValue() { return adultGrowthValue; }
    public List<FoodType> GetPreferedFood() { return preferedFood; }
    public int GetWeight() { return weight; }

}
