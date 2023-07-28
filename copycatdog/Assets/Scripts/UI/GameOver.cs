using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject bazzi;
    public GameObject dao;
    public TextMeshProUGUI winText;

    MainManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();

        if (manager.loser == 1)
        {
            // player 2 wins
            if (manager.player2 == 0)
            {
                dao.SetActive(false);
            } else
            {
                bazzi.SetActive(false);
            }
            winText.text = "???? 2 ??!";
        } else
        {
            // player 1 wins
            if (manager.player1 == 0)
            {
                dao.SetActive(false);
            }
            else
            {
                bazzi.SetActive(false);
            }
            winText.text = "???? 1 ??!";
        }
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
