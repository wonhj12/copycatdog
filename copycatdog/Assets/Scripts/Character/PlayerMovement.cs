using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //�ʿ��� ������Ʈ
    public Vector2 movementInput;
    public int dir;
    public bool isReversed = false;
    public float reverseDelayTime;

    //�ν����� â�� ���̴� �͵�
    [Header("�÷��̾� �ӵ�")]
    public float maxPlayerMoveSpeed;
    public float playerMoveSpeed;

    [Header("�����¿�Ű�� ���� �ð�")]
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
                //��
                movementInput = Vector2.up;
                dir = 0;
                break;
            case 1:
                //��
                movementInput = Vector2.down;
                dir = 1;
                break;
            case 2:
                //��
                movementInput = Vector2.left;
                dir = 2;
                break;
            case 3:
                //��
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
            //���� ��� ���̶��

            RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
            RaycastHit2D hit_2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

            if(hit_1.collider != null || hit_2.collider != null)
            {

                //���� �������̶��
                RaycastHit2D hit_l = Physics2D.Raycast(new Vector2(transform.position.x - 0.45f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
                RaycastHit2D hit_r = Physics2D.Raycast(new Vector2(transform.position.x + 0.45f, transform.position.y), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

                if (hit_l.collider != null && hit_r.collider != null)
                {
                    //�÷��̾ ��� �ִ� ���� ��, ���� ���� ��� �ִ� ��� ����
                    Debug.Log("���� ������ ����");
                    movementInput = Vector2.zero;
                }
                else if (hit_r.collider == null)
                {
                    //�÷��̾ ����ִ� ���� ���� ���� ���� ��� ���������� �켱 �̵�\
                    movementInput = Vector2.right;
                    dir = 3;
                }
                else
                {
                    //�÷��̾ ����ִ� ���� ���� ���� ���� ��� �������� �켱 �̵�\
                    movementInput = Vector2.left;
                    dir = 2;
                }
            }
        }
        else if(currentInputDir < 5)
        {
            //���� ��� ���̶��

            RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.4f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
            RaycastHit2D hit_2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

            if (hit_1.collider != null || hit_2.collider != null)
            {

                //�¿� �������̶��
                RaycastHit2D hit_u = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.45f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));
                RaycastHit2D hit_d = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), movementInput.normalized, 0.51f, LayerMask.GetMask("Wall"));

                if (hit_u.collider != null && hit_d.collider != null)
                {
                    //�÷��̾ ��� �ִ� ���� ��, �ϴ� ���� ��� �ִ� ��� ����
                    Debug.Log("�¿� ������ ����");
                    movementInput = Vector2.zero;
                }
                else if (hit_d.collider == null)
                {
                    //�÷��̾ ����ִ� ���� �ϴ� ���� ���� ��� �Ʒ������� �켱 �̵�
                    movementInput = Vector2.down;
                    dir = 1;
                }
                else
                {
                    //�÷��̾ ����ִ� ���� ��� ���� ���� ��� �������� �켱 �̵�
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