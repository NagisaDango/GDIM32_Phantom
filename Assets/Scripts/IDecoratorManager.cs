using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecoratorManager 
{
    Decorator DecoratorFactory(IGroupable group, Transform parent);
    void OnDecoratorClicked(Decorator selected);
}
