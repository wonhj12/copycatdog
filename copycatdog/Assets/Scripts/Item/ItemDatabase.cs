using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    [Header("?????? ???? ??????")]
    public static float speed = 1;
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

            /*  ?????? ??????  */
            case 101:
                //????????????
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
                //????
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
                //??????
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
                //???? ??????
                playerController.currentAtkLength = playerController.maxAtkLength;
                break;

            case 105:
                //????
                playerController.isThrowAvailable = true;
                break;

            case 106:
                //??????
                playerController.isPushAvailable = true;
                break;


            /*  ?????? ??????  */
            case 201:
                //??????
                int randInt = Random.Range(0, 2);
                Debug.Log("??????");
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
                //???? ????
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
                        playerController.currentAtkLength = playerController.initialAtkLength + length * bottleRemain;      //???? ???????? ?????? ???? ?????? ?????? ?????? ?????? ???????? ?????? ?????? ???????? ????
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


            /*  ?????? ??????  */
            case 301:
                //??????
                playerMovement.BananaCreate();
                break;

            case 302:
                //????
                playerMovement.TrapCreate();
                break;

            case 303:
                //????
                playerMovement.BondCreate();
                break;

            case 304:
                //????
                if(playerController.isDamaged == true)
                {
                    //???????? ???? ???????? ???? ????
                    playerController.UnDamage();
                }
                else
                {
                    //???????? ???? ???? ???????? ?????? ????
                    Debug.Log("???? ???? ????! ???????? ???? ?????? ????.");
                    playerController.RestoreItem(304);
                }
                break;

            case 305:
                //????
                break;

            case 306:
                //????
                break;

            case 307:
                //??????
                break;

            case 308:
                //????
                playerController.StartCoroutine(playerController.ShieldActivate());
                break;


            /*  ????  */
            case 401:
                //??????
                playerMovement.Board(401);
                break;

            case 402:
                //??????
                playerMovement.Board(402);
                break;

            case 403:
                //??????
                playerMovement.Board(403);
                break;
        }
    }

}
