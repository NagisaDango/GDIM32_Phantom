using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] private List<FoodInstance> foods = new List<FoodInstance>();





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

    public int GetSoyBean() { return soybean; }
    public int GetInsect() { return insect; }
    public int GetCarrot() {return carrot; }
    public int GetCorn() { return corn; }
    public int GetHay() { return hay; }
    public int GetMoney() { return money; }

    public void SetMoney(int money) { this.money = money; }

    public void AddMoney(int amount) { this.money += amount; }
    public void AddSoyBean(int amount) { this.soybean = amount; }
    public void AddInsect(int amount) { this.insect = amount;}
    public void AddHay(int amount) { this.hay= amount; }
    public void AddCorn(int amount) { this.corn = amount; }
    public void AddCarrot(int amount) { this.carrot = amount; }



    public List<AnimalInstance> GetAnimals() { return animals; }

    public void BuyItem(SOAnimalDefinition animalDef)
    {
        AnimalInstance animalInst = animalDef.Spawn(this, animalSpawnPos);
        animalInst.Initialize(animalDef.GetName(), animalDef.GetSellValue(), animalDef.GetAnimalType(), this, animalDef.GetIcon(), animalDef.GetAdultGrowthValue());
        animals.Add(animalInst);

        money -= animalDef.GetCost();
    }

    public void BuyItem(SOFoodDefinition foodDef)
    {
        FoodInstance foodInst = foodDef.Spawn();

        foodInst.Initialize(foodDef.GetName(), foodDef.GetCost(), foodDef.GetAnimalType(), this, foodDef.GetIcon(), foodDef.GetGrowValue());

        foods.Add(foodInst);

        switch (foodDef.GetType())
        {
            case SOFoodDefinition.FoodType.Hay:
                hay++;
                break;
            case SOFoodDefinition.FoodType.SoyBean:
                soybean++;
                break;
            case SOFoodDefinition.FoodType.Corn:
                corn++;
                break;
            case SOFoodDefinition.FoodType.Carrot:
                carrot++;
                break;
            case SOFoodDefinition.FoodType.Insect:
                insect++;
                break;
        }

        money -= foodDef.GetCost();
    }


    public void RemoveAnimal(AnimalInstance animal)
    {
        animals.Remove(animal);
    }


}
