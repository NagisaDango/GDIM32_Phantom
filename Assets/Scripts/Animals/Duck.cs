using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Xinlin Li
public class Duck : AnimalInstance
{
    // Update is called once per frame
    void Update()
    {
        if (playerReference != null)
        {
            if (playerReference.GetInsect() > 0 || playerReference.GetSoybean() > 0)
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
