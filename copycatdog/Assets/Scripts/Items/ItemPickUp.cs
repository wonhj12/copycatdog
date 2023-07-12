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
            if (collision.GetComponent<Character>().ItemAcceptence())
            {
                collision.GetComponent<Character>().GetItem(currentItem);
                Destroy(ItemObject);
            }
        }
    }

}
