using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
//Shenjie Zhang

public class FarmManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private ItemDecorator AnimalStatsDecoratorPrefab;
    [SerializeField] private Transform animalInfoParrent;

    [SerializeField] private Player currentPlayer; //todo: delete serializefield later


    public GameObject feedPanel;
    public GameObject sellPanel;
    public ToggleGroup toggleGroup;
    public ItemDecorator currentDecorator; // an reference for current selected decorator


    void OnEnable()
    {
        RefreshFarmList();
        currentPlayer.InPanel = true;
    }

    void OnDisable() 
    { 
        sellPanel.SetActive(false);
        feedPanel.SetActive(false);
        currentPlayer.InPanel = false;

    }

    public Decorator DecoratorFactory(IGroupable group, Transform parent) { return null; }

    public void OnDecoratorClicked(Decorator selected) { }

    public Player GetPlayer() { return currentPlayer; }

    //reload the panel each time 
    public void RefreshFarmList() 
    {
        for (int i = animalInfoParrent.childCount - 1; i >= 0; i--)
        {
            Destroy(animalInfoParrent.GetChild(i).gameObject);
        }

        foreach (AnimalInstance animal in currentPlayer.GetAnimals())
        {
            ItemDecorator ad = Instantiate(AnimalStatsDecoratorPrefab, animalInfoParrent);
            ad.Initialize(animal, this);
        }
    }

    
    public void OnSellConfirmed() // for pressing sell button
    {
        currentPlayer.AddMoney(currentDecorator.GetAnimalInst().CurrentValue);
        currentPlayer.RemoveAnimal(currentDecorator.GetAnimalInst());
        Destroy(currentDecorator.gameObject);

        sellPanel.SetActive(false); 
    }

    public void OnFeedConfirmed() // for pressing feed button
    {
        if (!toggleGroup.GetCurrentToggle()) return; // player need to selected a food toggle to continue

        SOFoodDefinition def = toggleGroup.GetCurrentToggle().GetComponent<FoodChoice>().GetFoodDef();

        currentDecorator.GetAnimalInst().AddGrowth(def.GetGrowValue());
        currentDecorator.RefreshGrowSlider();
        currentPlayer.AddFoodCount(def.GetFoodType(), -1);
        toggleGroup.CleanToggleGroup();

        feedPanel.SetActive(false);

    }

    //update the selling price info each time
    public void SetSellPriceText(int value)
    {
        sellPanel.transform.Find("PriceText").GetComponentInChildren<TMP_Text>().text = "Sell the animal for $" + value.ToString();
    }

}
