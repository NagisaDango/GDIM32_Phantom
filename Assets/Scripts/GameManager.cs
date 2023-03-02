using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool inGame;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int playerCount)
    {
        SceneManager.LoadScene(1);
        print("dafs1");

        InitializePlayer(playerCount);

        print("dafs");
    }

    public void InitializePlayer(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject go = Instantiate(playerPrefab,playerPrefab.transform.position,playerPrefab.transform.rotation);
            print(go.transform.position);
        }
    }



}
