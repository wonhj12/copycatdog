using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]

public class Character : MonoBehaviour
{
    [Header("???????? ????")]
    public int playerNum;

    public AudioSource audio;
    private KeyCode[] keySet = new KeyCode[2];
    public GameObject[] ping;
    private MapController mapController;

    //???? ???? ????
    [Header("???? ???? ????")]
    public bool isRiding;
    public bool isDamaged = false;
    public bool isAlive = true;
    public bool isBubbleDeployed = false;
    public bool isThrowAvailable = false;
    public bool isPushAvailable = false;
    public bool isShieldAvailable = false;
    public bool isBoarding = false;
    public Bubble bubble;


    //?????? ????
    [Header("?????? ???? - ????")]
    public string characterName;


    [Header("?????? ???? - ?????? ???? & ????")]
    public AudioClip bombSet;   // ?????? ???? audio
    public int currentBubble;   //???? ???? ???? ?????? ?????? ????
    public int carryBubble;     //???? ???????? ?? ???? ???? ?????? ????
    public int maxBubble;       //?????? ???????? ?? ???? ???????? ????
                                //currentBubble?? ???? carryBubble ??????
                                //carryBubble?? ???? maxBubble ??????


    [Header("?????? ???? - ?????? ????")]
    public int initialAtkLength;
    public int currentAtkLength;    //???? ???????? ???????? ???????? ????
    public int maxAtkLength;        //???????? ???? ?? ?????? ?? ???? ????????


    [Header("?????? ???? - ?????? ???? ???? ????")]
    public AudioClip eatItem;
    public float randMaxDelay;           //???????? ?? ???? ?????? ????
    public float randMinDelay;           //???????? ?? ???? ?????? ????

    private float currentRandomTime;    //???? ?????????? ?????? ???? (???????? ?????? ???? ????)
    public float randomSkillTime;       //???????? ?????? ???? ?? ???? (?????? ???? ????)

    public float shieldTime;


    //[Header("?????? ???? - ????????")]
    //public float currentSpeed;      //???? ????????
    //public float maxSpeed;          //???? ????????

    protected float bubbleExplodeTime;


    [Header("?????? ????????")]
    [SerializeField] private int[] inventory = new int[2];
    public int[] effectIndex = new int[999];


    //?????? ????????
    [Header("?????? ????????")]
    private PlayerMovement playerMovement;
    [SerializeField] private Item currentItem;
    //?????? ??????
    public GameObject Bubble;
    public Animator anim;
    private BoxCollider2D col;

    protected void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        bubbleExplodeTime = Bubble.GetComponent<Bubble>().explodeTime;
        audio = GetComponent<AudioSource>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();

