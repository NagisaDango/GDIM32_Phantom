using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackDecorator : Decorator
{
    public enum Type
    {
        Animal,
        Food
    }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemtype;
    [SerializeField] private Text description;
    [SerializeField] private Type type;

    private AnimalInstance ItemInst;
    public SOAnimalDefinition AnimalDef { get; set; }
    public SOFoodDefinition FoodDef { get; set; }

    public Button btn;

    public void Initialize(IGroupable a, IDecoratorManager manager)
    {
        if (a is AnimalInstance)
        {
            Group = a;
            ItemInst = a as AnimalInstance;
            icon.sprite = ItemInst.Icon;

            base.Initialize(manager);
        }
        else
        {
            Debug.LogError("Trying to decorate a non Animal as a Aniaml");
        }
    }

    public override void Refresh()
    {
    }

}
