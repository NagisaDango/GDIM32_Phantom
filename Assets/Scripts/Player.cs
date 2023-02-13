using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] private Transform animalSpawnPos;

    public int Money { get; private set; }
    public int SoyBean { get; private set; }
    public int Insect { get; private set; }
    public int Carrot { get; private set; }
    public int Corn { get; private set; }
    public int Hay { get; private set; }

    //[SerializeField] private int hay = 0;
    //[SerializeField] private int soybean = 0;
    //[SerializeField] private int insect = 0;
    //[SerializeField] private int carrot = 0;
    //[SerializeField] private int corn = 0;


    public void EarnMoney(int amount)
    {
        Money += amount;
    }

    public void BuyAnimal(SOAnimalDefinition animalDef)
    {
        AnimalInstance animalInst = animalDef.Spawn(this, animalSpawnPos);
        animalInst.Initialize(animalDef.GetName(), animalDef.GetCost(), animalDef.GetAnimalType(), this, animalDef.GetIcon());
        animals.Add(animalInst);
    }



}
