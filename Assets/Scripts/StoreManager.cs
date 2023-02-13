using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class StoreManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private ItemDecorator AnimalDecoratorPrefab;
    [SerializeField] private Transform animalInfoParrent;

    [SerializeField] private Player currentPlayer; //need delete serializefield later


    void Start()
    {
        

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDecoratorClicked(Decorator selected)
    {
        //if(selected is AnimalDecorator)   //For later group sell use
        //{
        //    AnimalDecorator ad = selected as AnimalDecorator;
        //    currentPlayer.BuyAnimal(ad.AnimalDef);
        //}
    }

    public Decorator DecoratorFactory(IGroupable grouop, Transform parent)
    {
        return null;
    }

    public Player GetPlayer() { return currentPlayer; }

}
