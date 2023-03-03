using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//by Xinlin Li

public class GameStateManager : MonoBehaviour
{
    public Transform canvas;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject shopPanel_Left;
    [SerializeField] private GameObject shopPanel_Right;
    [SerializeField] private GameObject farmPanel;
    [SerializeField] private GameObject farmPanel_Left;
    [SerializeField] private GameObject farmPanel_Right;
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject pausePanel;

    public Player playerPrefab;
    public CameraController cc;

    public List<Player> players = new List<Player>();
    public List<Transform> playerSpawnPos = new List<Transform>();

    [SerializeField] private int moneypreset = 500;

    private Dictionary<int, GameObject> shops;
    private Dictionary<int, GameObject> farms;
    private Dictionary<int, KeyCode> keys = new Dictionary<int, KeyCode>()
    {
        { 1,KeyCode.E },
        { 2,KeyCode.Return}
    };


    void Start()
    {
        shopPanel.SetActive(false);
        farmPanel.SetActive(false);
        displayPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);

        shops = new Dictionary<int, GameObject>{
            { 1, shopPanel_Left},
            { 2, shopPanel_Right}
        };

        farms = new Dictionary<int, GameObject>()
        {
            { 1, farmPanel_Left},
            { 2, farmPanel_Right}
        };



       
        for (int i = 0; i < GameManager._playerCount; i++)
        {
            Player go = Instantiate(playerPrefab, playerSpawnPos[i].position, Quaternion.identity);
            go.playerIndex = i + 1;
            players.Add(go);
            cc.m_Targets.Add(go.transform);
            print("GSM: " + go.transform.position);
            players[i].SetMoney(moneypreset);

            displayPanel.GetComponent<DisplayManager>().SetPlayer(go);

            
            if(GameManager._playerCount == 2)
            {
                shops[go.playerIndex].GetComponent<StoreManager>().SetPlayer(go);
                farms[go.playerIndex].GetComponent<FarmManager>().SetPlayer(go);
            }
            else
            {
                shopPanel.GetComponent<StoreManager>().SetPlayer(go);
                farmPanel.GetComponent<FarmManager>().SetPlayer(go);
            }

        }


    }

    public void CheckPanelForSingle()
    {
        if (players[0].InShop) //open the shop panel when player is near the shop
        {
            if (players[0].playerController.interactInput)
            {
                //if (inventoryPanel.activeSelf)
                //    inventoryPanel.SetActive(false);

                if (!shopPanel.activeSelf)
                    shopPanel.SetActive(true);
                else
                    shopPanel.SetActive(false);

                players[0].playerController.interactInput = false;
            }
        }
        else
        {
            shopPanel.SetActive(false);
        }

        if (players[0].InFarm) //open the farm panel when player is near the farm
        {
            if (players[0].playerController.interactInput)
            {
               // if (inventoryPanel.activeSelf)
              //      inventoryPanel.SetActive(false);

                if (!farmPanel.activeSelf)
                    farmPanel.SetActive(true);
                else
                    farmPanel.SetActive(false);

                players[0].playerController.interactInput = false;
            }
        }
        else
        {
            farmPanel.SetActive(false);
        }
    }

    public void CheckPanelForLocal()
    {
        for (int i = 0; i < players.Count; i++) 
        {
            var p = players[i];
            GameObject shop = shops[p.playerIndex];
            GameObject farm = farms[p.playerIndex];

            if (p.InShop) //open the shop panel when player is near the shop
            {
                if (p.playerController.interactInput)
                {
                   // if (inventoryPanel.activeSelf)
                   //     inventoryPanel.SetActive(false);

                    print(shop);
                    if (!shops[p.playerIndex].activeSelf)
                        shops[p.playerIndex].SetActive(true);
                    else
                        shops[p.playerIndex].SetActive(false);

                    p.playerController.interactInput = false;
                }
            }
            else
            {
                shops[p.playerIndex].SetActive(false);
            }


            if (p.InFarm) //open the farm panel when player is near the farm
            {
                if (p.playerController.interactInput)
                {
                    //if (inventoryPanel.activeSelf)
                    //    inventoryPanel.SetActive(false);

                    if (!farm.activeSelf)
                        farm.SetActive(true);
                    else
                        farm.SetActive(false);

                    p.playerController.interactInput = false;

                }

            }
            else
            {
                farm.SetActive(false);
            }

        }
    }

    void Update()
    {
        if(players.Count() == 1)
            CheckPanelForSingle();
        else if (players.Count() == 2)
            CheckPanelForLocal();

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


        if (players.Count() == 1)
        {
            //if player 1 in farm range and press c, send the animal back to farm
            if (players[0].playerController.placeAnimalInput && players[0].InFarm)
            {
                players[0].StoreToFarm(players[0].followingAnimals);
            }
        }

        else if (players.Count() == 2)
        {
            //if player 1 in farm range and press c, send the animal back to farm
            if (players[0].playerController.placeAnimalInput && players[0].InFarm)
            {
                players[0].StoreToFarm(players[0].followingAnimals);
            }
            //if player 2 in farm range and press right control, send the animal back to farm
            if (players[1].playerController.placeAnimalInput && players[1].InFarm)
            {
                players[1].StoreToFarm(players[1].followingAnimals);
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


    public void RefreshFarmForLocal()
    {
        if(players.Count() == 2)
        {
            farmPanel_Left.GetComponent<FarmManager>().RefreshFarmList();
            farmPanel_Right.GetComponent<FarmManager>().RefreshFarmList();
        }
        
    }

}
