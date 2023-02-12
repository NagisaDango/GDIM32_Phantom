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

    [SerializeField] private string animalName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int cost;
    [SerializeField] private AnimalInstance prefab;
    [SerializeField] private AnimalType type;
    
    public AnimalInstance Spawn(Player owner, Vector3 position)
    {
        AnimalInstance instance = Instantiate(prefab,position,Quaternion.identity);
        return instance;
    }

    public string GetName() { return name; }
    public Sprite GetIcon() { return icon; }
    public int GetCost() { return cost; }
}
