using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGroup : IGroupable, ISellable
{
    public Player Owner { get; set; }
    public IGroupable ParentGroup { get; set; }
    public string GroupName { get; set; }
    private List<IGroupable> groups = new List<IGroupable>();


    public void CreateGroup(string name)
    {
        GroupName= name;
    }

    public void AddToGroup(List<IGroupable> toAdd)
    {
        foreach(IGroupable add in toAdd)
        {
            add.ParentGroup = this;
            groups.Add(add);
        }
    }

    public void RemoveFromGroup(List<IGroupable> toRemove)
    {
        foreach (var agroup in toRemove)
        {
            groups.Remove(agroup);
        }
    }

    public void RemoveAndDestroy(List<IGroupable> toRemove)
    {
        for (int i = groups.Count - 1; i >= 0; i--) 
        {
            IGroupable agroup = groups[i];
            agroup.RemoveAndDestroy(agroup.GetSubGroups());
            groups.RemoveAt(i);
        }
    }

    public int GetValue()
    {
        int totalValue = 0; 
        foreach(IGroupable ag in groups)
        {
            totalValue+= ag.GetValue();
        }
        return totalValue;
    }

    public int GetAnimalCount()
    {
        int count = 0;
        foreach(IGroupable g in groups)
        {
            if (g.IsComposite())
            {
                AnimalGroup ag = g as AnimalGroup;
                count += ag.GetAnimalCount();
            }
            else
            {
                count++;
            }
        }
        return count;
    }

    public int GetGroupCount()
    {
        int count = 0;
        foreach (var g in groups)
        {
            if (g.IsComposite())
            {
                AnimalGroup ag = g as AnimalGroup;
                count += 1;
                count += ag.GetGroupCount();
            }
        }
        return count;
    }

    public List<IGroupable> GetSubGroups()
    {
        return groups;
    }
    public bool IsComposite()
    {
        return true;
    }


}
