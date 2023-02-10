using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour, IGroupable
{
    public Player Owner { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public IGroupable ParentGroup { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AddToGroup(List<IGroupable> toAdd)
    {
        throw new System.NotImplementedException();
    }

    public List<IGroupable> GetSubGroups()
    {
        throw new System.NotImplementedException();
    }

    public int GetValue()
    {
        throw new System.NotImplementedException();
    }

    public bool IsComposite()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveAndDestroy(List<IGroupable> toRemove)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveFromGroup(List<IGroupable> toRemove)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
