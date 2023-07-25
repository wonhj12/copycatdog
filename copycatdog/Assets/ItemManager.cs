using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] Items;

    public GameObject ChooseItem()
    {
        int randNum = Random.Range(0, Items.Length);

        int randProbability = Random.Range(0, 100);

        //Debug.Log(Items[randNum].GetComponentInChildren<ItemPickUp>().currentItem.spawnProbability);

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
