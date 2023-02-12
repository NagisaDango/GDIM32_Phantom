using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] private Transform animalSpawnPos;



    public void EarnMoney(int amount)
    {

    }

    public void BuyAnimal(SOAnimalDefinition animalDef)
    {
        AnimalInstance animalInst = animalDef.Spawn(this, animalSpawnPos);
        animalInst.Initialize(animalDef.GetName(), animalDef.GetCost(), animalDef.GetAnimalType(), this, animalDef.GetIcon());
        animals.Add(animalInst);
    }



}
