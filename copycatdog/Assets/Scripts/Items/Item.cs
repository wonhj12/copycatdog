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

    public enum ItemType
    {
        Bubble,
        Speed,
        Buff,
        Debuff,
        Trap,
        Vehicle,
        Vehicle_Buff,
        ETC
    }

}
