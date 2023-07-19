using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]

public class OverlapDetection : MonoBehaviour
{
    private Character Player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //�� ��ǳ���� ���� �ݶ��̴��� ���͵� �÷��̾� ������Ʈ���Լ� ĳ���� ������Ʈ ��������
            Player = collision.GetComponent<Character>();
            //if (Player != null)
                //Debug.Log("Player Detected");
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //�� ��ǳ���� �÷��̾ ����ִٸ� ��ǳ���� ��ġ�Ǿ��ٰ� �ν�
            Player.SetDeploy();
            //Debug.Log("Bubble Deployed");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //�� ��ǳ���� �������� �÷��̾ �����ٸ� ��ǳ���� �� �ڸ��� ��ġ�Ǿ� ���� �ʴٰ� �ν�
            Player.SetUndeploy();
            //Debug.Log("Bubble Undeployed");
        }
    }
}
