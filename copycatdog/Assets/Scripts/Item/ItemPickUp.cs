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
                //버프, 너프형 아이템이거나 탈것인 경우
                ItemDatabase.Use(currentItem.key, collision.GetComponent<Character>());
            }
            else
            {
                //소모형 아이템(아이템 사용 키를 눌러야 사용되는 아이템)인 경우
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
