using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("X축 맵 경계값")]
    public float xUpperBound;
    public float xLowerBound;

    [Header("Y축 맵 경계값")]
    public float yUpperBound;
    public float yLowerBound;

    [Header("스폰 포인트")]
    public Transform[] spawnPoints;

    [Header("플레이어 정보")]
    public int playerCharacter_1;
    public int playerCharacter_2;

    [Header("캐릭터")]
    public GameObject Bazzi;
    public GameObject Dao;

    private void Awake()
    {
        //서로 다른 스폰 포인트 생성
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
                //기본값은 배찌
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
                //기본값은 배찌
                player2 = Instantiate(Bazzi, spawnPoints[randSpawn_1].position, Quaternion.identity);
                break;
        }
        player2.GetComponent<Character>().playerNum = 2;
        */
    }
}
