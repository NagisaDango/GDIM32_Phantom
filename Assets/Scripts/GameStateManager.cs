using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//by Xinlin Li

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject farmPanel;
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Player player;

    [SerializeField] private int moneypreset = 500;

    void Start()
    {
        shopPanel.SetActive(false);
        farmPanel.SetActive(false);
        displayPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);

        player.SetMoney(moneypreset);

    }

    void Update()
    {
        if (player.InShop) //open the shop panel when player is near the shop
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryPanel.activeSelf)
                    inventoryPanel.SetActive(false);

                if (!shopPanel.activeSelf)
                    shopPanel.SetActive(true);
                else
                    shopPanel.SetActive(false);
            }
        }
        else
        {
            shopPanel.SetActive(false);
        }

        if (player.InFarm) //open the farm panel when player is near the farm
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryPanel.activeSelf)
                    inventoryPanel.SetActive(false);

                if (!farmPanel.activeSelf)
                    farmPanel.SetActive(true);
                else
                    farmPanel.SetActive(false);
            }
        }
        else
        {
            farmPanel.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.C))//open and close inventory 
        {
            if (farmPanel.activeSelf || shopPanel.activeSelf)
            {
                farmPanel.SetActive(false);
                shopPanel.SetActive(false);
            }

            if (!inventoryPanel.activeSelf)
                inventoryPanel.SetActive(true);
            else
                inventoryPanel.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Escape))//use escape to close the active panel if any of them are open
        {
            if (shopPanel.activeSelf)
            {
                shopPanel.SetActive(false);
                return;
            }
            else if (farmPanel.activeSelf)
            {
                farmPanel.SetActive(false);
                return;
            }
            else if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
                return;
            }



            if (!pausePanel.activeSelf)//open pause meny
            {
                pausePanel.SetActive(true);
                PauseGame();
            }
            else//close pause menu
            {
                //pausePanel.SetActive(false);
                UnPauseGame();
            }

        }


    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UnPauseGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
