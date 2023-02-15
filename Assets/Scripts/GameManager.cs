using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.I))//switch btwn farm and shop pannel
        {
            if (!shopPanel.activeSelf)
            {
                shopPanel.SetActive(true);
                farmPanel.SetActive(false);
            }
            else
            {
                farmPanel.SetActive(true);
                shopPanel.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.B))//switch btwn farm and shop pannel
        {
            if (!farmPanel.activeSelf)
            {
                farmPanel.SetActive(true);
                shopPanel.SetActive(false);
            }
            else
            {
                farmPanel.SetActive(false);
                shopPanel.SetActive(true);
            }

        }
        if (Input.GetKeyUp(KeyCode.C))//open and close inventory 
        {
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
            }
            else if(farmPanel.activeSelf)
            {
                farmPanel.SetActive(false);
            }
            else if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
            }
            else if (!pausePanel.activeSelf)//open pause meny
            {
                pausePanel.SetActive(true);
                PauseGame();
            }
            else//close pause menu
            {
                pausePanel.SetActive(false);
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
