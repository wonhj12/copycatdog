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
    //시간 관련 변수들
    [Header("시간 관련 변수들")]
    public float DelayTime;
    public float DestroyTime;

    //길이, 방향 관련 변수들
    [Header("길이, 방향 관련 변수들")]
    public int remain_Length;
    public int Direction = 5;

    //필요한 컴포넌트
    [Header("필요한 컴포넌트")]
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
                GameObject Stream0 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
                Stream0.GetComponent<Water>().Direction = (int)Dir.up;
                Stream0.GetComponent<Water>().remain_Length = remain_Length - 1;
                break;

            case (int)Dir.down:
                GameObject Stream1 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x, this.transform.position.y - 1), Quaternion.identity);
                Stream1.GetComponent<Water>().Direction = (int)Dir.down;
                Stream1.GetComponent<Water>().remain_Length = remain_Length - 1;
                break;

            case (int)Dir.left:
                GameObject Stream2 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
                Stream2.GetComponent<Water>().Direction = (int)Dir.left;
                Stream2.GetComponent<Water>().remain_Length = remain_Length - 1;
                break;

            case (int)Dir.right:
                GameObject Stream3 = Instantiate(WaterPrefab, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
                Stream3.GetComponent<Water>().Direction = (int)Dir.right;
                Stream3.GetComponent<Water>().remain_Length = remain_Length - 1;
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
    }
}
