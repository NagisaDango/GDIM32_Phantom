using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Yiran Luo

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text soyBean, carrot, hay, corn, insect;

    [SerializeField] private Player currentPlayer;
 
    void Update()
    {
        if (!currentPlayer) return;

        //update info per frame.
        money.text = currentPlayer.GetMoney().ToString();
        soyBean.text = currentPlayer.GetFoodCount(FoodType.SoyBean).ToString();
        carrot.text = currentPlayer.GetFoodCount(FoodType.Carrot).ToString();
        hay.text = currentPlayer.GetFoodCount(FoodType.Hay).ToString();
        corn.text = currentPlayer.GetFoodCount(FoodType.Corn).ToString();
        insect.text = currentPlayer.GetFoodCount(FoodType.Insect).ToString();
    }

    public void SetPlayer(Player player) { currentPlayer = player; }
}
