using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] Items;

    public GameObject ChooseItem()
    {
        int randCategory = Random.Range(0, 4);

        int randNum = 0;

        int randProbability = Random.Range(0, 100);

        switch (randCategory)
        {
            case 0:
                randNum = Random.Range(0, 6);
                break;
            case 1:
                randNum = Random.Range(6, 8);
                break;
            case 2:
                randNum = Random.Range(8, 13);
                break;
            case 3:
                randNum = Random.Range(13, 16);
                break;
        }

        if(randProbability >= Items[randNum].GetComponentInChildren<ItemPickUp>().currentItem.spawnProbability * 100)
        {
            return Items[randNum];
        }
        else
        {
            return null;
        }
    }
}
