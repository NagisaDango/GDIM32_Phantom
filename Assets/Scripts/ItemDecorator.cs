using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
//by Shengjie Zhang

public class ItemDecorator : Decorator
{
    public enum ItemType
    {
        Animal,
        Food
    }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemtype;
    [SerializeField] private ItemType type;
    [SerializeField] private Slider slider;
    [SerializeField] private MyToggle toggleChoicePrefab;

    private AnimalInstance AnimalInst;
    public SOAnimalDefinition AnimalDef { get; set; }
    public SOFoodDefinition FoodDef { get; set; }

    //public Button btn;
    private SOFoodDefinition[] foodDefs;

    void Awake()
    {
        foodDefs = Resources.LoadAll<SOFoodDefinition>("FoodDefinitions"); // load food definitions
    }

    // initialization for Instance inherited Igroupable interface
    public void Initialize(IGroupable a, IDecoratorManager manager) 
    {
        if (a is AnimalInstance) // case for Animal Instance
        {
            Group = a;
            AnimalInst = a as AnimalInstance;
            if(nameDisplay) nameDisplay.text = AnimalInst.name;
            icon.sprite = AnimalInst.Icon;

            if(slider) RefreshGrowSlider();
            base.Initialize(manager);
        }
        else
        {
            Debug.LogError("Trying to decorate a non Animal as a Aniaml");
        }
    }
    // initialization for aniamal definition
    public void Initialize(SOAnimalDefinition a, IDecoratorManager manager)
    {
        AnimalDef = a;
        nameDisplay.text = a.GetName();
        costDisplay.text = "$" + a.GetCost();
        descriptionDisplay.text = a.GetDescription();
        icon.sprite = a.GetIcon();
        type = ItemType.Animal;
        itemtype.text = "Animal";
        base.Initialize(manager);
    }

    // initialization for food definition
    public void Initialize(SOFoodDefinition f, IDecoratorManager manager)
    {
        FoodDef = f;
        nameDisplay.text = f.GetName();
        costDisplay.text = "$" + f.GetCost();
        descriptionDisplay.text = f.GetDescription();
        icon.sprite = f.GetIcon();
        type = ItemType.Food;
        itemtype.text = "Food";
        base.Initialize(manager);
    }

    public override void Refresh() { }

    public void OnBuyBtnClicked() // for pressing Buy button
    {
        if (displayManager is StoreManager)
            if (type == ItemType.Animal)
            {
                if ((displayManager as StoreManager).GetPlayer().GetMoney() < AnimalDef.GetCost()) return;

                (displayManager as StoreManager).GetPlayer().BuyItem(AnimalDef);
            }
            else
            {
                if ((displayManager as StoreManager).GetPlayer().GetMoney() < FoodDef.GetCost()) return;

                (displayManager as StoreManager).GetPlayer().BuyItem(FoodDef);
            }
    }


    public void OnFeedBtnClicked(Decorator decorator) //for pressign Feed Button
    {
        FarmManager farmManager = (displayManager as FarmManager);

        farmManager.currentDecorator = decorator as ItemDecorator;

        foreach(FoodType type in AnimalInst.PreferedFood)
        {
            foreach (SOFoodDefinition t in foodDefs)
            {
                if (type == t.GetFoodType())
                {
                    if (farmManager.GetPlayer().GetFoodCount(type) <= 0) continue; // continue if having no corresponding food

                    MyToggle go = Instantiate(toggleChoicePrefab, farmManager.toggleGroup.transform);

                    go.SetIcon(t.GetIcon());

                    //Toggle go = Instantiate(toggleChoicePrefab, (displayManager as FarmManager).toggleGroup.transform);
                    //go.transform.GetChild(1).GetComponent<Image>().sprite = t.GetIcon();
                    ////go.targetGraphic as Image = t.GetIcon();
                    //go.group = (displayManager as FarmManager).toggleGroup;

                    go.GetComponent<FoodChoice>().SetFoodDef(t);

                }
            }
        }

        Transform toggleTrans = farmManager.toggleGroup.transform;
        Button confirmBtn = farmManager.feedPanel.transform.Find("ConfirmBtn").GetComponent<Button>();
        Button cancelBtn = farmManager.feedPanel.transform.Find("CancelBtn").GetComponent<Button>();
        Navigation customNav = new Navigation();
        customNav.mode = Navigation.Mode.Explicit;

        for (int i = 0; i < toggleTrans.childCount; i++)
        {
            ResetNavigation(ref customNav);
            if (i != 0)
            {
                customNav.selectOnLeft = toggleTrans.GetChild(i - 1).GetComponent<MyToggle>();
            }

            if(i != toggleTrans.childCount - 1)
            {
                customNav.selectOnRight = toggleTrans.GetChild(i + 1).GetComponent<MyToggle>();
            }

            customNav.selectOnDown = confirmBtn;
            toggleTrans.GetChild(i).GetComponent<MyToggle>().navigation = customNav;

        }

        if(toggleTrans.childCount > 0)
        {
            ResetNavigation(ref customNav);
            customNav.selectOnUp = toggleTrans.GetChild(0).GetComponent<MyToggle>();
            customNav.selectOnRight = cancelBtn;
            confirmBtn.navigation = customNav;

            ResetNavigation(ref customNav);
            customNav.selectOnUp = toggleTrans.GetChild(toggleTrans.childCount-1).GetComponent<MyToggle>();
            customNav.selectOnLeft = confirmBtn;
            cancelBtn.navigation = customNav;
        }

        farmManager.feedPanel.SetActive(true);

        EventHandler.CallFeedClickedEvent(farmManager);
    }
    public void ResetNavigation(ref Navigation nav)
    {
        nav = new Navigation();
        nav.mode = Navigation.Mode.Explicit;

    }

    public void OnSellBtnClicked(Decorator decorator) //for pressign Sell Button
    {
        FarmManager farmManager = (displayManager as FarmManager);
        print("sell btn:    " + GameObject.Find("EventSystem").GetComponent<MultiplayerEventSystem>().currentSelectedGameObject);
        print("sell btn:    " + GameObject.Find("EventSystem_Multi").GetComponent<MultiplayerEventSystem>().currentSelectedGameObject);
        if(AnimalInst.GetGrowthRate() < 1) return;

        (displayManager as FarmManager).sellPanel.SetActive(true);
        (displayManager as FarmManager).SetSellPriceText(AnimalInst.CurrentValue);

        (displayManager as FarmManager).currentDecorator = decorator as ItemDecorator;

        EventHandler.CallSellClickedEvent(farmManager);
    }

    public AnimalInstance GetAnimalInst() { return AnimalInst; }

    public void RefreshGrowSlider()
    {
        slider.value = AnimalInst.GetGrowthRate(); // update the growing stat for animal instance
    }

}
