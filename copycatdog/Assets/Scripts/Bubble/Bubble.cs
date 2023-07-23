using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //시간 관련 변수들
    [Header("시간 관련 변수들")]
    public float explodeTime;           //물풍선 폭파 지점
    public float waterDestroyTime;      //물풍선 삭제 지점

    //길이, 방향 관련 변수들
    [Header("길이, 방향 관련 변수들")]
    public int Length;
    private int slipDir;
    private bool isSlipped = false;
    [SerializeField] private float slipSpeed;

    //필요한 컴포넌트
    [Header("필요한 컴포넌트")]
    public GameObject Water;
    public Character player;
    [SerializeField] private BoxCollider2D box_Col;
    [SerializeField] private GameObject circle_Col;
    public Sprite explodeSprite;
    [SerializeField] private SpriteRenderer sprite;

    [Header("상태 변수")]
    public bool isExploded = false;
    public bool isPlayerIn = true;
    private bool[] isWall = { false, false, false, false };

    private void Start()
    {
        box_Col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        box_Col.enabled = false;
        circle_Col.SetActive(true);

        StartCoroutine(WaterSplash());
        Destroy(this.gameObject, explodeTime + waterDestroyTime);
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            box_Col.isTrigger = false;
            isPlayerIn = false;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerIn = true;
            player = collision.GetComponent<Character>();       //닿아있는 플레이어의 캐릭터 오브젝트 가져오기
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Obstacle") && isSlipped)
        {
            switch (slipDir)
            {
                case 0:
                    RaycastHit2D hit_1 = Physics2D.Raycast(this.transform.position, Vector2.up, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if(hit_1.transform != null)
                    {
                        SlipStop();
                    }
                    break;
                case 1:
                    RaycastHit2D hit_2 = Physics2D.Raycast(this.transform.position, Vector2.down, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_2.transform != null)
                    {
                        SlipStop();
                    }
                    break;
                case 2:
                    RaycastHit2D hit_3 = Physics2D.Raycast(this.transform.position, Vector2.left, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_3.transform != null)
                    {
                        SlipStop();
                    }
                    break;
                case 3:
                    RaycastHit2D hit_4 = Physics2D.Raycast(this.transform.position, Vector2.right, 0.4f, LayerMask.GetMask("Wall", "WorldLimit"));
                    if (hit_4.transform != null)
                    {
                        SlipStop();
                    }
                    break;
            }
        }
    }


    private void SlipStop()
    {
        this.transform.position = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
        isSlipped = false;
    }


    private void FixedUpdate()
    {
        if (isSlipped)
        {
            switch (slipDir)
            {
                case 0:
                    this.transform.Translate(Vector2.up * slipSpeed * 0.016f);
                    break;
                case 1:
                    this.transform.Translate(Vector2.down * slipSpeed * 0.016f);
                    break;
                case 2:
                    this.transform.Translate(Vector2.left * slipSpeed * 0.016f);
                    break;
                case 3:
                    this.transform.Translate(Vector2.right * slipSpeed * 0.016f);
                    break;
            }
        }
    }


    public IEnumerator WaterSplash()
    {
        yield return new WaitForSeconds(explodeTime - 0.2f);

        CheckWall();

        isExploded = true;
        sprite.sprite = explodeSprite;

        if (isPlayerIn)
        {
            player.Damage();
        }

        if (isWall[0] == false)
        {
            GameObject Bubble_Up = Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
            Bubble_Up.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Up.GetComponent<Water>().Direction = (int)Dir.up;
        }

        if (isWall[1] == false)
        {
            GameObject Bubble_Down = Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y - 1), Quaternion.identity);
            Bubble_Down.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Down.GetComponent<Water>().Direction = (int)Dir.down;
        }

        if (isWall[2] == false)
        {
            GameObject Bubble_Left = Instantiate(Water, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
            Bubble_Left.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Left.GetComponent<Water>().Direction = (int)Dir.left;
        }

        if (isWall[3] == false)
        {
            GameObject Bubble_Right = Instantiate(Water, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
            Bubble_Right.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Right.GetComponent<Water>().Direction = (int)Dir.right;
        }
    }

    public IEnumerator WaterSplash_Immediate()
    {
        CheckWall();

        isExploded = true;

        if (isPlayerIn)
        {
            player.Damage();
        }

        if (isWall[0] == false)
        {
            GameObject Bubble_Up = Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
            Bubble_Up.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Up.GetComponent<Water>().Direction = (int)Dir.up;
        }

        if (isWall[1] == false)
        {
            GameObject Bubble_Down = Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y - 1), Quaternion.identity);
            Bubble_Down.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Down.GetComponent<Water>().Direction = (int)Dir.down;
        }

        if (isWall[2] == false)
        {
            GameObject Bubble_Left = Instantiate(Water, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
            Bubble_Left.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Left.GetComponent<Water>().Direction = (int)Dir.left;
        }

        if (isWall[3] == false)
        {
            GameObject Bubble_Right = Instantiate(Water, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
            Bubble_Right.GetComponent<Water>().remain_Length = Length - 1;
            Bubble_Right.GetComponent<Water>().Direction = (int)Dir.right;
        }

        Destroy(this.gameObject);

        yield return null;
    }

    private void CheckWall()
    {
        RaycastHit2D hit_up = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, 1, LayerMask.GetMask("Wall"));
        if(hit_up.transform != null)
        {
            isWall[0] = true;
        }

        RaycastHit2D hit_down = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 1, LayerMask.GetMask("Wall"));
        if (hit_down.transform != null)
        {
            isWall[1] = true;
        }

        RaycastHit2D hit_left = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 1, LayerMask.GetMask("Wall"));
        if (hit_left.transform != null)
        {
            isWall[2] = true;
        }

        RaycastHit2D hit_right = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 1, LayerMask.GetMask("Wall"));
        if (hit_right.transform != null)
        {
            isWall[3] = true;
        }
    }

    public void Slip(int dir)
    {
        Debug.Log("밀기 " + dir);
        slipDir = dir;
        isSlipped = true;
    }
}
