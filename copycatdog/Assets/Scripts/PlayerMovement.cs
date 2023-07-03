using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //�ʿ��� ������Ʈ
    private Rigidbody2D playerRb;
    private Vector2 movementInput;

    //�ν����� â�� ���̴� �͵�
    [Header("�÷��̾� �ӵ�")]
    [SerializeField] private float playerMoveSpeed;

    [Header("�����¿�Ű�� ���� �ð�")]
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
        // �� �̵�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputTime[0] += Time.deltaTime;
        }

        // �� �̵�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputTime[1] += Time.deltaTime;
        }

        // �� �̵�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputTime[2] += Time.deltaTime;
        }

        // �� �̵�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputTime[3] += Time.deltaTime;
        }


        //���� �ֱ� ���� Ű Ȯ��
        for (int i = 0; i < 4; i++)
        {
            if (minInputTime > inputTime[i] && inputTime[i] != 0)
            {
                minInputTime = inputTime[i];
                currentInputDir = i;
            }
        }


        //Ű ������ �� �ʱ�ȭ

        //��
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

        //��
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

        //��
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

        //��
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


        //���� �ֱٿ� �Էµ� ���� �������� ���� ������ ����
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