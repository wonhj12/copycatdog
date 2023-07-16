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
        switch (key)
        {
            case 1:
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
            case 2:
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
            case 3:
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
            case 4:
                //�ذ� ��ǳ��

                playerController.carryBubble = playerController.maxBubble;
                playerController.currentBubble = playerController.maxBubble;

                break;
            case 5:
                //����
                break;
            case 6:
                //�ȭ
                break;
            case 7:
                //������
                break;
        }
    }

}
