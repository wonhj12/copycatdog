using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [Header("Characters")]
    GameObject player1;
    GameObject player2;

    [Header("X?? ?? ??????")]
    public float xUpperBound;
    public float xLowerBound;

    [Header("Y?? ?? ??????")]
    public float yUpperBound;
    public float yLowerBound;

    [Header("???? ??????")]
    public Transform[] spawnPoints;

    [Header("???????? ????")]
    public int playerCharacter_1;
    public int playerCharacter_2;

    [Header("??????")]
    public GameObject Bazzi;
    public GameObject Dao;

    [Header("UI")]
    public GameObject[] p1Bombs;
    public GameObject[] p2Bombs;
    public GameObject[] p1Item;
    public GameObject[] p2Item;
    public Sprite[] character;
    public Image p1Image;
    public Image p2Image;

    private void Awake()
    {
        MainManager manager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();
        playerCharacter_1 = manager.player1;
        playerCharacter_2 = manager.player2;

        p1Image.sprite = character[manager.player1];
        p2Image.sprite = character[manager.player2];

        //???? ???? ???? ?????? ????
        int randSpawn_1 = Random.Range(0, spawnPoints.Length);
        int randSpawn_2 = Random.Range(0, spawnPoints.Length);

        while (randSpawn_2 == randSpawn_1)
        {
            randSpawn_2 = Random.Range(0, spawnPoints.Length);
        }

        Debug.Log(randSpawn_1 + " " + randSpawn_2);

        switch (playerCharacter_1)
        {
            case 0:
                player1 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
            case 1:
                player1 = Instantiate(Dao, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
            default:
                //???????? ????
                player1 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
        }
        player1.GetComponent<Character>().SetP1();

        switch (playerCharacter_2)
        {
            case 0:
                player2 = Instantiate(Bazzi, spawnPoints[randSpawn_2].position, Quaternion.identity);
                break;
            case 1:
                player2 = Instantiate(Dao, spawnPoints[randSpawn_2].position, Quaternion.identity);
                break;
            default:
                //???????? ????
                player2 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
        }
        player2.GetComponent<Character>().SetP2();
    }

    public void ShowBubble(int playerNum, int count)
    {
        Debug.Log("show bubble");
        if (playerNum == 1)
        {
            for (int i = 0; i < count - 1; i++)
            {
                p1Bombs[i].SetActive(true);
            }
        } else
        {
            for (int i = 0; i < count - 1; i++)
            {
                p2Bombs[i].SetActive(true);
            }
        }
    }

    public void ShowItem(int playerNum, int index, Sprite sprite)
    {
        Debug.Log("Show Item");
        if (playerNum == 1)
        {
            p1Item[index].GetComponent<Image>().sprite = sprite;
            p1Item[index].SetActive(true);
        } else
        {
            p2Item[index].GetComponent<Image>().sprite = sprite;
            p2Item[index].SetActive(true);
        }
    }

    public void UseItem(int playerNum, int index)
    {
        if (playerNum == 1)
        {
            p1Item[0].GetComponent<Image>().sprite = p1Item[1].GetComponent<Image>().sprite;
            p1Item[1].SetActive(false);
            if (index == 999)
            {
                p1Item[0].SetActive(false);
            }
        } else
        {
            p2Item[0].GetComponent<Image>().sprite = p2Item[1].GetComponent<Image>().sprite;
            p2Item[1].SetActive(false);
            if (index == 999)
            {
                p2Item[0].SetActive(false);
            }
        }
    }
}