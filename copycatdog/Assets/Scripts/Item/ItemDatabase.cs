using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    [Header("������ ȿ�� ������")]
    public static float speed = 2;
    public static int length = 1;
    public static int bubble = 1;

    public static void Use(int key, Character player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Character playerController = player.GetComponent<Character>();

        for (int i = 0; i < playerController.effectIndex.Length; i++)
        {
            if (playerController.effectIndex[i] == -1)
            {
                playerController.effectIndex[i] = key;
                break;
            }
        }
        

        switch (key)
        {

            /*  ������ ������  */
            case 101:
                //�ѷ�������Ʈ
                if (playerMovement.maxPlayerMoveSpeed >= playerMovement.playerMoveSpeed + speed)
                {
                    playerMovement.playerMoveSpeed += speed;
                }
                else
                {
                    playerMovement.playerMoveSpeed = playerMovement.maxPlayerMoveSpeed;
                }
                break;

            case 102:
                //����
                if (playerController.maxAtkLength >= playerController.currentAtkLength + length)
                {
                    playerController.currentAtkLength += length;
                }
                else
                {
                    playerController.currentAtkLength = playerController.maxAtkLength;
                }
                break;

            case 103:
                //��ǳ��
                if (playerController.maxBubble >= playerController.carryBubble + bubble)
                {
                    playerController.carryBubble += bubble;
                    playerController.currentBubble = playerController.carryBubble;
                }
                else
                {
                    playerController.carryBubble = playerController.maxBubble;
                    playerController.currentBubble = playerController.maxBubble;
                }
                break;

            case 104:
                //�ذ� ��ǳ��
                playerController.currentAtkLength = playerController.maxAtkLength;
                break;

            case 105:
                //����
                playerController.isThrowAvailable = true;
                break;

            case 106:
                //�ȭ
                playerController.isPushAvailable = true;
                break;

            case 107:
                //�ڼ�
                break;


            /*  ������ ������  */
            case 201:
                //������
                int randInt = Random.Range(0, 2);
                Debug.Log("������");
                switch (randInt)
                {
                    case 0:
                        playerMovement.StartCoroutine(playerMovement.Reverse());
                        break;
                    case 1:
                        playerController.StartCoroutine(playerController.RandomAttack());
                        break;
                }
                break;

            case 202:
                //�ʷ� �Ǹ�
                switch (playerController.effectIndex[0])
                {
                    case 101:
                        playerMovement.playerMoveSpeed -= speed;
                        break;
                    case 102:
                        playerController.currentAtkLength -= length;
                        break;
                    case 103:
                        playerController.carryBubble -= bubble;
                        playerController.currentBubble = playerController.carryBubble;
                        break;
                    case 104:
                        int bottleRemain = 0;
                        for (int i = 0; i < playerController.effectIndex.Length; i++)
                        {
                            if (playerController.effectIndex[i] == 102)
                            {
                                bottleRemain += 1;
                            }
                        }
                        playerController.currentAtkLength = playerController.initialAtkLength + length * bottleRemain;      //���� ���ٱ��� ���̸� �ʱ� ���ٱ� ���̿� ȹ���� ������ ������ŭ ȿ���� ���Ͽ� �����ϴ� �ڵ�
                        break;
                    case 105:
                        playerController.isThrowAvailable = false;
                        break;
                    case 106:
                        playerController.isPushAvailable = false;
                        break;
                    case 107:

                        break;
                }
                
                break;


            /*  �Ҹ��� ������  */
            case 301:
                //�ٳ���
                playerMovement.BananaCreate();
                break;

            case 302:
                //Ʈ��
                break;

            case 303:
                //����
                playerMovement.BondCreate();
                break;

            case 304:
                //�ٴ�
                if(playerController.isDamaged == true)
                {
                    //�������� ���� ���¶�� �ٴ� ���
                    playerController.UnDamage();
                }
                else
                {
                    //�������� ���� ���� ���¶�� ������ ����
                    Debug.Log("�ٴ� ��� ����! �������� ���� ���°� �ƴ�.");
                    playerController.RestoreItem(304);
                }
                break;

            case 305:
                //��Ʈ
                break;

            case 306:
                //����
                break;

            case 307:
                //������
                break;

            case 308:
                //�ǵ�
                playerController.StartCoroutine(playerController.ShieldActivate());
                break;


            /*  Ż��  */
            case 401:
                //�ź���
                break;

            case 402:
                //�ξ���
                break;

            case 403:
                //���ּ�
                break;
        }
    }

}
