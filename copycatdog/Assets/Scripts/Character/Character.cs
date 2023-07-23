using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]

public class Character : MonoBehaviour
{
    //상태 관련 변수
    [Header("상태 관련 변수")]
    public bool isRiding;
    public bool isDamaged = false;
    public bool isAlive = true;
    public bool isBubbleDeployed = false;
    public bool isThrowAvailable = false;
    public bool isPushAvailable = false;
    public bool isShieldAvailable = false;
    public bool isBoarding = false;
    public Bubble bubble;


    //캐릭터 속성
    [Header("캐릭터 속성 - 이름")]
    public string characterName;


    [Header("캐릭터 속성 - 물풍선 설치 & 개수")]
    public int currentBubble;   //현재 맵에 설치 가능한 물풍선 개수
    public int carryBubble;     //현재 들고다닐 수 있는 최대 물풍선 개수
    public int maxBubble;       //최대로 들고다닐 수 있는 물풍선의 개수
                                //currentBubble은 항상 carryBubble 이하임
                                //carryBubble은 항상 maxBubble 이하임


    [Header("캐릭터 속성 - 물풍선 길이")]
    public int initialAtkLength;
    public int currentAtkLength;    //현재 물풍선을 설치하면 나아가는 길이
    public int maxAtkLength;        //물풍선이 터질 때 나아갈 수 있는 최장거리


    [Header("캐릭터 속성 - 아이템 사용 관련 변수")]
    public float randMaxDelay;           //랜덤공격 간 최대 딜레이 타임
    public float randMinDelay;           //랜덤공격 간 최소 딜레이 타임

    private float currentRandomTime;    //현재 랜덤공격이 지속된 시간 (렐렐레가 지속된 누적 시간)
    public float randomSkillTime;       //랜덤으로 공격이 되는 총 시간 (렐렐레 지속 시간)

    public float shieldTime;


    //[Header("캐릭터 속성 - 이동속도")]
    //public float currentSpeed;      //현재 이동속도
    //public float maxSpeed;          //최대 이동속도

    protected float bubbleExplodeTime;


    [Header("아이템 인벤토리")]
    [SerializeField] private int[] inventory = new int[2];
    public int[] effectIndex = new int[999];


    //필요한 컴포넌트
    [Header("필요한 컴포넌트")]
    private PlayerMovement playerMovement;
    [SerializeField] private Item currentItem;
    //물풍선 프리팹
    public GameObject Bubble;



