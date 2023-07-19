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
            //이 물풍선의 내부 콜라이더에 엔터된 플레이어 오브젝트에게서 캐릭터 컴포넌트 가져오기
            Player = collision.GetComponent<Character>();
            //if (Player != null)
                //Debug.Log("Player Detected");
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //이 물풍선과 플레이어가 닿아있다면 물풍선이 배치되었다고 인식
            Player.SetDeploy();
            //Debug.Log("Bubble Deployed");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //이 물풍선의 범위에서 플레이어가 나간다면 물풍선이 그 자리에 배치되어 있지 않다고 인식
            Player.SetUndeploy();
            //Debug.Log("Bubble Undeployed");
        }
    }
}
