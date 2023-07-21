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

            /*  버프형 아이템  */
            case 101:
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

            case 102:
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

            case 103:
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

            case 104:
                //해골 물풍선
                playerController.currentAtkLength = playerController.maxAtkLength;
                break;

            case 105:
                //권투
                playerController.isThrowAvailable = true;
                break;

            case 106:
                //운동화
                playerController.isPushAvailable = true;
                break;

            case 107:
                //자석
                break;


            /*  너프형 아이템  */
            case 201:
                //렐렐레
                int randInt = Random.Range(0, 2);
                Debug.Log("렐렐레");
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
                //초록 악마
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
                        playerController.currentAtkLength = playerController.initialAtkLength + length * bottleRemain;      //현재 물줄기의 길이를 초기 물줄기 길이에 획득한 물병의 개수만큼 효과를 합하여 지정하는 코드
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


            /*  소모형 아이템  */
            case 301:
                //바나나
                playerMovement.BananaCreate();
                break;

            case 302:
                //트랩
                break;

            case 303:
                //본드
                playerMovement.BondCreate();
                break;

            case 304:
                //바늘
                if(playerController.isDamaged == true)
                {
                    //데미지를 입은 상태라면 바늘 사용
                    playerController.UnDamage();
                }
                else
                {
                    //데미지를 입지 않은 상태라면 아이템 복구
                    Debug.Log("바늘 사용 실패! 데미지를 입은 상태가 아님.");
                    playerController.RestoreItem(304);
                }
                break;

            case 305:
                //다트
                break;

            case 306:
                //센서
                break;

            case 307:
                //스프링
                break;

            case 308:
                //실드
                playerController.StartCoroutine(playerController.ShieldActivate());
                break;


            /*  탈것  */
            case 401:
                //거북이
                break;

            case 402:
                //부엉이
                break;

            case 403:
                //우주선
                break;
        }
    }

}
