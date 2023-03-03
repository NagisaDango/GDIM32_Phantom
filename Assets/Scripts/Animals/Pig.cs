using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Xinlin Li
public class Pig : AnimalInstance
{
    // Update is called once per frame
    void Update()
    {
        if (playerReference != null)
        {
            if (playerReference.GetSoybean() > 0 || playerReference.GetCorn() > 0 || playerReference.GetCarrot() > 0)
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
