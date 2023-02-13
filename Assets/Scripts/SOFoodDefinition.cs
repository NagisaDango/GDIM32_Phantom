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
    [SerializeField] private FoodType type;

    //public AnimalInstance Spawn(Player owner, Transform location)
    //{
    //    AnimalInstance instance = Instantiate(prefab, location.position, location.rotation);
    //    return instance;
    //}

    public string GetName() { return displayName; }
    public Sprite GetIcon() { return icon; }
    public int GetCost() { return displayCost; }
    public string GetDescription() { return displayDescription; }
    public FoodType GetAnimalType() { return type; }
}
