using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Shengjie Zhang
public interface IDecoratorManager 
{
    Decorator DecoratorFactory(IGroupable group, Transform parent);
    void OnDecoratorClicked(Decorator selected);


}
