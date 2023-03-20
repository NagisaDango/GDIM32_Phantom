using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Xinlin Li

public class MainMenu : MonoBehaviour
{
    public GameManager gm;
    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
    //Load the main game scene
    public void StartGame(int playerCount)
    {
        Debug.Log(1);
        SceneManager.LoadScene(1);

        EventHandler.CallStartGameEvent(playerCount);
    }
}
