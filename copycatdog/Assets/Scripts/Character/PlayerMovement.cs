using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    public int playerNum = 1;

    //�ʿ��� ������Ʈ
    [Header("�ʿ��� ������Ʈ")]
    public GameObject bondObject;
    public GameObject bananaObject;
    public GameObject trapObject;
    private Rigidbody2D rigid;
    private Character player;
    private Animator anim;

    [Header("����")]
    private int currentInputDir = 5;
    public Vector2 movementInput;
    public int lookingDir;      //0123 : �����¿�, ���� ���´� ����.
    public int dir = 4;         //4 : ����, 0123 : �����¿�

    [Header("�� ���󰡱� �ý��� ���� ������")]
    public float checkWallLimit;        //0.4f
    public float executeLimit;          //0.45f

    [Header("�� ���� ������")]
    private int slipDir;
    public bool isReversed = false;
    public bool isSlipped = false;
    private bool isFacingWall = false;
    public float reverseDelayTime;
    public float slipSpeed;
    private float slipPreviousSpeed;

    [Header("Ż�� ���� ������")]
    [SerializeField] private float turtleSpeed;
    [SerializeField] private float owlSpeed;
    [SerializeField] private float ufoSpeed;
    [SerializeField] private bool isUFO = false;
    private float previousSpeed;

    //�ν����� â�� ���̴� �͵�
    [Header("�÷��̾� �ӵ�")]
    public float maxPlayerMoveSpeed;
    public float playerMoveSpeed;

    [Header("�����¿�Ű�� ���� �ð�")]
    [SerializeField] private float[] inputTime = new float[4];

    private float minInputTime = 5;

    private void Awake()
    {
        player = GetComponent<Character>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                lookingDir = 0;
                dir = 0;
                break;
            case 1:
                //��
                movementInput = Vector2.down;
                lookingDir = 1;
                dir = 1;
                break;
            case 2:
                //��
                movementInput = Vector2.left;
                lookingDir = 2;
                dir = 2;
                break;
            case 3:
                //��
                movementInput = Vector2.right;
                lookingDir = 3;
                dir = 3;
                break;
            default:
                movementInput = Vector2.zero;
                dir = 4;
                break;
        }



        //�� ���󰡱� �ý���

        Vector2 WallFollowDirection = movementInput;
        if (isReversed)
        {
            WallFollowDirection = WallFollowDirection * (-1);
        }

        Debug.DrawRay(transform.position, new Vector3(WallFollowDirection.normalized.x, WallFollowDirection.normalized.y, 0), new Color(0, 1, 0));

        if (currentInputDir < 2)
        {
            //���� ��� ���̶��

            RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x - checkWallLimit, transform.position.y), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("WorldLimit"));
            RaycastHit2D hit_2 = Physics2D.Raycast(new Vector2(transform.position.x + checkWallLimit, transform.position.y), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("WorldLimit"));
            if (isUFO == false)
            {
                hit_1 = Physics2D.Raycast(new Vector2(transform.position.x - checkWallLimit, transform.position.y), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));
                hit_2 = Physics2D.Raycast(new Vector2(transform.position.x + checkWallLimit, transform.position.y), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));
            }

            Debug.DrawRay(new Vector2(transform.position.x - executeLimit, transform.position.y), WallFollowDirection.normalized, new Color(1, 0, 0));
            Debug.DrawRay(new Vector2(transform.position.x + executeLimit, transform.position.y), WallFollowDirection.normalized, new Color(1, 0, 0));

            if (hit_1.collider != null || hit_2.collider != null)
            {

                //���� �������̶��
                RaycastHit2D hit_l = Physics2D.Raycast(new Vector2(transform.position.x - executeLimit, transform.position.y), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));
                RaycastHit2D hit_r = Physics2D.Raycast(new Vector2(transform.position.x + executeLimit, transform.position.y), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));

                if (hit_l.collider != null && hit_r.collider != null)
                {
                    //�÷��̾ ��� �ִ� ���� ��, ���� ���� ��� �ִ� ��� ����
                    //Debug.Log("���� ������ ����");
                    movementInput = Vector2.zero;
                    dir = 4;
                }
                else if (hit_r.collider == null)
                {
                    //�÷��̾ ����ִ� ���� ���� ���� ���� ��� ���������� �켱 �̵�
                    movementInput = Vector2.right;
                    lookingDir = 3;
                    dir = 3;
                    if (isReversed)
                    {
                        movementInput = Vector2.left;
                        lookingDir = 2;
                        dir = 2;
                    }
                }
                else
                {
                    //�÷��̾ ����ִ� ���� ���� ���� ���� ��� �������� �켱 �̵�\
                    movementInput = Vector2.left;
                    lookingDir = 2;
                    dir = 2;
                    if (isReversed)
                    {
                        movementInput = Vector2.right;
                        lookingDir = 3;
                        dir = 3;
                    }
                }
            }
        }
        else if (currentInputDir < 4)
        {
            //���� ��� ���̶��

            RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + checkWallLimit), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("WorldLimit"));
            RaycastHit2D hit_2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - checkWallLimit), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("WorldLimit"));
            if (isUFO == false)
            {
                hit_1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + checkWallLimit), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));
                hit_2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - checkWallLimit), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));
            }

            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + executeLimit), WallFollowDirection.normalized, new Color(1, 0, 0));
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - executeLimit), WallFollowDirection.normalized, new Color(1, 0, 0));

            if (hit_1.collider != null || hit_2.collider != null)
            {

                //�¿� �������̶��
                RaycastHit2D hit_u = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + executeLimit), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));
                RaycastHit2D hit_d = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - executeLimit), WallFollowDirection.normalized, 0.51f, LayerMask.GetMask("Wall", "WorldLimit"));

                //Debug.Log((hit_d != null) + " " + (hit_u != null));

                if (hit_u.collider != null && hit_d.collider != null)
                {
                    //�÷��̾ ��� �ִ� ���� ��, �ϴ� ���� ��� �ִ� ��� ����
                    //Debug.Log("�¿� ������ ����");
                    movementInput = Vector2.zero;
                    dir = 0;
                }
                else if (hit_d.collider == null)
                {
                    //�÷��̾ ����ִ� ���� �ϴ� ���� ���� ��� �Ʒ������� �켱 �̵�
                    movementInput = Vector2.down;
                    lookingDir = 1;
                    dir = 1;
                    if (isReversed)
                    {
                        movementInput = Vector2.up;
                        lookingDir = 0;
                        dir = 0;
                    }
                }
                else
                {
                    //�÷��̾ ����ִ� ���� ��� ���� ���� ��� �������� �켱 �̵�
                    movementInput = Vector2.up;
                    lookingDir = 0;
                    dir = 0;
                    if (isReversed)
                    {
                        movementInput = Vector2.down;
                        lookingDir = 1;
                        dir = 1;
                    }
                }
            }
        }

        anim.SetFloat("moveDir", dir);

    }

    private void FixedUpdate()
    {
        if (!isSlipped)
        {
            if (isReversed)
            {
                this.transform.Translate(movementInput.normalized * playerMoveSpeed * 0.016f * -1f);
            }
            else
            {
                this.transform.Translate(movementInput.normalized * playerMoveSpeed * 0.016f);
                //Debug.Log(movementInput);
                //rigid.velocity = movementInput.normalized * playerMoveSpeed ;
            }
        }
        else
        {
            switch (slipDir)
            {
                case 0:
                    this.transform.Translate(Vector2.up * playerMoveSpeed * 0.016f);
                    RaycastHit2D hit_1 = Physics2D.Raycast(this.transform.position, Vector2.up, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_1.transform != null)
                    {
                        isFacingWall = true;
                    }
                    else
                    {
                        isFacingWall = false;
                    }
                    break;
                case 1:
                    this.transform.Translate(Vector2.down * playerMoveSpeed * 0.016f);
                    RaycastHit2D hit_2 = Physics2D.Raycast(this.transform.position, Vector2.down, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_2.transform != null)
                    {
                        isFacingWall = true;
                    }
                    else
                    {
                        isFacingWall = false;
                    }
                    break;
                case 2:
                    this.transform.Translate(Vector2.left * playerMoveSpeed * 0.016f);
                    RaycastHit2D hit_3 = Physics2D.Raycast(this.transform.position, Vector2.left, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_3.transform != null)
                    {
                        isFacingWall = true;
                    }
                    else
                    {
                        isFacingWall = false;
                    }
                    break;
                case 3:
                    this.transform.Translate(Vector2.right * playerMoveSpeed * 0.016f);
                    RaycastHit2D hit_4 = Physics2D.Raycast(this.transform.position, Vector2.right, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_4.transform != null)
                    {
                        isFacingWall = true;
                    }
                    else
                    {
                        isFacingWall = false;
                    }
                    break;

            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Obstacle"))
        {
            if (isSlipped && isFacingWall)
            {
                playerMoveSpeed = slipPreviousSpeed;
                isSlipped = false;
            }
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

    public IEnumerator Slow()
    {
        float speed = playerMoveSpeed;
        playerMoveSpeed = 0.5f;
        yield return new WaitForSeconds(10);
        StartCoroutine(ExitSlow(speed));
    }

    private IEnumerator ExitSlow(float speed)
    {
        playerMoveSpeed = speed;
        yield return null;
    }

    public void BondCreate()
    {
        Instantiate(bondObject, new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)), transform.rotation);
    }

    public void BananaCreate()
    {
        Instantiate(bananaObject, new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)), transform.rotation);
    }

    public IEnumerator Slip()
    {
        slipDir = lookingDir;
        isSlipped = true;
        slipPreviousSpeed = playerMoveSpeed;
        playerMoveSpeed = slipSpeed;
        yield return null;
    }

    public void TrapCreate()
    {
        Instantiate(trapObject, new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)), transform.rotation);
    }

    public void Board(int key)
    {
        player.isBoarding = true;
        //isBoarding = true;

        previousSpeed = playerMoveSpeed;

        switch (key)
        {
            //�ӵ� ����
            //��� + 1
            //������ �Ծ ���� �������� ��(������) �Ϲݻ��·� �����ǵ���
            case 401:
                playerMoveSpeed = turtleSpeed;
                break;

            case 402:
                playerMoveSpeed = owlSpeed;
                break;

            case 403:
                //���ּ��̶� �� �׵θ��� �ƴϸ� �浹 ����
                playerMoveSpeed = ufoSpeed;
                isUFO = true;

                this.gameObject.layer = 13;
                break;
        }
    }

    public IEnumerator UnBoard()
    {
        player.isBoarding = false;

        player.isShieldAvailable = true;
        playerMoveSpeed = 0;
        yield return new WaitForSeconds(1);
        player.isShieldAvailable = false;
        playerMoveSpeed = previousSpeed;
    }
}