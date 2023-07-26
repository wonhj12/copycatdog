using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public Button[] maps; // Maps
    public Button startBtn; // Game start button
    public int selMap = -1; // Selected map

    private void Start()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            int index = i;
            maps[index].onClick.AddListener(() => selectMap(index));
        }
    }

    void selectMap(int mapIndex)
    {
        startBtn.interactable = true;
        if (selMap > -1)
        {
            maps[selMap].transform.GetChild(0).gameObject.SetActive(false);
        }
        selMap = mapIndex;
        maps[selMap].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void startGame()
    {
        switch (selMap)
        {
            case 0:
                SceneManager.LoadScene("Village_5");
                break;
            case 1:
                SceneManager.LoadScene("Village_6");
                break;
            case 2:
                SceneManager.LoadScene("Village_10");
                break;
            case 3:
                SceneManager.LoadScene("Village_11");
                break;
            case 4:
                SceneManager.LoadScene("Village_12");
                break;
            default:
                Debug.Log("Select Map");
                break;
        }
    }
}
