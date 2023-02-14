using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class FarmManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private List<AnimalInstance> animals = new List<AnimalInstance>();
    [SerializeField] private ItemDecorator AnimalStatsDecoratorPrefab;
    [SerializeField] private Transform animalInfoParrent;

    [SerializeField] private Player Owner;


    public GameObject feedPanel;
    public GameObject sellPanel;
    public ToggleGroup toggleGroup;

    public ItemDecorator currentDecorator;


    void OnEnable()
    {
        RefreshFarmList();
    }

    void OnDisable() { }

    public Decorator DecoratorFactory(IGroupable group, Transform parent) { return null; }

    public void OnDecoratorClicked(Decorator selected) { }

    public Player GetPlayer() { return Owner; }

    public void RefreshFarmList()
    {
        for (int i = animalInfoParrent.childCount - 1; i >= 0; i--)
        {
            Destroy(animalInfoParrent.GetChild(i).gameObject);
        }

        foreach (AnimalInstance animal in Owner.GetAnimals())
        {
            ItemDecorator ad = Instantiate(AnimalStatsDecoratorPrefab, animalInfoParrent);
            ad.Initialize(animal, this);
        }
    }
    public void OnSellConfirmed()
    {
        Owner.AddMoney(currentDecorator.GetAnimalInst().GetValue());
        Owner.RemoveAnimal(currentDecorator.GetAnimalInst());
        Destroy(currentDecorator.gameObject);
        sellPanel.SetActive(false);
    }

    public void OnFeedConfirmed()
    {
        if (!toggleGroup.GetCurrentToggle()) return;

        SOFoodDefinition def = toggleGroup.GetCurrentToggle().GetComponent<FoodChoice>().GetFoodDef();

        currentDecorator.GetAnimalInst().AddGrowth(def.GetGrowValue());
        currentDecorator.RefreshGrowSlider();
        Owner.AddFoodCount(def.GetFoodType(), -1);


        feedPanel.SetActive(false);

        toggleGroup.CLeanToggleGroup();
    }

    public void SetSellPriceText(int value)
    {
        sellPanel.transform.Find("PriceText").GetComponentInChildren<TMP_Text>().text = "Sell the animal for $" + value.ToString();
    }

}
