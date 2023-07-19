using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //필요한 컴포넌트
    public Vector2 movementInput;
    public int dir;
    public bool isReversed = false;
    public float reverseDelayTime;

    //인스펙터 창에 보이는 것들
    [Header("플레이어 속도")]
    public float maxPlayerMoveSpeed;
    public float playerMoveSpeed;

    [Header("상하좌우키가 눌린 시간")]
    [SerializeField] private float[] inputTime = new float[4];

    private float minInputTime = 5;
    private int currentInputDir = 5;

    private void Awake()
    {
        inputTime[0] = 0;
        inputTime[1] = 0;
        inputTime[2] = 0;
        inputTime[3] = 0;
    }

    private void Update()
    {
        // 상 이동
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputTime[0] += Time.deltaTime;
        }

        // 하 이동
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputTime[1] += Time.deltaTime;
        }

        // 좌 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputTime[2] += Time.deltaTime;
        }

        // 우 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputTime[3] += Time.deltaTime;
        }


        //가장 최근 눌린 키 확인
        for (int i = 0; i < 4; i++)
        {
            if (minInputTime > inputTime[i] && inputTime[i] != 0)
            {
                minInputTime = inputTime[i];
                currentInputDir = i;
            }
        }


        //키 놓았을 때 초기화

        //상
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            inputTime[0] = 0;
            minInputTime = 5;
            if (currentInputDir == 0)
            {
                minInputTime = 5;
                currentInputDir = 5;
                movementInput = Vector2.zero;
            }
        }

        //하
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            inputTime[1] = 0;
            minInputTime = 5;
            if (currentInputDir == 1)
            {
                minInputTime = 5;
                currentInputDir = 5;
                movementInput = Vector2.zero;
            }
        }

        //좌
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            inputTime[2] = 0;
            minInputTime = 5;
            if (currentInputDir == 2)
            {
                minInputTime = 5;
                currentInputDir = 5;
                movementInput = Vector2.zero;
            }
        }

        //우
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            inputTime[3] = 0;
            minInputTime = 5;
            if (currentInputDir == 3)
            {
                minInputTime = 5;
                currentInputDir = 5;
                movementInput = Vector2.zero;
            }
        }


        //가장 최근에 입력된 값을 기준으로 현재 움직임 결정
        switch (currentInputDir)
        {
            case 0:
                //상
                movementInput = Vector2.up;
                dir = 0;
                break;
            case 1:
                //하
                movementInput = Vector2.down;
                dir = 1;
                break;
            case 2:
                //좌
                movementInput = Vector2.left;
                dir = 2;
                break;
            case 3:
                //우
                movementInput = Vector2.right;
                dir = 3;
                break;
            default:
                movementInput = Vector2.zero;
                break;
        }

        Debug.DrawRay(transform.position, new Vector3(movementInput.normalized.x, movementInput.normalized.y, 0), new Color(0, 1, 0));

        if(currentInputDir < 2)
        {
            //벽과 닿는 중이라면

            RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
            RaycastHit2D hit_2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

            if(hit_1.collider != null || hit_2.collider != null)
            {

                //상하 움직임이라면
                RaycastHit2D hit_l = Physics2D.Raycast(new Vector2(transform.position.x - 0.45f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
                RaycastHit2D hit_r = Physics2D.Raycast(new Vector2(transform.position.x + 0.45f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

                if (hit_l.collider != null && hit_r.collider != null)
                {
                    //플레이어가 닿고 있는 벽의 좌, 우측 벽이 모두 있는 경우 정지
                    Debug.Log("상하 움직임 막힘");
                    movementInput = Vector2.zero;
                }
                else if (hit_r.collider == null)
                {
                    //플레이어가 닿고있는 벽의 우측 벽이 없을 경우 오른쪽으로 우선 이동\
                    movementInput = Vector2.right;
                    dir = 3;
                }
                else
                {
                    //플레이어가 닿고있는 벽의 좌측 벽이 없을 경우 왼쪽으로 우선 이동\
                    movementInput = Vector2.left;
                    dir = 2;
                }
            }
        }
        else if(currentInputDir < 5)
        {
            //벽과 닿는 중이라면

            RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.4f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
            RaycastHit2D hit_2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

            if (hit_1.collider != null || hit_2.collider != null)
            {

                //좌우 움직임이라면
                RaycastHit2D hit_u = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.45f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
                RaycastHit2D hit_d = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

                if (hit_u.collider != null && hit_d.collider != null)
                {
                    //플레이어가 닿고 있는 벽의 상, 하단 벽이 모두 있는 경우 정지
                    Debug.Log("좌우 움직임 막힘");
                    movementInput = Vector2.zero;
                }
                else if (hit_d.collider == null)
                {
                    //플레이어가 닿고있는 벽의 하단 벽이 없을 경우 아래쪽으로 우선 이동
                    movementInput = Vector2.down;
                    dir = 1;
                }
                else
                {
                    //플레이어가 닿고있는 벽의 상단 벽이 없을 경우 위쪽으로 우선 이동
                    movementInput = Vector2.up;
                    dir = 0;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (isReversed)
        {
            this.transform.Translate(movementInput.normalized * playerMoveSpeed * 0.016f * -1f);
        }
        else
        {
            this.transform.Translate(movementInput.normalized * playerMoveSpeed * 0.016f);
        }
    }


    public IEnumerator Reverse()
    {
        isReversed = true;
        yield return new WaitForSeconds(reverseDelayTime);
        StartCoroutine(ExitReverse());
    }

    private IEnumerator ExitReverse()
    {
        isReversed = false;
        yield return null;
    }
}