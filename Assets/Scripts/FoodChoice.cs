using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Xinlin Li

public class FoodChoice : MonoBehaviour
{
    private SOFoodDefinition foodDefs;

    public SOFoodDefinition GetFoodDef()
    {
        return foodDefs;
    }

    public void SetFoodDef(SOFoodDefinition foodDef)
    {
        this.foodDefs = foodDef;
    }

}
