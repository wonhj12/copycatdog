using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelection : MonoBehaviour
{
    public Button[] char1;
    public Button[] char2;
    public Button mapBtn;
    public int selChar1;
    public int selChar2;

    MainManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();

        // Char 1 
        for (int i = 0; i < char1.Length; i++)
        {
            int index = i;
            char1[index].onClick.AddListener(() => selectChar1(index));
        }

        // Char 2
        for (int i = 0; i < char2.Length; i++)
        {
            int index = i;
            char2[index].onClick.AddListener(() => selectChar2(index));
        }
    }

    private void selectChar1(int index)
    {
        if (selChar1 > -1)
        {
            char1[selChar1].transform.GetChild(0).gameObject.SetActive(false);
        }
        selChar1 = index;
        char1[selChar1].transform.GetChild(0).gameObject.SetActive(true);
    }

    private void selectChar2(int index)
    {
        mapBtn.interactable = true;
        if (selChar2 > -1)
        {
            char2[selChar2].transform.GetChild(0).gameObject.SetActive(false);
        }
        selChar2 = index;
        char2[selChar2].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void gotoMapSel()
    {
        manager.player1 = selChar1;
        manager.player2 = selChar2;

        SceneManager.LoadScene("Map_Selection");
    }
}
