using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//by Shengjie Zhang
public abstract class Decorator : MonoBehaviour
{
    public IGroupable Group { get; protected set; }

    //commonly used text component for decorator
    [SerializeField] protected TMP_Text nameDisplay;
    [SerializeField] protected TMP_Text costDisplay;
    [SerializeField] protected TMP_Text descriptionDisplay;

    protected IDecoratorManager displayManager;

    public void Initialize(IDecoratorManager manager)
    {
        displayManager = manager;
    }

    public void OnClick()
    {
        displayManager.OnDecoratorClicked(this);
    }

    public abstract void Refresh();


}