        initialAtkLength = currentAtkLength;
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = 999;
        }
        for (int i = 0; i < effectIndex.Length; i++)
        {
            effectIndex[i] = -1;
        }

        if(playerNum == 1)
        {
            ping[0].SetActive(true);
            ping[1].SetActive(false);

            keySet[0] = KeyCode.F;
            keySet[1] = KeyCode.G;

        }else if(playerNum == 2)
        {
            ping[0].SetActive(false);
            ping[1].SetActive(true);

            keySet[0] = KeyCode.Greater;
            keySet[1] = KeyCode.Less;

        }
    }

    public void SetP1()
    {
        playerNum = 1;
        playerMovement.SetP1();
        ping[0].SetActive(true);
        ping[1].SetActive(false);

        keySet[0] = KeyCode.F;
        keySet[1] = KeyCode.G;
    }


    public void SetP2()
    {
        playerNum = 2;
        playerMovement.SetP2();
        ping[0].SetActive(false);
        ping[1].SetActive(true);

        keySet[0] = KeyCode.Comma;
        keySet[1] = KeyCode.Period;
    }


    protected void Update()
    {
        //???????? ????
        if (isAlive)
        {
            //???? ?????? ??????
            if (Input.GetKeyDown(keySet[0]))
            {
                Attack();
            }

            UseItem();
        }

        // number of bombs ui
        mapController.ShowBubble(playerNum, carryBubble);
    }


    protected void Attack()
    {
        //???????? ?? ?????? ?? ??????, ?????? ?? ?????? ?????? ???????? ??????
        if(currentBubble > 0)
        {
            if (!isBubbleDeployed)
            {
                //?????? ???? ???? ????
                Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

                //?????? ???????? Instantiate???? ???? ???????? ???? ?? ???? ???????? ?????? ?????????? ????
                GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
                bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
                bubble = bubbleObject.GetComponent<Bubble>();

                // ???? ????
                audio.clip = bombSet;
                audio.Play();

                //?????? ????
                currentBubble -= 1;

                //?????? ??????
                StartCoroutine(RestoreBubble());
            }
            else if(isThrowAvailable)
            {
                int direction = playerMovement.lookingDir;

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


    public IEnumerator RandomAttack()
    {
        //?????? ???? ?? ??????

        float randTime = Random.Range(randMinDelay, randMaxDelay);

        currentRandomTime += randTime;
        yield return new WaitForSeconds(randTime);

        if (currentBubble > 0 && !isBubbleDeployed)
        {
            //?????? ???? ???? ????
            Vector2 AttackLocation = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

            //?????? ???????? Instantiate???? ???? ???????? ???? ?? ???? ???????? ?????? ?????????? ????
            GameObject bubbleObject = Instantiate(Bubble, AttackLocation, Quaternion.identity);
            bubbleObject.GetComponent<Bubble>().Length = currentAtkLength;
            bubble = bubbleObject.GetComponent<Bubble>();

            //?????? ????
            currentBubble -= 1;

            //?????? ??????
            StartCoroutine(RestoreBubble());
        }

        Debug.Log(currentRandomTime + " " + randomSkillTime);
        if (currentRandomTime < randomSkillTime)
        {
            StartCoroutine(RandomAttack());
        }
    }


    protected IEnumerator RestoreBubble()
    {
        //???????? ???? ?? ?? ???????????? ?????? ??????, ???? ???????? ???? ?? ?????? ???? ????
        yield return new WaitForSeconds(bubbleExplodeTime);

        //???? ???? ???? ?????? ???????? ???? ???? ???????? ?? ???? ???? ?????? ???????? ?????? ??????
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


    //???????? ?? ?????? ???????????? ???? ????
    public void SetDeploy()
    {
        isBubbleDeployed = true;
    }


    //???????? ?? ?????? ?? ???? ???????? ???? ???????? ???? ????
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
                mapController.ShowItem(playerNum, i, item.itemImage);
                break;
            }
        }
    }


    public void RestoreItem(int key)
    {
        //?????? ???? ???? ?? ???????? ????
        for (int i = inventory.Length - 1; i >= 0; i--)
        {
            if (inventory[i] != 999)
            {
                inventory[i + 1] = inventory[i];
                inventory[i] = 999;
            }
        }
        inventory[0] = key;
    }


    protected void UseItem()
    {
        //?????? ???? ???? ?? ????
        //?????? ???????? ???????? ???????? ????

        if (Input.GetKeyDown(keySet[1]))
        {
            if (inventory[0] != 999)
            {
                //???????????? ???????????? ?????? ?? ???? ?????????? ???????? ????.
                //ItemDatabase ?????????? Use?????? ????????, Use???????? ???????????? ?????? ??????.
                //?????????? ???????? ?????? ???????????????? ???? ?????????? ?????? ?????? ?? ????.

                ItemDatabase.Use(inventory[0], this.GetComponent<Character>());

                Debug.Log(inventory[0] + "Item Used");

                inventory[0] = inventory[1];
                inventory[1] = 999;

                mapController.UseItem(playerNum, inventory[0]);
            }
            else
            {
                Debug.Log("No Item");
            }
        }
    }


    public IEnumerator ShieldActivate()
    {
        isShieldAvailable = true;
        yield return new WaitForSeconds(shieldTime);
        StartCoroutine(ShieldDeActivate());
    }

    protected IEnumerator ShieldDeActivate()
    {
        isShieldAvailable = false;
        yield return null;
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bubble") && isPushAvailable)
        {
            Debug.Log("????");
            int direction = playerMovement.lookingDir;


            switch (direction)
            {
                case 0:
                    RaycastHit2D hit_1 = Physics2D.Raycast(this.transform.position, Vector2.up, 0.51f, LayerMask.GetMask("Bubble"));

                    if(hit_1.transform != null)
                    {
                        if (hit_1.transform.tag == "Bubble")
                        {
                            hit_1.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
                case 1:
                    RaycastHit2D hit_2 = Physics2D.Raycast(this.transform.position, Vector2.down, 0.51f, LayerMask.GetMask("Bubble"));

                    if (hit_2.transform != null)
                    {
                        if (hit_2.transform.tag == "Bubble")
                        {
                            hit_2.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
                case 2:
                    RaycastHit2D hit_3 = Physics2D.Raycast(this.transform.position, Vector2.left, 0.51f, LayerMask.GetMask("Bubble"));

                    if (hit_3.transform != null)
                    {
                        if (hit_3.transform.tag == "Bubble")
                        {
                            hit_3.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
                case 3:
                    RaycastHit2D hit_4 = Physics2D.Raycast(this.transform.position, Vector2.right, 0.51f, LayerMask.GetMask("Bubble"));

                    if (hit_4.transform != null)
                    {
                        if (hit_4.transform.tag == "Bubble")
                        {
                            hit_4.transform.GetComponent<Bubble>().Slip(direction);
                        }
                    }
                    break;
            }
        }
    }


    public void Damage()
    {
        if (isBoarding)
        {
            playerMovement.StartCoroutine(playerMovement.UnBoard());
        }
        else
        {
            if (!isDamaged && !isShieldAvailable)
            {
                isDamaged = true;
                this.gameObject.layer = 14;
                col.isTrigger = true;

                //???????? ???? ?????????? ????
                anim.SetBool("isDamaged", true);
                playerMovement.playerMoveSpeed *= 0.1f;

                StartCoroutine(Die());
            }
        }
    }


    public void UnDamage()
    {
        isDamaged = false;
        this.gameObject.layer = 3;
        col.isTrigger = false;

        anim.SetBool("isDamaged", false);
        anim.SetBool("isRevived", true);

        playerMovement.playerMoveSpeed *= 10f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.gameObject != this.gameObject)
        {
            StartCoroutine(InstantDie());
        }
    }


    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        if (isDamaged)
        {
            anim.SetBool("isDead", true);
            playerMovement.playerMoveSpeed = 0;
            isAlive = false;
            GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>().GameOver(playerNum);
        }
    }

    private IEnumerator InstantDie()
    {
        yield return null;
        if (isDamaged)
        {
            anim.SetBool("isDead", true);
            playerMovement.playerMoveSpeed = 0;
            isAlive = false;
            GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>().GameOver(playerNum);
        }
    }
}
