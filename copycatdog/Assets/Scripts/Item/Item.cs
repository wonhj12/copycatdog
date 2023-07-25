using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public int key;             //아이템의 키값
    public string itemName;     //아이템의 이름
    [TextArea]
    public string itemDesc;     //아이템의 설명
    public ItemType itemType;   //아이템의 유형
    //public Sprite itemImage;    //아이템의 이미지(캔버스 필요 X)
    public GameObject itemPrefab;   //아이템의 프리팹
    [Range(0, 1)]
    public float spawnProbability;    //아이템의 확률

    public enum ItemType
    {
        Buff,
        Nerf,
        Consume,
        Vehicle,
        ETC
    }

}
