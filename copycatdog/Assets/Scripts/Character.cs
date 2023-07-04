using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]

public class Character : MonoBehaviour
{
    //상태 관련 변수
    [Header("상태 관련 변수")]
    public bool isRiding;
    public bool isDamaged;
    public bool isAlive = true;


    //캐릭터 속성
    [Header("캐릭터 속성")]
    public string characterName;

    [SerializeField] protected int currentBubble;   //현재 맵에 설치 가능한 물풍선 개수
    public int carryBubble;     //현재 들고다닐 수 있는 최대 물풍선 개수
    public int maxBubble;       //최대로 들고다닐 수 있는 물풍선의 개수
                                //currentBubble은 항상 carryBubble 이하임
                                //carryBubble은 항상 maxBubble 이하임

    public int currentAtkLength;    //현재 물풍선을 설치하면 나아가는 길이
    public int maxAtkLength;        //물풍선이 터질 때 나아갈 수 있는 최장거리

    public float currentSpeed;      //현재 이동속도
    public float maxSpeed;          //최대 이동속도

    protected float bubbleExplodeTime;


    //필요한 컴포넌트
    [Header("필요한 컴포넌트")]
    //물풍선 프리팹
    public GameObject Bubble;



    protected void Awake()
    {
        bubbleExplodeTime = Bubble.GetComponent<Bubble>().explodeTime;
    }


    protected void Update()
    {
        //공격 버튼을 누르면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    protected void Attack()
    {
        //물풍선을 더 설치할 수 있다면
        if(currentBubble > 0)
        {
            //물풍선 설치 위치 계산
            Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

            //임시로 작성한 코드
            Instantiate(Bubble, AttackLocation, Quaternion.identity);

            //물풍선 프리팹을 Instantiate하고 현재 캐릭터가 만들 수 있는 물풍선의 길이를 전달해주는 코드
            
            GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
            bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
            

            //물풍선 사용
            currentBubble -= 1;

            //물풍선 재충전
            StartCoroutine(RestoreBubble());
        }
    }


    protected IEnumerator RestoreBubble()
    {
        //물풍선의 속성 값 중 터지기까지의 시간을 가져와, 해당 시간동안 대기 후 아래의 코드 실행
        yield return new WaitForSeconds(bubbleExplodeTime);

        //만약 현재 설치 가능한 물풍선의 수가 현재 들고다닐 수 있는 최대 물풍선 개수보다 작으면 재충전
        if(currentBubble < carryBubble)
        {
            currentBubble += 1;
        }
    }


    public void IncreaseCarryBubbleCount()
    {
        if(carryBubble < maxBubble)
        {
            currentBubble += 1;
            carryBubble += 1;
        }
    }


    protected void GetItem(/*Item item*/)
    {
        //아이템 기능 개발 후 적용
        //아이템 형식의 배열에다가 현재 아이템을 저장

        //for 문으로 인벤토리 내에 같은 종류의 아이템이 있는지 확인
        //있다면 개수를 증가시키기
        Debug.Log("Item Find");
    }


    protected void UseItem()
    {
        //아이템 기능 개발 후 적용
        //아이템 효과들을 모아놓은 스크립트 호출
        Debug.Log("Item Used");
    }


    public void Dead()
    {
        Debug.Log("Died");
        isAlive = false;
    }
}
