using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public int key;             //�������� Ű��
    public string itemName;     //�������� �̸�
    [TextArea]
    public string itemDesc;     //�������� ����
    public ItemType itemType;   //�������� ����
    //public Sprite itemImage;    //�������� �̹���(ĵ���� �ʿ� X)
    public GameObject itemPrefab;   //�������� ������
    [Range(0, 1)]
    public float spawnProbability;    //�������� Ȯ��

    public enum ItemType
    {
        Buff,
        Nerf,
        Consume,
        Vehicle,
        ETC
    }

}
