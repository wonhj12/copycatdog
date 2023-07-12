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
            Bubble.GetComponent<Bubble>().player = collision.GetComponent<Character>();       //����ִ� �÷��̾��� ĳ���� ������Ʈ ��������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Bubble.GetComponent<BoxCollider2D>().enabled = true;
            Bubble.GetComponent<Bubble>().isPlayerIn = false;

            Debug.Log("������ ���");

            this.gameObject.SetActive(false);
        }
    }



}
