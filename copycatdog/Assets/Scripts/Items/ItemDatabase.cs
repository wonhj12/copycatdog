using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    [Header("아이템 효과 변수들")]
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
                //롤러스케이트

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
                //물병

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
                //물풍선

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
                //해골 물풍선

                playerController.carryBubble = playerController.maxBubble;
                playerController.currentBubble = playerController.maxBubble;

                break;
            case 5:
                //권투
                break;
            case 6:
                //운동화
                break;
            case 7:
                //렐렐레
                break;
        }
    }

}
