using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
//Shenjie Zhang

public class StoreManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private ItemDecorator AnimalDecoratorPrefab;
    public Transform animalInfoParrent;

    [SerializeField] private Player currentPlayer; 

    public MultiplayerEventSystem eventSystem;

    void Awake()
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
        currentPlayer.InPanel = true;
        if (animalInfoParrent.GetChild(0).gameObject.activeSelf)
        {
            Button btn = animalInfoParrent.GetChild(0).Find("BuyButton").GetComponent<Button>();
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(btn.gameObject);
            btn.OnSelect(null);
        }

    }
    private void OnDisable()
    {
        currentPlayer.InPanel = false;
        eventSystem.SetSelectedGameObject(null);

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
