using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text soyBean, carrot, hay, corn, insect;


    [SerializeField] private Player currentPlayer;
 



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money.text = currentPlayer.GetMoney().ToString();
        soyBean.text = currentPlayer.GetSoyBean().ToString();
        carrot.text = currentPlayer.GetCarrot().ToString();
        hay.text = currentPlayer.GetHay().ToString();
        corn.text = currentPlayer.GetCorn().ToString();
        insect.text = currentPlayer.GetInsect().ToString();


    }
}
