using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
    //Load the main game scene
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
