using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleColliderDetect : MonoBehaviour
{
    public GameObject Bubble;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Bubble.GetComponent<Bubble>().isPlayerIn = true;
            Bubble.GetComponent<Bubble>().player = collision.GetComponent<Character>();       //닿아있는 플레이어의 캐릭터 오브젝트 가져오기
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Bubble.GetComponent<BoxCollider2D>().enabled = true;
            Bubble.GetComponent<Bubble>().isPlayerIn = false;

            Debug.Log("원에서 벗어남");

            this.gameObject.SetActive(false);
        }
    }



}
