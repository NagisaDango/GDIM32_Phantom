using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
//Shenjie Zhang

public class FarmManager : MonoBehaviour, IDecoratorManager
{
    [SerializeField] private ItemDecorator AnimalStatsDecoratorPrefab;
    [SerializeField] private Transform animalInfoParrent;

    [SerializeField] private Player currentPlayer; //todo: delete serializefield later

    public List<ItemDecorator> animalStats = new List<ItemDecorator>();

    public GameObject feedPanel;
    public GameObject sellPanel;
    public MyToggleGroup toggleGroup;

    public ItemDecorator currentDecorator; // an reference for current selected decorator
    
    public MultiplayerEventSystem eventSystem;
    public GameStateManager gsm;
    public GameObject currentSelected;

    void OnEnable()
    {
        RefreshFarmList();
        currentPlayer.InPanel = true;


        EventHandler.FeedClickedEvent += OnFeedClickedEvent;
        EventHandler.SellClickedEvent += OnSellClickedEvent;

        StartCoroutine(SetFirstSelected()); // using corountine since the button just generate this frame;
    }

    IEnumerator SetFirstSelected()
    {
        //yield return null;
        yield return null;
        
        if (animalInfoParrent.childCount > 0 && animalInfoParrent.GetChild(0).gameObject.activeSelf)
        {
            Button btn = animalInfoParrent.GetChild(0).Find("Feed").GetComponent<Button>();
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(btn.gameObject,null);

        }
    }

    void OnDisable() 
    { 
        sellPanel.SetActive(false);
        feedPanel.SetActive(false);
        currentPlayer.InPanel = false;
        eventSystem.SetSelectedGameObject(null);
        EventHandler.FeedClickedEvent -= OnFeedClickedEvent;
        EventHandler.SellClickedEvent -= OnSellClickedEvent;
    }


    //reload the panel each time 
    public void RefreshFarmList() 
    {
        for (int i = animalInfoParrent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(animalInfoParrent.GetChild(i).gameObject);
        }

        foreach (AnimalInstance animal in currentPlayer.GetAnimals())
        {
            ItemDecorator ad = Instantiate(AnimalStatsDecoratorPrefab, animalInfoParrent);
            ad.Initialize(animal, this);
            ad.RefreshGrowSlider();

        }
    }

    public void OnFeedClickedEvent(FarmManager manager)
    {
        if (manager != this) return;

        currentSelected = eventSystem.currentSelectedGameObject;

        Button btn = feedPanel.transform.Find("ConfirmBtn").GetComponent<Button>();
        eventSystem.SetSelectedGameObject(btn.gameObject);

    }
    public void OnSellClickedEvent(FarmManager manager)
    {
        if (manager != this) return;

        currentSelected = eventSystem.currentSelectedGameObject;

        Button btn = sellPanel.transform.Find("ConfirmBtn").GetComponent<Button>();
        eventSystem.SetSelectedGameObject(btn.gameObject);
    }

    public void ResetSelected()
    {
        if (currentSelected)
        {
            print("last selected exist" + currentSelected);
            StartCoroutine(ResetNewSelected(currentSelected.GetComponent<Button>()));
        }
        else
        {
            if (animalInfoParrent.childCount == 0)
                return;

            Button btn = animalInfoParrent.GetChild(0).Find("Feed").GetComponent<Button>();
            StartCoroutine(ResetNewSelected(btn));
        }
    }

    IEnumerator ResetNewSelected(Button btn)
    {
        yield return null;
        eventSystem.SetSelectedGameObject(btn.gameObject);
        btn.OnSelect(null);
    }

    
    public void OnSellConfirmed() // for pressing sell button
    {
        currentPlayer.AddMoney(currentDecorator.GetAnimalInst().CurrentValue);
        currentPlayer.RemoveAnimal(currentDecorator.GetAnimalInst());
        DestroyImmediate(currentDecorator.gameObject);
        sellPanel.SetActive(false);

        gsm.RefreshFarmForLocal();
        ResetSelected();

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

        gsm.RefreshFarmForLocal();
        ResetSelected();
    }

    public void CleanToggleGroupChilds()
    {
        for (int i = toggleGroup.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(toggleGroup.transform.GetChild(i).gameObject);
        }
    }

    //update the selling price info each time
    public void SetSellPriceText(int value)
    {
        sellPanel.transform.Find("PriceText").GetComponentInChildren<TMP_Text>().text = "Sell the animal for $" + value.ToString();
    }

    public Decorator DecoratorFactory(IGroupable group, Transform parent) { return null; }

    public void OnDecoratorClicked(Decorator selected) { }

    public Player GetPlayer() { return currentPlayer; }

    public void SetPlayer(Player player) { currentPlayer = player; }
}
