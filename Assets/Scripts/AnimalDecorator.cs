using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimalDecorator : Decorator
{
    [SerializeField] private Image icon;
    private AnimalInstance animalInst;
    public SOAnimalDefinition AnimalDef { get; set; }

    public Button btn;

    public void Initialize(IGroupable a, IDecoratorManager manager)
    {
        if(a is AnimalInstance)
        {
            Group = a;
            animalInst = a as AnimalInstance;
            nameDisplay.text = animalInst.name;
            costDisplay.text = "$" + animalInst.GetValue();
            icon.sprite = animalInst.Icon;
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
        base.Initialize(manager);

        //btn.onClick.AddListener();

    }

    public override void Refresh()
    {

    }




}
