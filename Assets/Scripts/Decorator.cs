using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Decorator : MonoBehaviour
{
    public IGroupable Group { get; protected set; }

    [SerializeField] protected TMP_Text nameDisplay;
    [SerializeField] protected TMP_Text costDisplay;

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
