using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
//Shengjie Zhang

public class Player : MonoBehaviour
{
    [SerializeField] private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] private List<FoodInstance> foods = new List<FoodInstance>();

    [SerializeField] private BackpackManager backpack;

    public bool InFarm { get; private set; } // in trigger area for Farm panel
    public bool InShop { get; private set; } // in trigger area for Ship panel

    public bool InPanel { get; set; } // still viewing panel




    [SerializeField] private Transform animalSpawnPos;


    //public int Money { get; private set; }    // use serilizeField first for easier in-enigne test 
    //public int SoyBean { get; private set; }
    //public int Insect { get; private set; }
    //public int Carrot { get; private set; }
    //public int Corn { get; private set; }
    //public int Hay { get; private set; }

    [SerializeField][Min(0)] private int money = 0;
    [SerializeField][Min(0)] private int hay = 0;
    [SerializeField][Min(0)] private int soybean = 0;
    [SerializeField][Min(0)] private int insect = 0;
    [SerializeField][Min(0)] private int carrot = 0;
    [SerializeField][Min(0)] private int corn = 0;

    public int GetMoney() { return money; }

    public void SetMoney(int money) { this.money = money; }

    public void AddMoney(int amount) { this.money += amount; }

    public void AddFoodCount(FoodType type, int amount)
    {
        switch (type)
        {
            case FoodType.Hay:
                hay += amount;
                break;
            case FoodType.SoyBean:
                soybean += amount;
                break;
            case FoodType.Corn:
                corn += amount;
                break;
            case FoodType.Carrot:
                carrot += amount;
                break;
            case FoodType.Insect:
                insect += amount;
                break;
        }
    }

    public int GetFoodCount(FoodType type)
    {
        switch (type)
        {
            case FoodType.Hay:
                return hay;
                break;
            case FoodType.SoyBean:
                return soybean;
                break;
            case FoodType.Corn:
                return corn;
                break;
            case FoodType.Carrot:
                return carrot;
                break;
            case FoodType.Insect:
                return insect;
                break;
        }
        return -1;
    }

    public List<AnimalInstance> GetAnimals() { return animals; }


    public void BuyItem(SOAnimalDefinition animalDef) // buy animal
    {
        AnimalInstance animalInst = animalDef.Spawn(this, animalSpawnPos.position, animalSpawnPos.rotation);
        animalInst.Initialize(animalDef.GetName(), animalDef.GetSellValue(), animalDef.GetAnimalType(), this, animalDef.GetIcon(), animalDef.GetAdultGrowthValue(), animalDef.GetPreferedFood(),animalDef.GetWeight());
        animalInst.CloseTriggerCollider();
        animals.Add(animalInst);

        money -= animalDef.GetCost();
    }

    public void BuyItem(SOFoodDefinition foodDef) // buy food
    {
        FoodInstance foodInst = foodDef.Spawn();

        foodInst.Initialize(foodDef.GetName(), foodDef.GetCost(), foodDef.GetFoodType(), this, foodDef.GetIcon(), foodDef.GetGrowValue());

        foods.Add(foodInst);

        switch (foodDef.GetFoodType())
        {
            case FoodType.Hay:
                hay++;
                break;
            case FoodType.SoyBean:
                soybean++;
                break;
            case FoodType.Corn:
                corn++;
                break;
            case FoodType.Carrot:
                carrot++;
                break;
            case FoodType.Insect:
                insect++;
                break;
        }

        money -= foodDef.GetCost();
    }


    public void RemoveAnimal(AnimalInstance animal)
    {
        animals.Remove(animal);
        Destroy(animal.gameObject);
    }

    public void StoreToFarm(AnimalInstance animal) //store the animals in inventory back to farm if the player is in farm
    {
        if (InFarm)
        {
            animals.Add(animal);
            animal.transform.position = animalSpawnPos.position;
            animal.gameObject.SetActive(true);
            animal.CloseTriggerCollider();
        }
    }

    //see if the player is in the farm or Shop range
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Farm"))
        {
            InFarm = true;
        }
        if (other.gameObject.CompareTag("Shop"))
        {
            InShop = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Farm"))
        {
            InFarm = false;
        }
        if (other.gameObject.CompareTag("Shop"))
        {
            InShop = false;
        }
    }
}
