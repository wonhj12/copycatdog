using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]

public class Character : MonoBehaviour
{
    //���� ���� ����
    [Header("���� ���� ����")]
    public bool isRiding;
    public bool isDamaged = false;
    public bool isAlive = true;
    public bool isBubbleDeployed = false;
    public bool isThrowAvailable = false;
    public Bubble bubble;


    //ĳ���� �Ӽ�
    [Header("ĳ���� �Ӽ�")]
    public string characterName;

    public int currentBubble;   //���� �ʿ� ��ġ ������ ��ǳ�� ����
    public int carryBubble;     //���� ���ٴ� �� �ִ� �ִ� ��ǳ�� ����
    public int maxBubble;       //�ִ�� ���ٴ� �� �ִ� ��ǳ���� ����
                                //currentBubble�� �׻� carryBubble ������
                                //carryBubble�� �׻� maxBubble ������

    public int currentAtkLength;    //���� ��ǳ���� ��ġ�ϸ� ���ư��� ����
    public int maxAtkLength;        //��ǳ���� ���� �� ���ư� �� �ִ� ����Ÿ�

    public float currentSpeed;      //���� �̵��ӵ�
    public float maxSpeed;          //�ִ� �̵��ӵ�

    protected float bubbleExplodeTime;


    [Header("������ �κ��丮")]
    [SerializeField] private int[] inventory = new int[2];


    //�ʿ��� ������Ʈ
    [Header("�ʿ��� ������Ʈ")]
    private PlayerMovement playerMovement;
    [SerializeField] private Item currentItem;
    //��ǳ�� ������
    public GameObject Bubble;



    protected void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        bubbleExplodeTime = Bubble.GetComponent<Bubble>().explodeTime;
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = 999;
        }
    }


    protected void Update()
    {
        //����ִ� ����
        if (isAlive)
        {
            //���� ��ư�� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }

            UseItem();
        }
    }


    protected void Attack()
    {
        //��ǳ���� �� ��ġ�� �� �ִٸ�, �׸��� �� �ڸ��� ��ġ�� ��ǳ���� ���ٸ�
        if(currentBubble > 0)
        {
            if (!isBubbleDeployed)
            {
                //��ǳ�� ��ġ ��ġ ���
                Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

                //��ǳ�� �������� Instantiate�ϰ� ���� ĳ���Ͱ� ���� �� �ִ� ��ǳ���� ���̸� �������ִ� �ڵ�
                GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
                bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
                bubble = bubbleObject.GetComponent<Bubble>();

                //��ǳ�� ���
                currentBubble -= 1;

                //��ǳ�� ������
                StartCoroutine(RestoreBubble());
            }
            else if(isThrowAvailable)
            {
                int direction = playerMovement.dir;

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


    protected void RandomAttack()
    {
        //������ ȿ�� �� �ϳ���

        if (currentBubble > 0 && !isBubbleDeployed)
        {
            //��ǳ�� ��ġ ��ġ ���
            Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

            //��ǳ�� �������� Instantiate�ϰ� ���� ĳ���Ͱ� ���� �� �ִ� ��ǳ���� ���̸� �������ִ� �ڵ�
            GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
            bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
            bubble = bubbleObject.GetComponent<Bubble>();

            //��ǳ�� ���
            currentBubble -= 1;

            //��ǳ�� ������
            StartCoroutine(RestoreBubble());
        }
    }


    protected IEnumerator RestoreBubble()
    {
        //��ǳ���� �Ӽ� �� �� ����������� �ð��� ������, �ش� �ð����� ��� �� �Ʒ��� �ڵ� ����
        yield return new WaitForSeconds(bubbleExplodeTime);

        //���� ���� ��ġ ������ ��ǳ���� ���� ���� ���ٴ� �� �ִ� �ִ� ��ǳ�� �������� ������ ������
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


    //��ǳ���� �� �ڸ��� ��ġ�Ǿ��ٴ� ��ȣ �Է�
    public void SetDeploy()
    {
        isBubbleDeployed = true;
    }


    //��ǳ���� �� �ڸ��� �� �̻� ��ġ���� ���� ���¶�� ��ȣ �Է�
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


    protected void UseItem()
    {
        //������ ��� ���� �� ����
        //������ ȿ������ ��Ƴ��� ��ũ��Ʈ ȣ��

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (inventory[0] != 999)
            {
                //�� ����� ������ ���Ŀ� ItemDatabase ��ũ��Ʈ �ۼ� ��, �����ͺ��̽� ������Ʈ���� ����ϵ��� �� ����.
                //�����ͺ��̽� ������Ʈ���� ������ Ű ���� �����۵��� �Ҵ�Ǿ� ���� ����.
                //�� �Լ��� �����ͺ��̽��� ItemDatabase ��ũ��Ʈ�� Use�Լ��� ȣ���ϰ�, Use�Լ����� �÷��̾�� ȿ���� �ο��� ��.
                //�׷��� �÷��̾ �þ�� �ϳ��� �����ͺ��̽����� ���� ȿ�������� ȿ���� ������ �� ����.

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


        //�ٴ� ��� ��ư�� ������ (�����ڵ�)
        /*
        if (Input.GetKeyDown(KeyCode.LeftControl) && isDamaged)
        {
            UnDamage();
        }
        */
    }


    public void Damage()
    {
        if (!isDamaged)
        {
            isDamaged = true;
            Debug.Log("Damaged");


            //�������� ���� �ִϸ��̼� ����
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            playerMovement.playerMoveSpeed *= 0.1f;

            StartCoroutine(Die());
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
