using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir{
    up,
    down,
    left,
    right
};

public class Water : MonoBehaviour
{
    //�ð� ���� ������
    [Header("�ð� ���� ������")]
    public float DelayTime;
    public float DestroyTime;

    //����, ���� ���� ������
    [Header("����, ���� ���� ������")]
    public int remain_Length;
    public int Direction = 5;

    //�ʿ��� ������Ʈ
    [Header("�ʿ��� ������Ʈ")]
    public GameObject WaterPrefab;

    private void Start()
    {
        Destroy(this.gameObject, DestroyTime);
        if (remain_Length > 0)
        {
            StartCoroutine(WaterStream(Direction));
        }
    }

    private IEnumerator WaterStream(int dir)
    {
        yield return new WaitForSeconds(DelayTime);

        switch (dir)
        {
            case (int)Dir.up:
                RaycastHit2D hit_up = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, 1, LayerMask.GetMask("Wall"));
                if(hit_up.transform == null)
                {
                    GameObject Stream0 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
                    Stream0.GetComponent<Water>().Direction = (int)Dir.up;
                    Stream0.GetComponent<Water>().remain_Length = remain_Length - 1;
                }
                else
                {
                    if (hit_up.transform.CompareTag("Obstacle"))
                    {
                        //Destroy ��� ������ ��� �Լ��� ȣ���ϸ� ��
                        Destroy(hit_up.transform.gameObject);
                    }
                }
                break;

            case (int)Dir.down:
                RaycastHit2D hit_down = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 1, LayerMask.GetMask("Wall"));
                if (hit_down.transform == null)
                {
                    GameObject Stream1 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 1), Quaternion.identity);
                    Stream1.GetComponent<Water>().Direction = (int)Dir.down;
                    Stream1.GetComponent<Water>().remain_Length = remain_Length - 1;
                }
                else
                {
                    if (hit_down.transform.CompareTag("Obstacle"))
                    {
                        //Destroy ��� ������ ��� �Լ��� ȣ���ϸ� ��
                        Destroy(hit_down.transform.gameObject);
                    }
                }
                break;

            case (int)Dir.left:
                RaycastHit2D hit_left = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 1, LayerMask.GetMask("Wall"));
                if (hit_left.transform == null)
                {
                    GameObject Stream2 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
                    Stream2.GetComponent<Water>().Direction = (int)Dir.left;
                    Stream2.GetComponent<Water>().remain_Length = remain_Length - 1;
                }
                else
                {
                    if (hit_left.transform.CompareTag("Obstacle"))
                    {
                        //Destroy ��� ������ ��� �Լ��� ȣ���ϸ� ��
                        Destroy(hit_left.transform.gameObject);
                    }
                }
                break;

            case (int)Dir.right:
                RaycastHit2D hit_right = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 1, LayerMask.GetMask("Wall"));
                if (hit_right.transform == null)
                {
                    GameObject Stream3 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
                    Stream3.GetComponent<Water>().Direction = (int)Dir.right;
                    Stream3.GetComponent<Water>().remain_Length = remain_Length - 1;
                }
                else
                {
                    if (hit_right.transform.CompareTag("Obstacle"))
                    {
                        //Destroy ��� ������ ��� �Լ��� ȣ���ϸ� ��
                        Destroy(hit_right.transform.gameObject);
                    }
                }
                break;

            default:
                Debug.Log("error");
                break;
        }

        yield return null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("PlayerHitBox"))
        {
            Debug.Log("Hit");
            collision.GetComponentInParent<Character>().Damage();
        }

        if (collision.CompareTag("Item"))
        {
            Debug.Log(collision.name);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Bubble"))
        {
            //�̹� ��ǳ���� �������°� �ƴ϶��
            if(collision.GetComponent<Bubble>().isExploded == false)
            {
                Debug.Log("������� " + collision.transform.name);
                collision.GetComponent<Bubble>().StopAllCoroutines();

                collision.GetComponent<Bubble>().StartCoroutine(collision.GetComponent<Bubble>().WaterSplash_Immediate());
            }
        }
    }
}
