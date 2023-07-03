using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //필요한 컴포넌트
    private Rigidbody2D playerRb;
    private Vector2 movementInput;

    //인스펙터 창에 보이는 것들
    [Header("플레이어 속도")]
    [SerializeField] private float playerMoveSpeed;

    [Header("상하좌우키가 눌린 시간")]
    [SerializeField] private float[] inputTime = new float[4];

    private float minInputTime = 5;
    private int currentInputDir = 5;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
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
                movementInput = Vector2.up;
                break;
            case 1:
                movementInput = Vector2.down;
                break;
            case 2:
                movementInput = Vector2.left;
                break;
            case 3:
                movementInput = Vector2.right;
                break;
            default:
                movementInput = Vector2.zero;
                break;
        }
    }

    private void FixedUpdate()
    {
        this.transform.Translate(movementInput.normalized * playerMoveSpeed * 0.016f);
    }
}