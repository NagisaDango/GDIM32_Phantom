using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{

    [SerializeField]
    public TMP_Text money;
    [SerializeField]
    private GroupDisplayManager playerDisplay;

    private Player currentPlayer;

    public void Open()
    {
        playerDisplay.Initialize(currentPlayer.BaseGroup);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        money.text = "$" + currentPlayer.money.ToString();
    }
}
