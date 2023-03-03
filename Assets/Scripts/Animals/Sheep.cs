using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Xinlin Li
public class Sheep : AnimalInstance
{
    // Update is called once per frame
    void Update()
    {
        if (playerReference != null)
        {
            if (playerReference.GetHay() > 0)
            {
                canFollow = true;
            }
            else
            {
                canFollow = false;
            }
        }
    }
}
