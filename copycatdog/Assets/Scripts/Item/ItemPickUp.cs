using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item currentItem;
    public GameObject ItemObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Character c = collision.GetComponent<Character>();
            c.audio.clip = c.eatItem;
            c.audio.Play();

            if(currentItem.itemType != Item.ItemType.Consume)
            {
                //����, ������ �������̰ų� Ż���� ���
                ItemDatabase.Use(currentItem.key, collision.GetComponent<Character>());
            }
            else
            {
                //�Ҹ��� ������(������ ��� Ű�� ������ ���Ǵ� ������)�� ���
                if (collision.GetComponent<Character>().ItemAcceptence())
                {
                    collision.GetComponent<Character>().GetItem(currentItem);
                }
            }
            Debug.Log(currentItem.itemName);
            Destroy(ItemObject);
        }
    }

}
