using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject FarmPanel;
    [SerializeField] private GameObject displayPanel;

    [SerializeField] private Player player;

    [SerializeField] private int moneypreset = 500;
     



    void Start()
    {
        shopPanel.SetActive(false);
        FarmPanel.SetActive(false);
        displayPanel.SetActive(true);

        player.SetMoney(moneypreset);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!shopPanel.activeSelf)
                shopPanel.SetActive(true);
            else
                shopPanel.SetActive(false);
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            if (!FarmPanel.activeSelf)
                FarmPanel.SetActive(true);
            else
                FarmPanel.SetActive(false);
        }



    }
}
