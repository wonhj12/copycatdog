using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject instantBubble;
    public float explodeDelayTime = 0;
    private bool isActive = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            GameObject bubble = Instantiate(instantBubble, this.transform.position, this.transform.rotation);
            bubble.GetComponent<Bubble>().explodeTime = explodeDelayTime;
            Destroy(this.gameObject);
        }
    }
}
