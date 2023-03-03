using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{

    public static int _playerCount;
    public Player playerPrefab;

    // Start is called before the first frame update

    private void OnEnable()
    {
        EventHandler.StartGameEvent += OnStartGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartGameEvent -= OnStartGameEvent;
    }

    public void OnStartGameEvent(int playerCount)
    {
        _playerCount = playerCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int playerCount)
    {

    }



}