    protected void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        bubbleExplodeTime = Bubble.GetComponent<Bubble>().explodeTime;
        initialAtkLength = currentAtkLength;
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = 999;
        }
        for (int i = 0; i < effectIndex.Length; i++)
        {
            effectIndex[i] = -1;
        }
    }


    protected void Update()
    {
        //살아있는 동안
        if (isAlive)
        {
            //공격 버튼을 누르면
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

            UseItem();
        }
    }


    protected void Attack()
    {
        //물풍선을 더 설치할 수 있다면, 그리고 그 자리에 배치된 물풍선이 없다면
        if(currentBubble > 0)
        {
            if (!isBubbleDeployed)
            {
                //물풍선 설치 위치 계산
                Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

                //물풍선 프리팹을 Instantiate하고 현재 캐릭터가 만들 수 있는 물풍선의 길이를 전달해주는 코드
                GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
                bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
                bubble = bubbleObject.GetComponent<Bubble>();

                //물풍선 사용
                currentBubble -= 1;

                //물풍선 재충전
                StartCoroutine(RestoreBubble());
            }
            else if(isThrowAvailable)
            {
                int direction = playerMovement.lookingDir;

                if (direction == 0)
                {
                    bubble.GetComponent<BubbleThrow>().Throw(0);
                }
                else if(direction == 1)
                {
                    bubble.GetComponent<BubbleThrow>().Throw(1);
                }
                else if (direction == 2)
                {
                    bubble.GetComponent<BubbleThrow>().Throw(2);
                }
                else if (direction == 3)
                {
                    bubble.GetComponent<BubbleThrow>().Throw(3);
                }
            }
        }
    }


    public IEnumerator RandomAttack()
    {
        //렐렐레 효과 중 하나임

        float randTime = Random.Range(randMinDelay, randMaxDelay);

        currentRandomTime += randTime;
        yield return new WaitForSeconds(randTime);

        if (currentBubble > 0 && !isBubbleDeployed)
        {
            //물풍선 설치 위치 계산
            Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

            //물풍선 프리팹을 Instantiate하고 현재 캐릭터가 만들 수 있는 물풍선의 길이를 전달해주는 코드
            GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
            bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
            bubble = bubbleObject.GetComponent<Bubble>();

            //물풍선 사용
            currentBubble -= 1;

            //물풍선 재충전
            StartCoroutine(RestoreBubble());
        }

        Debug.Log(currentRandomTime + " " + randomSkillTime);
        if (currentRandomTime < randomSkillTime)
        {
            StartCoroutine(RandomAttack());
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


    //물풍선이 그 자리에 배치되었다는 신호 입력
    public void SetDeploy()
    {
        isBubbleDeployed = true;
    }


    //물풍선이 그 자리에 더 이상 배치되지 않은 상태라는 신호 입력
    public void SetUndeploy()
    {
        isBubbleDeployed = false;
    }


    public bool ItemAcceptence()
    {
        bool canAccept = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == 999)
            {
                canAccept = true;
            }
        }

        return canAccept;
    }


    public void GetItem(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == 999)
            {
                inventory[i] = item.key;
                break;
            }
        }
    }


    public void RestoreItem(int key)
    {
        //아이템 사용 실패 시 호출되는 함수
        for (int i = inventory.Length - 1; i >= 0; i--)
        {
            if (inventory[i] != 999)
            {
                inventory[i + 1] = inventory[i];
                inventory[i] = 999;
            }
        }
        inventory[0] = key;
    }


    protected void UseItem()
    {
        //아이템 기능 개발 후 적용
        //아이템 효과들을 모아놓은 스크립트 호출

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (inventory[0] != 999)
            {
                //데이터베이스 오브젝트에는 각각의 키 값에 아이템들이 할당되어 있음.
                //ItemDatabase 스크립트의 Use함수를 호출하고, Use함수에서 플레이어에게 효과를 부여함.
                //플레이어가 늘어나도 하나의 데이터베이스만을 통해 효율적으로 효과를 제공할 수 있음.

                ItemDatabase.Use(inventory[0], this.GetComponent<Character>());

                Debug.Log(inventory[0] + "Item Used");

                inventory[0] = inventory[1];
                inventory[1] = 999;
            }
            else
            {
                Debug.Log("No Item");
            }
        }
    }


    public IEnumerator ShieldActivate()
    {
        isShieldAvailable = true;
        yield return new WaitForSeconds(shieldTime);
        StartCoroutine(ShieldDeActivate());
    }

    protected IEnumerator ShieldDeActivate()
    {
        isShieldAvailable = false;
        yield return null;
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bubble") && isPushAvailable)
        {
            Debug.Log("밀기");
            int direction = playerMovement.lookingDir;


            switch (direction)
            {
                case 0:
                    RaycastHit2D hit_1 = Physics2D.Raycast(this.transform.position, Vector2.up, 0.51f, LayerMask.GetMask("Bubble"));

                    if(hit_1.transform != null)
                    {
                        if (hit_1.transform.tag == "Bubble")
                        {
                            hit_1.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
                case 1:
                    RaycastHit2D hit_2 = Physics2D.Raycast(this.transform.position, Vector2.down, 0.51f, LayerMask.GetMask("Bubble"));

                    if (hit_2.transform != null)
                    {
                        if (hit_2.transform.tag == "Bubble")
                        {
                            hit_2.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
                case 2:
                    RaycastHit2D hit_3 = Physics2D.Raycast(this.transform.position, Vector2.left, 0.51f, LayerMask.GetMask("Bubble"));

                    if (hit_3.transform != null)
                    {
                        if (hit_3.transform.tag == "Bubble")
                        {
                            hit_3.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
                case 3:
                    RaycastHit2D hit_4 = Physics2D.Raycast(this.transform.position, Vector2.right, 0.51f, LayerMask.GetMask("Bubble"));

                    if (hit_4.transform != null)
                    {
                        if (hit_4.transform.tag == "Bubble")
                        {
                            hit_4.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
            }
        }
    }


    public void Damage()
    {
        if (isBoarding)
        {
            playerMovement.StartCoroutine(playerMovement.UnBoard());
        }
        else
        {
            if (!isDamaged && !isShieldAvailable)
            {
                isDamaged = true;
                Debug.Log("Damaged");


                //데미지를 받은 애니메이션 실행
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                playerMovement.playerMoveSpeed *= 0.1f;

                StartCoroutine(Die());
            }
        }
    }


    public void UnDamage()
    {
        isDamaged = false;
        Debug.Log("Revived");

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        playerMovement.playerMoveSpeed *= 10f;
    }


    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        if (isDamaged)
        {
            Debug.Log("Died");
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            playerMovement.playerMoveSpeed = 0;
            isAlive = false;
        }
    }
}
