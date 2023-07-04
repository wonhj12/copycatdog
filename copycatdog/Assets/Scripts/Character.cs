using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]

public class Character : MonoBehaviour
{
    //���� ���� ����
    [Header("���� ���� ����")]
    public bool isRiding;
    public bool isDamaged;
    public bool isAlive = true;


    //ĳ���� �Ӽ�
    [Header("ĳ���� �Ӽ�")]
    public string characterName;

    [SerializeField] protected int currentBubble;   //���� �ʿ� ��ġ ������ ��ǳ�� ����
    public int carryBubble;     //���� ���ٴ� �� �ִ� �ִ� ��ǳ�� ����
    public int maxBubble;       //�ִ�� ���ٴ� �� �ִ� ��ǳ���� ����
                                //currentBubble�� �׻� carryBubble ������
                                //carryBubble�� �׻� maxBubble ������

    public int currentAtkLength;    //���� ��ǳ���� ��ġ�ϸ� ���ư��� ����
    public int maxAtkLength;        //��ǳ���� ���� �� ���ư� �� �ִ� ����Ÿ�

    public float currentSpeed;      //���� �̵��ӵ�
    public float maxSpeed;          //�ִ� �̵��ӵ�

    protected float bubbleExplodeTime;


    //�ʿ��� ������Ʈ
    [Header("�ʿ��� ������Ʈ")]
    //��ǳ�� ������
    public GameObject Bubble;



    protected void Awake()
    {
        bubbleExplodeTime = Bubble.GetComponent<Bubble>().explodeTime;
    }


    protected void Update()
    {
        //���� ��ư�� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    protected void Attack()
    {
        //��ǳ���� �� ��ġ�� �� �ִٸ�
        if(currentBubble > 0)
        {
            //��ǳ�� ��ġ ��ġ ���
            Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

            //�ӽ÷� �ۼ��� �ڵ�
            Instantiate(Bubble, AttackLocation, Quaternion.identity);

            //��ǳ�� �������� Instantiate�ϰ� ���� ĳ���Ͱ� ���� �� �ִ� ��ǳ���� ���̸� �������ִ� �ڵ�
            
            GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
            bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
            

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


    protected void GetItem(/*Item item*/)
    {
        //������ ��� ���� �� ����
        //������ ������ �迭���ٰ� ���� �������� ����

        //for ������ �κ��丮 ���� ���� ������ �������� �ִ��� Ȯ��
        //�ִٸ� ������ ������Ű��
        Debug.Log("Item Find");
    }


    protected void UseItem()
    {
        //������ ��� ���� �� ����
        //������ ȿ������ ��Ƴ��� ��ũ��Ʈ ȣ��
        Debug.Log("Item Used");
    }


    public void Dead()
    {
        Debug.Log("Died");
        isAlive = false;
    }
}
