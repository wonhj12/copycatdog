using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("X�� �� ��谪")]
    public float xUpperBound;
    public float xLowerBound;

    [Header("Y�� �� ��谪")]
    public float yUpperBound;
    public float yLowerBound;

    [Header("���� ����Ʈ")]
    public Transform[] spawnPoints;

    [Header("�÷��̾� ����")]
    public int playerCharacter_1;
    public int playerCharacter_2;

    [Header("ĳ����")]
    public GameObject Bazzi;
    public GameObject Dao;

    private void Awake()
    {
        //���� �ٸ� ���� ����Ʈ ����
        int randSpawn_1 = Random.Range(0, spawnPoints.Length);
        int randSpawn_2 = Random.Range(0, spawnPoints.Length);

        GameObject player1;
        GameObject player2;

        /*
        while (randSpawn_2 == randSpawn_1)
        {
            randSpawn_2 = Random.Range(0, spawnPoints.Length);
        }

        switch (playerCharacter_1)
        {
            case 0:
                player1 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
            case 1:
                player1 = Instantiate(Dao, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
            default:
                //�⺻���� ����
                player1 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
        }
        player1.GetComponent<Character>().playerNum = 1;

        switch (playerCharacter_2)
        {
            case 0:
                player2 = Instantiate(Bazzi, spawnPoints[randSpawn_2].position, Quaternion.identity);
                break;
            case 1:
                player2 = Instantiate(Dao, spawnPoints[randSpawn_2].position, Quaternion.identity);
                break;
            default:
                //�⺻���� ����
                player2 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
        }
        player2.GetComponent<Character>().playerNum = 2;
        */
    }
}
