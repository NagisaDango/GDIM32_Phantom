using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//by Xinlin Li

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform canvasMulti;

    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject shopPanel_Left;
    [SerializeField] private GameObject shopPanel_Right;
    [SerializeField] private GameObject farmPanel;
    [SerializeField] private GameObject farmPanel_Left;
    [SerializeField] private GameObject farmPanel_Right;
    [SerializeField] private GameObject displayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;

    public Player playerAIPrefab;
    public Player playerPrefab;
    public CameraController cc;
    public MultiplayerEventSystem eventsys;
    public List<Player> players = new List<Player>();
    public List<Transform> playerSpawnPos = new List<Transform>();
    public List<MultiplayerEventSystem> eventSystems = new List<MultiplayerEventSystem>();

    [SerializeField] private int moneypreset = 500;
    [SerializeField] private int targetMoney = 1000;


    private Dictionary<int, GameObject> shops;
    private Dictionary<int, GameObject> farms;
    private Dictionary<int, string> schemes;


    void Start()
    {
        Time.timeScale = 1;
        shopPanel.SetActive(false);
        shopPanel_Left.SetActive(false);
        shopPanel_Right.SetActive(false);
        farmPanel.SetActive(false);
        farmPanel_Left.SetActive(false);
        farmPanel_Right.SetActive(false);
        displayPanel.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);

        shops = new Dictionary<int, GameObject>{
            { 1, shopPanel_Left},
            { 2, shopPanel_Right}
        };

        farms = new Dictionary<int, GameObject>()
        {
            { 1, farmPanel_Left},
            { 2, farmPanel_Right}
        };

        schemes = new Dictionary<int, string>()
        {
            { 1, "Keyboard&Mouse"},
            { 2, "Gamepad"}
        };

        Player go = null;
        InputActionAsset module = (InputActionAsset)AssetDatabase.LoadAssetAtPath("Assets/Scripts/PlayerAction.inputactions", typeof(InputActionAsset));
        InputActionAsset module2 = (InputActionAsset)AssetDatabase.LoadAssetAtPath("Assets/Scripts/PlayerAction_Multi.inputactions", typeof(InputActionAsset));

        for (int i = 0; i < GameManager._playerCount; i++)
        {
            go = Instantiate(playerPrefab, playerSpawnPos[i].position, Quaternion.identity);
            go.playerIndex = i + 1;
            players.Add(go);
            cc.m_Targets.Add(go.transform);
            players[i].SetMoney(moneypreset);
            go.gameObject.SetActive(true);

            displayPanel.GetComponent<DisplayManager>().SetPlayer(go);


            shops[go.playerIndex].GetComponent<StoreManager>().SetPlayer(go);
            farms[go.playerIndex].GetComponent<FarmManager>().SetPlayer(go);

            go.playerInput.defaultControlScheme = schemes[go.playerIndex];
            go.playerInput.neverAutoSwitchControlSchemes = true;

            if (i == 0)
            {
                go.playerInput.actions = module;
                go.playerInput.uiInputModule = eventSystems[i].GetComponent<InputSystemUIInputModule>();
            }
            else if (i == 1)
            {
                go.playerInput.actions = module2;
                go.playerInput.uiInputModule = eventSystems[i].GetComponent<InputSystemUIInputModule>();
            }
        }


        if (GameManager._playerCount == 1) 
        {
            shopPanel.GetComponent<StoreManager>().SetPlayer(go);
            farmPanel.GetComponent<FarmManager>().SetPlayer(go);
            go.playerInput.neverAutoSwitchControlSchemes = false;
            Instantiate(playerAIPrefab, playerSpawnPos[1].position, Quaternion.identity);
            canvas.gameObject.SetActive(true);
        }
        else if(GameManager._playerCount == 2)
        {
            canvas.gameObject.SetActive(true);

        }



    }

    public void CheckPanelForSingle()
    {
        if (players[0].InShop) //open the shop panel when player is near the shop
        {
            Debug.Log("enter in shop");
            if (players[0].playerController.interactInput)
            {
                Debug.Log("enter interact");
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

            Debug.Log("enter in farm");

            if (players[0].playerController.interactInput)
            {
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
                //Debug.Log("enter in shop");
                if (p.playerController.interactInput)
                {

                    if (!shop.activeSelf)
                        shop.SetActive(true);
                    else
                        shop.SetActive(false);

                    p.playerController.interactInput = false;
                }
            }
            else
            {
                shop.SetActive(false);
            }


            if (p.InFarm) //open the farm panel when player is near the farm
            {
                //Debug.Log("enter in farm");
                if (p.playerController.interactInput)
                {

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
        if (players.Count <= 0) return;

        if (players[0].GetMoney() >= targetMoney)
        {
            PauseGame();
            winPanel.SetActive(true);
        }

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    players[0].AddMoney(1000);
        //}

        if (players.Count() == 1)
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
            if (shopPanel_Left.activeSelf)
            {
                shopPanel_Left.SetActive(false);
                return;
            }
            if (shopPanel_Right.activeSelf)
            {
                shopPanel_Right.SetActive(false);
                return;
            }

            if (farmPanel.activeSelf)
            {
                farmPanel.SetActive(false);
                return;
            }
            if (farmPanel_Left.activeSelf)
            {
                farmPanel_Left.SetActive(false);
                return;
            }
            if (farmPanel_Right.activeSelf)
            {
                farmPanel_Right.SetActive(false);
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
            if (players[0].playerController.placeAnimalInput && players[0].InFarm && !players[0].InPanel)
            {
                players[0].StoreToFarm(players[0].followingAnimals);
            }
            //if player 2 in farm range and press right control, send the animal back to farm
            if (players[1].playerController.placeAnimalInput && players[1].InFarm && !players[1].InPanel)
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
        UnPauseGame();
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
            if (farmPanel_Left.activeSelf)
            {
                farmPanel_Left.GetComponent<FarmManager>().ResetSelected();
                farmPanel_Left.GetComponent<FarmManager>().sellPanel.SetActive(false);
                farmPanel_Left.GetComponent<FarmManager>().feedPanel.SetActive(false);


            }

            farmPanel_Right.GetComponent<FarmManager>().RefreshFarmList();
            if (farmPanel_Right.activeSelf)
            {
                farmPanel_Right.GetComponent<FarmManager>().ResetSelected();
                farmPanel_Right.GetComponent<FarmManager>().sellPanel.SetActive(false);
                farmPanel_Right.GetComponent<FarmManager>().feedPanel.SetActive(false);
            }
        }
        
    }

}
