using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
//Shenjie Zhang

public class StoreManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private ItemDecorator AnimalDecoratorPrefab;
    [SerializeField] private Transform animalInfoParrent;

    [SerializeField] private Player currentPlayer; //todo: delete serializefield later


    void Start()
    {
        //load animal and food definition into the shop panel
        SOAnimalDefinition[] animalDefs = Resources.LoadAll<SOAnimalDefinition>("AnimalDefinitions");
        SOFoodDefinition[] foodDefs = Resources.LoadAll<SOFoodDefinition>("FoodDefinitions");

        foreach (SOAnimalDefinition animal in animalDefs)
        {
            ItemDecorator ad = Instantiate(AnimalDecoratorPrefab, animalInfoParrent);
            ad.Initialize(animal, this);
        }

        foreach (SOFoodDefinition food in foodDefs)
        {
            ItemDecorator fd = Instantiate(AnimalDecoratorPrefab, animalInfoParrent);
            fd.Initialize(food, this);
        }

    }

    private void OnEnable()
    {
        //EventHandler.PlayerSpawnEvent += OnPlayerSpawnEvent;
        currentPlayer.InPanel = true;
    }
    private void OnDisable()
    {
        //EventHandler.PlayerSpawnEvent -= OnPlayerSpawnEvent;
        currentPlayer.InPanel = false;
    }

    public void OnPlayerSpawnEvent(Player player)
    {
        currentPlayer = player;
    }

    public void OnDecoratorClicked(Decorator selected)
    {
        //if(selected is AnimalDecorator)   //For later group sell use
        //{
        //    AnimalDecorator ad = selected as AnimalDecorator;
        //    currentPlayer.BuyAnimal(ad.AnimalDef);
        //}
    }

    public Decorator DecoratorFactory(IGroupable grouop, Transform parent) { return null; }

    public Player GetPlayer() { return currentPlayer; }

    public void SetPlayer(Player player) { currentPlayer = player; }

}
