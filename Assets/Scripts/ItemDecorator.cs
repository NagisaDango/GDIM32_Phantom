using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private AnimalInstance ItemInst;
    public SOAnimalDefinition AnimalDef { get; set; }
    public SOFoodDefinition FoodDef { get; set; }

    public Button btn;

    //public void Initialize(IGroupable a, IDecoratorManager manager)
    //{
    //    if(a is AnimalInstance)
    //    {
    //        Group = a;
    //        animalInst = a as AnimalInstance;
    //        nameDisplay.text = animalInst.name;
    //        costDisplay.text = "$" + animalInst.GetValue();
    //        icon.sprite = animalInst.Icon;
    //        base.Initialize(manager);
    //    }
    //    else
    //    {
    //        Debug.LogError("Trying to decorate a non Animal as a Aniaml");
    //    }
    //}

    public void Initialize(IGroupable a, IDecoratorManager manager)
    {
        if (a is AnimalInstance)
        {
            Group = a;
            ItemInst = a as AnimalInstance;
            nameDisplay.text = ItemInst.name;
            icon.sprite = ItemInst.Icon;

            slider.value = ItemInst.GetGrowthRate();
            base.Initialize(manager);
        }
        else
        {
            Debug.LogError("Trying to decorate a non Animal as a Aniaml");
        }
    }

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



    public override void Refresh()
    {

    }

    public void OnBuyBtnClicked()
    {
        if (displayManager is StoreManager)
            if (type == ItemType.Animal)
            {
                if ((displayManager as StoreManager).GetPlayer().GetMoney() < AnimalDef.GetCost()) { return; }
                (displayManager as StoreManager).GetPlayer().BuyItem(AnimalDef);
            }
            else
            {
                if ((displayManager as StoreManager).GetPlayer().GetMoney() < FoodDef.GetCost()) { return; }
                (displayManager as StoreManager).GetPlayer().BuyItem(FoodDef);
            }
    }

    //public void OnBuyFoodBtnClicked()
    //{
    //    if (displayManager is StoreManager)
    //        if ((displayManager as StoreManager).GetPlayer().GetMoney() < FoodDef.GetCost()) { return; }
    //        (displayManager as StoreManager).GetPlayer().BuyItem(FoodDef);
    //}

    public void OnFeedBtnClicked()
    {

    }

    public void OnSellBtnClicked()
    {
        ItemInst.Owner.AddMoney(ItemInst.GetValue());
        ItemInst.Owner.RemoveAnimal(ItemInst);
        Destroy(gameObject);
    }

}
