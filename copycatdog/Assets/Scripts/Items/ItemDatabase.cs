using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [Header("아이템 효과 변수들")]
    [SerializeField] private float speed;
    [SerializeField] private int length;

    public void Use(int key, Character player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Character playerController = player.GetComponent<Character>();
        switch (key)
        {
            case 1:
                //롤러스케이트

                if (playerMovement.maxPlayerMoveSpeed >= playerMovement.playerMoveSpeed + speed)
                    playerMovement.playerMoveSpeed += speed;
                else
                    playerMovement.playerMoveSpeed = playerMovement.maxPlayerMoveSpeed;

                break;
            case 2:
                //물병

                if (playerController.maxAtkLength >= playerController.currentAtkLength + length)
                    playerController.currentAtkLength += length;
                else
                    playerController.currentAtkLength = playerController.maxAtkLength;
                break;
            case 3:
                break;
        }
    }

}
