using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupDisplayManager : MonoBehaviour, IDecoratorManager
{
    public Decorator Selected { get;  set; }

    [SerializeField] private Transform displayRoot;
    [SerializeField] private Button sellBtn;
    [SerializeField] private Button addBtn;
    [SerializeField] private Button deleteBtn;
    [SerializeField] private GameObject createPopUp;
    [SerializeField] TMPro.TMP_InputField groupNameEntry;

    [SerializeField] private GroupDecorator AGDecoratorPrefab;
    [SerializeField] private AnimalDecorator AnimalDecoratorPrefab;

    private Decorator rootDecorator;

    private IGroupable previousSelected;
    private bool addingToGroup;

    public void Initialize(IGroupable group)
    {
        if(rootDecorator != null)
        {
            Destroy(rootDecorator.gameObject);
        }
        rootDecorator = DecoratorFactory(group, displayRoot);
    }

    public void Refresh()
    {
        rootDecorator.Refresh();
    }

    public Decorator DecoratorFactory(IGroupable group, Transform parent)
    {
        if(group is Group)
        {
            GroupDecorator AGD = Instantiate(AGDecoratorPrefab, parent);
            AGD.Initialize(group, this);
            return AGD;
        }
        else if(group is AnimalInstance)
        {
            AnimalDecorator AD = Instantiate(AnimalDecoratorPrefab, parent);
            AD.Initialize(group, this);
            return AD;
        }

        Debug.LogError("GroupDisplayManager::DecoratorFactory - Couldn't find decorator for " + group.GetType());
        return null;
    }

    public void OnDecoratorClicked(Decorator selected)
    {
        ResetButtons();
        Selected = selected;

        if(addingToGroup && Selected.Group.IsComposite())
        {
            FinishAddToGroup();
            return;
        }
        addingToGroup = false;

        if(Selected.Group is ISellable)
        {
            sellBtn.interactable = true;
        }

        if(Selected is AnimalDecorator)
        {
            deleteBtn.interactable = false;
        }

        if(Selected == rootDecorator)
        {
            sellBtn.interactable = false;
            addBtn.interactable = false;
            deleteBtn.interactable = false;
        }
    }

    public void ResetButtons()
    {
        sellBtn.interactable = false;
        addBtn.interactable = true;
        deleteBtn.interactable = true; ;
    }

    public void Sell()
    {
        addingToGroup = false;
        IGroupable group = Selected.Group;
        group.Owner.EarnMoney(group.GetValue());
        IGroupable parent = group.ParentGroup;
        if(parent != null)
        {
            parent.RemoveAndDestroy(new List<IGroupable>() { group });
        }
        rootDecorator.Refresh();
    }

    public void StartCreate()
    {
        addingToGroup = false;
        createPopUp.SetActive(true);
    }

    public void FinishCreate()
    {
        createPopUp.SetActive(false);
        Group newGroup = new Group();
        newGroup.Owner = GameStateManager.GetCurrentPlayer();
        newGroup.GroupName = groupNameEntry.text;
        rootDecorator.Group.AddToGroup(new List<IGroupable>() { newGroup });
        rootDecorator.Refresh();
    }

    public void DeleteGroup()
    {
        addingToGroup= false;
        IGroupable group = Selected.Group;
        List<IGroupable> children = group.GetSubGroups();
        IGroupable parent = group.ParentGroup;
        parent.RemoveFromGroup(new List<IGroupable> { group });
        parent.AddToGroup(children);
        rootDecorator.Refresh();
    }

    public void StartAddToGroup()
    {
        previousSelected = Selected.Group;
        addingToGroup= true;
    }

    public void FinishAddToGroup()
    {
        IGroupable group = Selected.Group;
        addingToGroup = false;
        if (group.IsComposite())
        {
            IGroupable prevParent = previousSelected.ParentGroup;
            List<IGroupable> ToMove = new List<IGroupable>() { previousSelected };
            prevParent.RemoveFromGroup(ToMove);
            group.AddToGroup(ToMove);
        }
        rootDecorator.Refresh();
    }



}
